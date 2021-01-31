using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalItem : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI goalText;
    public Animator animator;

    public void RemoveGoalItem()
    {
      Destroy(this.gameObject, 0f);
    }

    // TEMPORARY ENABLE GOAL
    void Start()
    {
      StartCoroutine(ShowBasicGoal());
    }

    IEnumerator ShowBasicGoal()
    {
      yield return new WaitForSeconds(4);
      animator.SetTrigger("IsActive");
    }
}
