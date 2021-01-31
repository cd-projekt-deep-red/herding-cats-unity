using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemInteractable : Interactable
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private StoreController store;
    [SerializeField] private float costOfItem = 10f;
    private Holdable holdable;

   
    private void Start()
    {
        store = this.transform.parent.GetComponent<StoreController>();
    }


    public override void Interact(PlayerOne player)
    {
        if(player.heldObject == null)
        {
            
           if(store.gameState.playerMoney >= costOfItem)//need to look at how to get money of item
            {
                store.itemPurchased(costOfItem);
                GameObject realObject = Instantiate(itemPrefab, new Vector3 { x = 0f, y = 0f, z = 0f }, Quaternion.identity);
                holdable = realObject.GetComponent<Holdable>();
                holdable.Interact(player);
                
            }
            else if (store.gameState.playerMoney < costOfItem)
            {
                store.notEnoughMoney();
            }
        }
        else 
        {

        }
        
    }

    public override bool IsInteractable(PlayerOne player)
    {
        return true;
    }
}
