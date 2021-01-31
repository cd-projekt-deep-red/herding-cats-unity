using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckCutscene : MonoBehaviour
{
    [SerializeField]private SpriteMask spriteMask;
    [SerializeField]private Sprite[] maskSprites;
    [SerializeField]private GameObject catsWrapper;
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private AudioClip[] audioClip;
    [SerializeField]private GameObject player;
    [SerializeField] private Animator animator;

    private bool isFirstSound = true;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        animator.SetBool("Trigger Cutscene", true);
    }

    public void SetMaskSprite(int spriteIndex)
    {
      spriteMask.sprite = maskSprites[spriteIndex];
    }

    public void RemoveCutsceneObjects()
    {
      Destroy(catsWrapper, 0f);
    }

    public void PlaySounds(float pitch)
    {
      if(isFirstSound)
      {
        audioSource.PlayOneShot(audioClip[0]);
        isFirstSound = false;
      }
      else
      {
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(audioClip[1]);
      }


    }

    public void SetPlayerPosition(int positionIndex)
    {
      Vector3 playerPosA = new Vector3(-3.993258f, 5.095146f, 0f);
      Vector3 playerPosB = new Vector3(0f, 0f, 0f);
      if(positionIndex == 0)
      {
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.transform.position = playerPosA;
      }
      else
      {
        player.transform.position = playerPosB;
        player.GetComponent<SpriteRenderer>().enabled = true;
      }
    }
}
