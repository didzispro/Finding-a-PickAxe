using UnityEngine;

public class Platform : MonoBehaviour
{
    [Space(5)]
    public float moveDistance = 5f; 
    [Space(10)]
    public float moveSpeed = 2f;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool shouldGoDown = false;
    
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + Vector3.down * moveDistance;
    }

    void Update()
    {
        MoveTowards();
    }

    void MoveTowards()
    {
        if (shouldGoDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);
        }
    }

    public void GoDown()
    {
        shouldGoDown = true;
    }
}