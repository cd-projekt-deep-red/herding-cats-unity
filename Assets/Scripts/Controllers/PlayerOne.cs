using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : PlayerController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 framePlayerMove = new Vector2 { x = 0, y = 0 };
        if (Input.GetKey(KeyCode.RightArrow))
        {
            framePlayerMove.x++;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            framePlayerMove.x--;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            framePlayerMove.y++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            framePlayerMove.y--;
        }
        moveBy(framePlayerMove);
    }
}
