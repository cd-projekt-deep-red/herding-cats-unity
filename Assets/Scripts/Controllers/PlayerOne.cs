using UnityEngine;

public class PlayerOne : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Vector3 leftFootLocation;
    [SerializeField] private Vector3 rightFootLocation;
    [SerializeField] private GameObject footprintPrefab;
    [SerializeField] private HorzMovementDirection playerHorzDirection;
    [Range(-1, 1)] private float lastPlayerMovement;

  
    [SerializeField] private GameObject goalGO;

    public Holdable heldObject;
    public float playerSpeed = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * 5000;

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rigidbody.AddForce(movement);

        //need to calculate layer based on transform.y
        spriteRenderer.sortingOrder = (int) (-1* transform.position.y + 150f);

       
    }

    private void LateUpdate()
    {
        playerSpeed = rigidbody.velocity.magnitude;
        if (playerSpeed > 2 && rigidbody.velocity.x > 0)
        {
            characterAnimator.SetBool("Moving", true);
            characterAnimator.SetFloat("DirX", rigidbody.velocity.x);
            playerHorzDirection = HorzMovementDirection.East;
        }
        if (playerSpeed > 2 && rigidbody.velocity.x < 0)
        {
            characterAnimator.SetBool("Moving", true);
            characterAnimator.SetFloat("DirX", rigidbody.velocity.x);
            playerHorzDirection = HorzMovementDirection.West;
        }
        if (playerSpeed < 2)
        {
            // Player has slowed enough to stop moving
            characterAnimator.SetBool("Moving", false);
            if(playerSpeed > 1)
            {
                lastPlayerMovement = rigidbody.velocity.x;
                characterAnimator.SetFloat("Last DirX", lastPlayerMovement);
            }
            
            playerHorzDirection = HorzMovementDirection.None;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public void SpawnFootprint(int foot)
    {
      Vector3 footPosition = this.gameObject.transform.position;
      // 0 for left foot, 1 for right foot
      if(foot == 0)
      {
        // Left Foot
        if(playerHorzDirection != HorzMovementDirection.East)
        {
          Vector3 inverseFootPosition = leftFootLocation;
          inverseFootPosition.x = inverseFootPosition.x * -1f;
          footPosition = footPosition + inverseFootPosition;
        }
        else
        {
          footPosition = footPosition + leftFootLocation;
        }
      }
      else
      {
        // Right foot
        if(playerHorzDirection != HorzMovementDirection.East)
        {
          Vector3 inverseFootPosition = rightFootLocation;
          inverseFootPosition.x = inverseFootPosition.x * -1f;
          footPosition = footPosition + inverseFootPosition;
        }
        else
        {
          footPosition = footPosition + rightFootLocation;
        }
      }

      Instantiate(footprintPrefab, footPosition, Quaternion.identity);
    }

    public void PickUp(Holdable holdable)
    {
        holdable.transform.SetParent(this.transform);
        holdable.transform.localPosition = Vector3.up * 1;
        this.heldObject = holdable;
    }

    public void PutDownHeldObject()
    {
        this.heldObject.transform.localPosition = Vector3.up * -0.25f;
        this.heldObject.transform.SetParent(null);
        this.heldObject = null;
    }

    public enum HorzMovementDirection {
      East,
      West,
      None
    }

}
