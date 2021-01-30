using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemInteractable : Interactable
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private StoreController store;
    
    public override void Interact(PlayerOne player)
    {
        if(player.heldObject == null)
        {
           //GameObject purchasedItem
        }
        else
        {

        }
        
    }

    public override bool IsInteractable(PlayerOne player)
    {
        throw new System.NotImplementedException();
    }
}
