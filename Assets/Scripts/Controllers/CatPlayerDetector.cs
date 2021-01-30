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
        this.cat.OnPlayerDetected(collision.gameObject);
    }
}
