public abstract class Holdable : Interactable
{
    public abstract void OnPickUp();

    public abstract void OnPutDown();

    public override void Interact(PlayerOne player)
    {
        if (player.heldObject == null)
        {
            OnPickUp();
            player.PickUp(this);
        }
        else if (player.heldObject == this)
        {
            OnPutDown();
            player.PutDownHeldObject();
        }
    }

    public override bool IsInteractable(PlayerOne player)
    {
        if (player.heldObject == this) return false;
        if (player.heldObject != null) return false;
        return true;
    }
}
