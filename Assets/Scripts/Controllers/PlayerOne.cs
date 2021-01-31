using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerOne : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Vector3 leftFootLocation;
    [SerializeField] private Vector3 rightFootLocation;
    [SerializeField] private GameObject footprintPrefab;
    [SerializeField] private HorzMovementDirection playerHorzDirection;
    [SerializeField] private GameObject goalGO;
    [SerializeField] private Tilemap fenceTilemap;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] grassSteps;
    [SerializeField] private RuleTile fenceTiles;
    [SerializeField] private Tile highlightTile;

    public Holdable heldObject;
    public float playerSpeed = 0f;
    public bool isCrouching = false;
    [Range(-1, 1)] private float lastPlayerMovement;
    public float timestill = 0f;

    private Vector3Int previousTile;

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
        spriteRenderer.sortingOrder = (int) (-1* transform.position.y + 151f);


    }

    private void Update()
    {
        if (heldObject != null && heldObject.GetComponent<Fence>() != null)
        {
            //calculate the tile we want to place on 
            Vector3 footposition = this.transform.position - new Vector3 { x = 0f, y = -.65f, z = 0f };

            Vector3Int cellCoordinate = groundTilemap.WorldToCell(footposition);
            //place tile
            if(previousTile != cellCoordinate)
            {
                groundTilemap.SetTile(previousTile, null);
            }
            groundTilemap.SetTile(cellCoordinate, highlightTile);
            previousTile = cellCoordinate;
        }
    }

    private void LateUpdate()
    {
        playerSpeed = rigidbody.velocity.magnitude;
        if (playerSpeed > 2 && rigidbody.velocity.x > 0)
        {
            this.isCrouching = false;
            characterAnimator.SetBool("Moving", true);
            characterAnimator.SetBool("Crouching", false);
            characterAnimator.SetFloat("DirX", rigidbody.velocity.x);
            playerHorzDirection = HorzMovementDirection.East;
        }
        if (playerSpeed > 2 && rigidbody.velocity.x < 0)
        {
            this.isCrouching = false;
            characterAnimator.SetBool("Moving", true);
            characterAnimator.SetBool("Crouching", false);
            characterAnimator.SetFloat("DirX", rigidbody.velocity.x);
            playerHorzDirection = HorzMovementDirection.West;
        }
        if (playerSpeed < 2 && timestill < 5f)
        {
            // Player has slowed enough to stop moving
            characterAnimator.SetBool("Moving", false);
            if(playerSpeed > 1)
            {
                lastPlayerMovement = rigidbody.velocity.x;
                characterAnimator.SetFloat("Last DirX", lastPlayerMovement);
            }

            timestill = timestill + Time.deltaTime;
            playerHorzDirection = HorzMovementDirection.None;
        }
        if(playerSpeed<2 && timestill > 5f)
        {
            timestill = 0f;
            this.isCrouching = true;
            characterAnimator.SetBool("Moving", false);
            characterAnimator.SetBool("Crouching", true);
            timestill = timestill + Time.deltaTime;
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
        if (holdable.GetComponent<CatBehavior>() != null) // if cat
        {
            holdable.GetComponent<BoxCollider2D>().offset = Vector3.up * -1.6f;
            holdable.GetComponent<BoxCollider2D>().size = new Vector2 { x = .1f, y = .1f };
            Debug.Log("cat hitbox moved to feet");
        }
        holdable.transform.SetParent(this.transform);
        holdable.transform.localPosition = Vector3.up * 1;
        this.heldObject = holdable;
    }

    public void PutDownHeldObject()
    {
        if (heldObject.GetComponent<CatBehavior>() != null) // if cat
        {
            Vector2 originalCatColiderSize = new Vector2 { x = 0.8181818f, y = 0.6363636f };
            heldObject.GetComponent<BoxCollider2D>().size = originalCatColiderSize;
            heldObject.GetComponent<BoxCollider2D>().offset = Vector3.zero;
        }
        if (heldObject.GetComponent<Fence>() != null)//if the held object is a fence
        {

            //calculate the tile we want to place on 
            Vector3 footposition = this.transform.position - new Vector3 { x = 0f, y = -.65f, z = 0f};

            Vector3Int cellCoordinate = fenceTilemap.WorldToCell(footposition);
            //place tile
            
            fenceTilemap.SetTile(cellCoordinate, fenceTiles);
            groundTilemap.SetTile(previousTile, null);
        }
        this.heldObject.transform.localPosition = Vector3.up * -0.25f;
        this.heldObject.transform.SetParent(null);
        this.heldObject = null;
    }

    public void PlayerStep()
    {
      // audioSource.pitch = Random.Range(0.5f, 1.25f);
      // audioSource.volume = Random.Range(0.25f, 0.375f);
      // audioSource.PlayOneShot(grassSteps[Random.Range(0, grassSteps.Length)]);
    }

    public void placeFenceRuleTile()
    {
        //calculate the tile we want to place on 
        Vector3 footposition = this.transform.position - new Vector3 { x = 0f, y = -.65f, z = 0f };

        Vector3Int cellCoordinate = fenceTilemap.WorldToCell(footposition);
        //place tile

        fenceTilemap.SetTile(cellCoordinate, fenceTiles);
    }

    public enum HorzMovementDirection {
      East,
      West,
      None
    }

}
