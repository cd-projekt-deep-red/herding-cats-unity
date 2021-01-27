using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUtilities
{
    public static Vector2 positionChange(Vector2 velocity, Vector2 accel)
    {
        float speed = .05f;
        velocity = velocity + accel * speed;
        return velocity * speed;
    }
}
