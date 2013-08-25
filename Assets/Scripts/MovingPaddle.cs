using UnityEngine;
using System.Collections;

public class MovingPaddle : MonoBehaviour
{
    public float minMovementTime = 1.5f;
    public float maxMovementTime = 4;
    public float minMovementRange = 1;
    public float maxMovementRange = 3;

    private float movementTime;
    private float movementRange;

    private float timer = 0;
    private int animationDirection = 1;
    private Vector3 basePosition;

    private void Start()
    {
        basePosition = transform.position;

        movementTime = Random.Range(minMovementTime, maxMovementTime);
        movementRange = Random.Range(minMovementRange, maxMovementRange);
    }

    private void Update()
    {
        transform.position = basePosition + Vector3.Lerp(Vector3.left * movementRange, Vector3.right * movementRange, timer);
        timer += animationDirection * (Time.deltaTime/movementTime);

        if (Mathf.Abs(timer) >= 1 || timer <= 0)
        {
            animationDirection *= -1;
        }
    }
}
