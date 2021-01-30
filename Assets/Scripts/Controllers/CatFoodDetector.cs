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
        this.cat.OnFoodDetected(collision.gameObject);
    }
}
