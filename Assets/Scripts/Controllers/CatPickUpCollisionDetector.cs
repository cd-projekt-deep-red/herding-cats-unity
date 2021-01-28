using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatPickUpCollisionDetector : MonoBehaviour
{
    private IList<Interactable> interactables = new List<Interactable>();
    private Interactable closestInteractable = null;
    private PlayerOne player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = this.gameObject.transform.parent.gameObject.GetComponent<PlayerOne>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.closestInteractable != null)
        {
            this.closestInteractable.Interact(this.player);
        }
        CalculateClosestInteractable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.interactables.Add(collision.gameObject.GetComponent<Interactable>());
        //CalculateClosestInteractable();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.interactables.Remove(collision.gameObject.GetComponent<Interactable>());
        //CalculateClosestInteractable();
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
