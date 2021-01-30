using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GoalObject", menuName = "GoalObject")]
public class GoalObject : ScriptableObject
{
    public bool goalCompleted;
    public string goalType;
    public GameState gameState;


}
