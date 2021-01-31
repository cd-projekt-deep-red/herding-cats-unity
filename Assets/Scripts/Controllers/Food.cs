using UnityEngine;

public class Food : Holdable
{
    public float amount = 1f;
    private new Collider2D collider;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite halfEmptySprite;
    [SerializeField] private Sprite emptySprite;

    private void Awake()
    {
        this.collider = GetComponent<BoxCollider2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnPickUp()
    {
        this.collider.enabled = false;
    }

    public override void OnPutDown()
    {
        this.collider.enabled = true;
    }

    public void Consume()
    {
        this.amount -= .01f;
        if (this.amount <= 0.5f && this.amount > 0f)
        {
            this.spriteRenderer.sprite = this.halfEmptySprite;
        }
        else if (this.amount <= 0f)
        {
            this.spriteRenderer.sprite = this.emptySprite;
            this.collider.enabled = false;
        }
    }
}
