using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timerText;
    [SerializeField]private TextMeshProUGUI moneyText;
    [SerializeField]private TextMeshProUGUI valueChangeText;
    [SerializeField]private Animator paypurrAnimation;
    [SerializeField] private GameState gameState;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] moneyAdd;
    [SerializeField] private AudioClip[] UIMusic;

    public bool isIntroMusic = true;

    private float playerCash = 0f;

    public void SetDroneTime(float timeLeft)
    {
      int secondsLeft = (int)timeLeft % 60;
      int minutesLeft = (int)Mathf.Floor(timeLeft / 60);
      string timerString = " Next Drone";

      if(timeLeft <= 15.0f)
      {
        timerText.text ="<color=#df3e23>" + minutesLeft.ToString() + ":" + secondsLeft.ToString("D2") + "</color>" + timerString;
      }
      else
      {
        timerText.text = minutesLeft.ToString() + ":" + secondsLeft.ToString("D2") + timerString;
      }
    }

    public void updatePlayerMoney()
    {

      if(gameState.playerMoney >= playerCash )
      {
          // Add Cash
          valueChangeText.text = "<color=#59c135>+" + (gameState.playerMoney - playerCash).ToString();
          playerCash = gameState.playerMoney;
          audioSource.PlayOneShot(moneyAdd[Random.Range(0, moneyAdd.Length)]);
      }
      else
      {
        // Minus Cash
        valueChangeText.text = "<color=#df3e23>" + (gameState.playerMoney - playerCash).ToString();
            playerCash = gameState.playerMoney;
      }

      paypurrAnimation.SetTrigger("Count Change");
      moneyText.text = ((int)playerCash).ToString();
      StartCoroutine(WaitAndResetPaypurr(0.1f));
    }

    private IEnumerator WaitAndResetPaypurr(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        paypurrAnimation.ResetTrigger("Count Change");
    }

    void Update()
    {
      if(isIntroMusic)
      {
        isIntroMusic = false;
        audioSource.clip = UIMusic[0];
        audioSource.Play();
        StartCoroutine(PlayOutOfLoop());
      }

    }

    IEnumerator PlayOutOfLoop()
    {
      yield return new WaitForSeconds(audioSource.clip.length);
      audioSource.clip = UIMusic[1];
      audioSource.Play();
      StartCoroutine(PlayOutOfLoop());
    }
}
