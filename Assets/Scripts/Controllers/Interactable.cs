using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private bool isHighlighted = false;

    public abstract void Interact(PlayerOne player);
    public abstract bool IsInteractable(PlayerOne player);

    public void Highlight()
    {
        if (!this.isHighlighted)
        {
            GetComponent<SpriteRenderer>().material.SetFloat("_OutlineThickness", 1f);
            this.isHighlighted = true;
        }
    }

    public void UnHighlight()
    {
        if (this.isHighlighted)
        {
            GetComponent<SpriteRenderer>().material.SetFloat("_OutlineThickness", 0f);
            this.isHighlighted = false;
        }
    }
}
