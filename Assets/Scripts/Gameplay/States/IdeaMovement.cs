using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeaMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float threshold = 1f;
    private Vector3 startPosition;
    private Vector3 randomDirection;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        PickNewDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the sprite
        transform.position += randomDirection * moveSpeed * Time.deltaTime;

        // Check if the sprite has moved beyond the threshold
        if (Vector3.Distance(startPosition, transform.position) > threshold)
        {
            // Change: Pick a new random direction when the threshold is reached
            PickNewDirection();
            // Optional: Reset the start position if you want the new threshold 
            // to be relative to the current position
            // startPosition = transform.position;
        }
    }
    void PickNewDirection()
    {
        // Pick a random direction
        randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

}
