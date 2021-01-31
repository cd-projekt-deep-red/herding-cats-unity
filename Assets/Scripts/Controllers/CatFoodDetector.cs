using System;
using UnityEngine;

public class CatFoodDetector : MonoBehaviour
{
    private CatBehavior cat;
    void Awake()
    {
        this.cat = this.transform.parent.GetComponent<CatBehavior>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var food = collision.gameObject.GetComponent<Food>();
        if (food == null)
        {
            throw new InvalidOperationException("Found object without Food script.");
        }
        this.cat.OnFoodDetected(food);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var food = collision.gameObject.GetComponent<Food>();
        if (food == null)
        {
            throw new InvalidOperationException("Found object without Food script.");
        }
        this.cat.OnFoodRemoved(food);
    }
}
