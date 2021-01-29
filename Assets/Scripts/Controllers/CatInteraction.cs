using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInteraction : Interactable
{

    public override void Interact(PlayerOne player)
    {
        print("Player tried to interact with me");
        this.GetComponent<CatBehavior>().enabled = false;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        this.transform.SetParent(player.transform);
        this.transform.localPosition = Vector3.zero + Vector3.up * 1;
    }

    public override bool IsInteractable(PlayerOne player)
    {
        return true;
    }
}
