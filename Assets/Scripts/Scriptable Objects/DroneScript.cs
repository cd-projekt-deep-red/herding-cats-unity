﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour
{
    public float droneCycleTime = 60.0f;
    [SerializeField]public UIScript UIScript;
    [SerializeField]private Animator droneAnimator;

    private float timeToCycle = 0f;

    public void SetAnimationTrigger(bool triggerState)
    {
      droneAnimator.SetBool("DronePickup", triggerState);
    }

    void Update()
    {
      if(timeToCycle <= 0.0f)
      {
        SetAnimationTrigger(false);
        timeToCycle = droneCycleTime;
      }
      else
      {
        timeToCycle = timeToCycle - Time.deltaTime;
        UIScript.SetDroneTime(timeToCycle);
        if(timeToCycle < 3f)
        {
          SetAnimationTrigger(true);
        }
      }
    }
}