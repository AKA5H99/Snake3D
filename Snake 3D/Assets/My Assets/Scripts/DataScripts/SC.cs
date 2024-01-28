using UnityEngine;
using System.Collections.Generic;

public class SC : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public GameObject bodySegmentPrefab;
    public float bodySegmentGap = 0.1f; // Adjust the gap size as needed
    private float nextMoveTime = 0.0f;
    private Vector3 moveDirection = Vector3.forward;
    private Transform snakeHead;
    private List<Transform> bodySegments = new List<Transform>();

    private void Start()
    {
        snakeHead = transform;
    }

    private void Update()
    {
        if (Time.time >= nextMoveTime)
        {
            MoveSnake();
            nextMoveTime = Time.time + 1 / moveSpeed;
        }
    }

    private void MoveSnake()
    {
        Vector3 newPosition = snakeHead.position + moveDirection;
        snakeHead.position = newPosition;

        for (int i = 0; i < bodySegments.Count; i++)
        {
            Vector3 tempPosition = bodySegments[i].position;
            bodySegments[i].position = newPosition;
            newPosition = tempPosition;
        }
    }

    public void EatFruit()
    {
        Vector3 spawnPosition = (bodySegments.Count > 0) ? bodySegments[bodySegments.Count - 1].position : snakeHead.position;

        Transform newBodySegment = Instantiate(bodySegmentPrefab, spawnPosition - moveDirection * (bodySegmentGap + 1.0f), Quaternion.identity).transform;
        bodySegments.Add(newBodySegment);
    }

    public void ChangeDirection(Vector3 newDirection)
    {
        // Ensure the snake can't reverse direction instantly
        if (newDirection != -moveDirection)
        {
            moveDirection = newDirection;
        }
    }
}
