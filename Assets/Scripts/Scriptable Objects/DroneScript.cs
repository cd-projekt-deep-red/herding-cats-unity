using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour
{
    public float droneCycleTime = 60.0f;
    [SerializeField]public UIScript UIScript;
    [SerializeField]private Animator droneAnimator;
    public GameState gameState;
    [SerializeField] private GoalDetector goalScript;


    private float timeToCycle = 0f;


    public void SetAnimationTrigger(bool triggerState, bool catsPresent)
    {
      droneAnimator.SetBool("DronePickup", triggerState);
      droneAnimator.SetBool("CatsPresent", catsPresent);
    }

    void Update()
    {
      if(timeToCycle <= 0.0f)
      {
        SetAnimationTrigger(false, false);
        timeToCycle = droneCycleTime;
      }
      else
      {
        timeToCycle = timeToCycle - Time.deltaTime;
        UIScript.SetDroneTime(timeToCycle);
        if(timeToCycle < 3f)
        {
          if(goalScript.catsInGoal.Count > 0)
          {
            // Cats in Goal
            SetAnimationTrigger(true, true);
          }
          else
          {
            // No Cats in Goal
            SetAnimationTrigger(true, false);
          }
        }
      }
    }

    public void droneTakeoff()
    {
        if (goalScript.catsInGoal.Count != 0)
        {
            int catsevacuated = 0;
            Debug.Log(goalScript.catsInGoal.Count.ToString() + "cats in goal");
            gameState.catsEvacuated(goalScript.catsInGoal);
            Debug.Log("cat count " + goalScript.catsInGoal.ToString());
            for (int i = 0; i < goalScript.catsInGoal.Count; i++)
            {
                catsevacuated++;
                Destroy(goalScript.catsInGoal[i]);
            }
            Debug.Log(catsevacuated.ToString() + "cats evacuated");
            UIScript.updatePlayerMoney();
        }
     }

}
