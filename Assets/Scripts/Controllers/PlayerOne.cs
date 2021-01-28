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

    //private Vector2 moveVector = new Vector2 { x = 0f, y = 0f };
    [Range(0f, 1f)] private float movementScaler = .05f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * 5000;
        print(movement);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rigidbody.AddForce(movement);
        playerSpeed = rigidbody.velocity.magnitude;
        if (playerSpeed > 2 && movement.x > 0)
        {
            characterAnimator.SetBool("MoveRight", true);
            characterAnimator.SetBool("MoveLeft", false);
        }
        if (playerSpeed > 2 && movement.x < 0)
        {
            characterAnimator.SetBool("MoveLeft", true);
            characterAnimator.SetBool("MoveRight", false);
        }
        if (playerSpeed < 2)
        {
            characterAnimator.SetBool("MoveRight", false);
            characterAnimator.SetBool("MoveLeft", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("walking Over physics object");
    }



}
