using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicSpin : MonoBehaviour
{
    public float rotationSpeed = 5f; // Speed of rotation
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 movementDirection = transform.position - lastPosition;
        if (movementDirection != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

            // Interpolate to the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        lastPosition = transform.position; // Update lastPosition for the next frame
    }
}
