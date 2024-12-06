using System.Collections;
using UnityEngine;

public class MachineMover : MonoBehaviour
{
    public Vector2 movementAmount = new Vector2(2f, 0f); // Amount to move on the X and Y axis
    public float moveDuration = 1f; // Time to move to the target position
    public float waitDuration = 2f; // Time to wait at the target position before returning

    private Vector2 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        while (true)
        {
            // Move to the target position
            yield return StartCoroutine(MoveToPosition(originalPosition + movementAmount, moveDuration));
            
            // Wait at the target position
            yield return new WaitForSeconds(waitDuration);
            
            // Move back to the original position
            yield return StartCoroutine(MoveToPosition(originalPosition, moveDuration));
            
            // Wait again before restarting
            yield return new WaitForSeconds(waitDuration);
        }
    }

    private IEnumerator MoveToPosition(Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolate position based on elapsed time
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the final position is the target position
        transform.position = targetPosition;
    }
}