using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Animator bigChungusAnimator;

    private Vector2 moveVector = new Vector2 { x = 0f, y = 0f };
    [Range(0f, 1f)] private float movementScaler = .1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVector.x++;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector.x--;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVector.y++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVector.y--;
        }

        rigidbody.position = rigidbody.position + moveVector * movementScaler;
        //movementScaler = movementScaler + .01f;
        moveVector = moveVector * .05f;
        bigChungusAnimator.SetFloat("Speed",  moveVector.magnitude);
        Debug.Log(moveVector.magnitude.ToString());
        
    }
}
