using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0, 5, -7);
    public float smoothSpeed = 0.125f; 

    void LateUpdate() 
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform not assigned to Camera Follow script.");
            return;
        }
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;        
        transform.LookAt(playerTransform);
    }
}
