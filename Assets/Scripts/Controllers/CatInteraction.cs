using UnityEngine;

public class CatInteraction : Holdable
{
    private Animator animator;
    private CatBehavior catBehavior;
    private new Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider;
    private Vector2 originalCatColiderSize = new Vector2 { x = 0.8181818f, y = 0.6363636f };

    

    private void Start()
    {
        this.animator = GetComponent<Animator>();
        this.catBehavior = GetComponent<CatBehavior>();
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.boxCollider = GetComponent<BoxCollider2D>();
        
      
    }

    public override void OnPickUp()
    {
        this.animator.SetBool("MoveRight", false);
        this.animator.SetBool("MoveLeft", false);
        this.animator.SetBool("Sit", true);
        this.catBehavior.enabled = false;
        this.rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        this.boxCollider.offset = Vector3.up * -1.6f;
        this.boxCollider.size = new Vector2 { x = .1f, y = .1f };
        Debug.Log("cat hitbox moved to feet");
    }

    public override void OnPutDown()
    {
        this.catBehavior.enabled = true;
        this.rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        this.boxCollider.size = originalCatColiderSize;
        this.boxCollider.offset = Vector3.zero;
    }
}
