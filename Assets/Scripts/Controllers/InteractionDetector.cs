using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private readonly IList<Interactable> interactables = new List<Interactable>();
    private Interactable closestInteractable = null;
    private PlayerOne player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = this.transform.parent.GetComponent<PlayerOne>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this.closestInteractable == null && this.player.heldObject != null)
            {
                this.player.heldObject.Interact(this.player);
            }
            else if (this.closestInteractable != null)
            {
                this.closestInteractable.Interact(this.player);
            }
        }
        CalculateClosestInteractable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.interactables.Add(collision.GetComponent<Interactable>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.interactables.Remove(collision.GetComponent<Interactable>());
    }

    private void CalculateClosestInteractable()
    {
        Interactable newClosest = this.interactables
            .Where(interactable => interactable.IsInteractable(this.player))
            .OrderBy(interactable => (interactable.transform.position - this.transform.position).magnitude)
            .FirstOrDefault();
        if (newClosest != this.closestInteractable)
        {
            if (newClosest != null) newClosest.Highlight();
            if (this.closestInteractable != null) this.closestInteractable.UnHighlight();
            this.closestInteractable = newClosest;
        }
    }
}
