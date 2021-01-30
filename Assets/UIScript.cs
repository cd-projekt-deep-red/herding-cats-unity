using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timerText;

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
}
