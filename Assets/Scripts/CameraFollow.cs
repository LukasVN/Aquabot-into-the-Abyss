using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target; // Player transform
    public Vector2 minBounds; // Minimum bounds for camera
    public Vector2 maxBounds; // Maximum bounds for camera
    public float smoothTime = 0.3f; // Smoothing time for camera movement
    private Vector3 velocity = Vector3.zero;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate target position with clamped values
            float targetX = Mathf.Clamp(target.position.x, minBounds.x, maxBounds.x);
            float targetY = Mathf.Clamp(target.position.y, minBounds.y, maxBounds.y);
            Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

            // Smoothly move the camera towards the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
