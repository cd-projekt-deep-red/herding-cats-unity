using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timerText;
    [SerializeField]private TextMeshProUGUI moneyText;

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
      if(cashValue >= 0)
      {
          // Add Cash

      }
      else
      {
        // Minus Cash

      }
    }

    public void UpdatePlayerMoney()
    {
      moneyText.text = ((int)playerCash).ToString();
    }
}
