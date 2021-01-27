using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody;


    public void moveBy(Vector2 moveAmount)
    {
        rigidbody.MovePosition(rigidbody.position + moveAmount);
    }


   
}
