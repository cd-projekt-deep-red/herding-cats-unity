using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalUI : MonoBehaviour
{
    [SerializeField]private Animator goalUI;

    // Change display state of the goal
    public void ToggleGoalDisplay(bool isLarge)
    {
      goalUI.SetBool("NearGoal", isLarge);
    }
}
