using UnityEngine;

public class Food : Holdable
{
    public int amount;

    public override void OnPickUp()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public override void OnPutDown()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
