using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    public int size;
    public readonly ISet<CatBehavior> cats = new HashSet<CatBehavior>();

    public override void Interact(PlayerOne player)
    {
        var cat = player.heldObject.GetComponent<CatBehavior>();
        if (cat == null)
        {
            throw new System.InvalidOperationException("tried to put something other than cat in box");
        }
        // fake the player putting down the cat
        var catInteraction = cat.GetComponent<CatInteraction>();
        catInteraction.Interact(player);
        catInteraction.OnPickUp();
        catInteraction.isBeingHeld = true;
        cat.transform.SetParent(this.transform);
        cat.transform.localPosition = Vector3.zero;
        this.cats.Add(cat);
        print("putting cat in box. # cats = " + cats.Count);
    }

    public override bool IsInteractable(PlayerOne player)
    {
        return player.heldObject?.GetComponent<CatBehavior>() != null && this.cats.Count < size;
    }
}
