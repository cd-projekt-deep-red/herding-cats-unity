using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInteraction : Interactable
{

    public override void Interact(PlayerOne player)
    {
        print("Player tried to interact with me");
        this.gameObject.SetActive(false);
    }

    public override bool IsInteractable(PlayerOne player)
    {
        return true;
    }
}
