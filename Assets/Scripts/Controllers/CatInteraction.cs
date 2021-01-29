using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInteraction : Holdable
{
    public override void OnPickUp()
    {
        this.GetComponent<CatBehavior>().enabled = false;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public override void OnPutDown()
    {
        this.GetComponent<CatBehavior>().enabled = true;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
