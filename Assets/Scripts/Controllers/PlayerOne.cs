using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Animator characterAnimator;

    public float playerSpeed = 0f;

    private float maxMovement = .1f;

    private Vector2 moveVector = new Vector2 { x = 0f, y = 0f };
    [Range(0f, 1f)] private float movementScaler = .05f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.RightArrow) && moveVector.x < maxMovement)
        {
            moveVector.x = moveVector.x + movementScaler;

            if(moveVector.x > 2)
            {
                Debug.Log("Warpspeed");
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) && -1*moveVector.x < maxMovement)
        {
            moveVector.x = moveVector.x - movementScaler;
        }
        if (Input.GetKey(KeyCode.UpArrow) && moveVector.y < maxMovement)
        {
            moveVector.y = moveVector.y + movementScaler;
        }
        if (Input.GetKey(KeyCode.DownArrow) && -1*moveVector.y < maxMovement)
        {
            moveVector.y = moveVector.y - movementScaler;
        }

        rigidbody.position = rigidbody.position + moveVector;
        moveVector = moveVector * .75f;

        if(moveVector.magnitude > .1 && moveVector.x > 0)
        {
            characterAnimator.SetBool("MoveRight", true);
        }
        if(moveVector.magnitude > .1 && moveVector.x < 0)
        {
            characterAnimator.SetBool("MoveLeft", true);
        }
        if(moveVector.magnitude < .1)
        {
            characterAnimator.SetBool("MoveRight",false);
            characterAnimator.SetBool("MoveLeft", false);
        }

        // Update the players speed
        playerSpeed = moveVector.magnitude; 

    }





}
