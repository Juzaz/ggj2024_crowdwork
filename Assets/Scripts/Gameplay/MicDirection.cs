using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicDirection : MonoBehaviour
{
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
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        lastPosition = transform.position; // Update lastPosition for the next frame
    }
}
