using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float moveDistance = 5f;   // How far down
    public float moveSpeed = 2f;      // How fast

    private bool shouldGoDown = false;
    private Vector3 startPos;
    private Vector3 endPos;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + Vector3.down * moveDistance;
    }

    void Update()
    {
        if (shouldGoDown)
        {
            // Move platform down smoothly
            transform.position = Vector3.MoveTowards(
                transform.position, 
                endPos, 
                moveSpeed * Time.deltaTime
            );
        }
    }

    // Called by Coin when collected
    public void GoDown()
    {
        shouldGoDown = true;
    }
}