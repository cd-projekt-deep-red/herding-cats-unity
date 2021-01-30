public abstract class Holdable : Interactable
{
    public bool isBeingHeld = false;

    public abstract void OnPickUp();

    public abstract void OnPutDown();

    public override void Interact(PlayerOne player)
    {
        if (player.heldObject == null)
        {
            OnPickUp();
            player.PickUp(this);
            this.isBeingHeld = true;
        }
        else if (player.heldObject == this)
        {
            OnPutDown();
            player.PutDownHeldObject();
            this.isBeingHeld = false;
        }
    }

    public override bool IsInteractable(PlayerOne player)
    {
        if (player.heldObject == this) return false;
        if (player.heldObject != null) return false;
        if (this.isBeingHeld) return false;
        return true;
    }
}
