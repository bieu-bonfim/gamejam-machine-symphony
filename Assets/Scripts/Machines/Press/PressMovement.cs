using System.Collections;
using UnityEngine;

public class PressMovement : MonoBehaviour
{
    private Vector2 originalPosition;
    public float moveSpeed = 2f; // Speed of the movement
    public float targetYPosition = -5f; // Target Y position to move to

    private void Start()
    {
        // Store the original position of the sprite
        originalPosition = transform.position;
    }

    // Function to move the sprite down to the target Y position
    public void MoveDown()
    {
        StartCoroutine(MoveToTarget(targetYPosition));
    }

    // Function to move the sprite back to the original position
    public void MoveUp()
    {
        StartCoroutine(MoveToTarget(originalPosition.y));
    }

    // Coroutine to move the sprite to a specific Y position smoothly
    private IEnumerator MoveToTarget(float targetY)
    {
        float startY = transform.position.y;
        float timeElapsed = 0f;

        // Move smoothly to the target Y position
        while (Mathf.Abs(transform.position.y - targetY) > 0.01f)
        {
            timeElapsed += Time.deltaTime * moveSpeed;
            float newY = Mathf.Lerp(startY, targetY, timeElapsed);
            transform.position = new Vector2(transform.position.x, newY);
            yield return null;
        }

        // Ensure the sprite reaches the exact target position
        transform.position = new Vector2(transform.position.x, targetY);
    }
}

