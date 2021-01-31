using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Box : Holdable
{
    public int size;
    private IList<CatBehavior> cats = new List<CatBehavior>();

    public override void Interact(PlayerOne player)
    {
        if (player.heldObject == null || player.heldObject == this)
        {
            // normal holdable interaction
            base.Interact(player);
        }
        else
        {
            var cat = player.heldObject.GetComponent<CatBehavior>();
            if (cat == null)
            {
                throw new System.InvalidOperationException("tried to put something other than cat in box");
            }
            // fake the player putting down the cat
            var catInteraction = cat.GetComponent<CatInteraction>();
            var catSpriteRenderer = cat.GetComponent<SpriteRenderer>();
            catInteraction.Interact(player);
            catInteraction.OnPickUp();
            catInteraction.isBeingHeld = true;
            cat.transform.SetParent(this.transform);
            cat.transform.localPosition = Vector3.up * Random.Range(.5f, .9f) + Vector3.right * Random.Range(-.15f, .15f);
            catSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            catSpriteRenderer.sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder + 1;
            this.cats.Add(cat);
        }
    }

    public override bool IsInteractable(PlayerOne player)
    {
        if (player.heldObject == null) return true;
        if (player.heldObject.GetComponent<CatBehavior>() != null && !IsFull()) return true;
        return false;
    }

    public override void OnPickUp()
    {
      

    }

    public override void OnPutDown()
    {
       
    }

    private bool IsFull()
    {
        this.cats = this.cats.Where(c => c != null).ToList(); // prune cats that have been extracted
        return this.cats.Count() >= size;
    }
}
