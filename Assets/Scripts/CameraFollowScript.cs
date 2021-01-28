using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float cameraZoom = 5.0f;
    public Vector3 offset;
    public PlayerOne playerScript;
    public Camera mainCamera;
    public float zoomScale = 10f;

    void FixedUpdate()
    {
        // Follow the player
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Zoom the camera based on the player speed
        float desiredZoom = cameraZoom + (playerScript.playerSpeed * zoomScale);
        float smoothedZoom = Mathf.Lerp(mainCamera.orthographicSize, desiredZoom, smoothSpeed / 6f);
        mainCamera.orthographicSize = smoothedZoom;
    }
}
