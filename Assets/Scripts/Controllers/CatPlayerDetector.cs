using UnityEngine;

public class CatPlayerDetector : MonoBehaviour
{
    private CatBehavior cat;
    void Start()
    {
        this.cat = this.transform.parent.GetComponent<CatBehavior>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        print("im a cat and a player is nearby");
        this.cat.OnPlayerDetected(collision.gameObject);
    }
}
