using UnityEngine;

public class CatInteraction : Holdable
{
    private Animator animator;
    private CatBehavior catBehavior;
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        this.animator = GetComponent<Animator>();
        this.catBehavior = GetComponent<CatBehavior>();
        this.rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override void OnPickUp()
    {
        this.animator.SetBool("MoveRight", false);
        this.animator.SetBool("MoveLeft", false);
        this.animator.SetBool("Sit", true);
        this.catBehavior.enabled = false;
        this.rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void OnPutDown()
    {
        this.catBehavior.enabled = true;
        this.rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}
