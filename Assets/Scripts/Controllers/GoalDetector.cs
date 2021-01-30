using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    public IList<GameObject> catsInGoal = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            catsInGoal.Add(collision.gameObject);

            //Debug.Log($"you have caught {catsInGoal.Count} cats");
        
       
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        catsInGoal.Remove(collider.gameObject);

        //Debug.Log($"you have caught {catsInGoal.Count} cats");
    }


}
