﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timerText;
    [SerializeField]private TextMeshProUGUI moneyText;
    [SerializeField]private TextMeshProUGUI valueChangeText;
    [SerializeField]private Animator paypurrAnimation;

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

    public void SetPlayerMoney(float cashValue)
    {
      playerCash = playerCash + cashValue;
      if(cashValue >= 0)
      {
          // Add Cash
          valueChangeText.text = "<color=#59c135>" + cashValue.ToString();
      }
      else
      {
        // Minus Cash
        valueChangeText.text = "<color=#df3e23>" + cashValue.ToString();
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
}