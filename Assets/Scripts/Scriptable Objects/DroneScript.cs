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

    public void droneTakeoff()
    {
        if (goalScript.catsInGoal.Count != 0)
        {
            gameState.catsEvacuated(goalScript.catsInGoal);
            for (int i = 0; i < goalScript.catsInGoal.Count; i++)
            {
                Destroy(goalScript.catsInGoal[i]);
            }
            UIScript.updatePlayerMoney();
        }
        
     }

}
