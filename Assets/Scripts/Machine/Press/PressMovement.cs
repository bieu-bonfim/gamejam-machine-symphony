using UnityEngine;
using System.Collections;

public class PressMovement : MonoBehaviour, IMovementControl
{
    [SerializeField]
    private float movementTime = 0.5f;

    private Transform initialPosition;

    [SerializeField]
    private Vector2 distance;

    private bool isMoving = false;

    void Start()
    {
        initialPosition = transform;        
    }

    public void Move() {
        StartCoroutine(SmoothMove());
    }

    private IEnumerator SmoothMove()
    {
        isMoving = true;

        // Calculate the target position
        Vector2 targetPosition = new Vector2(initialPosition.position.x, initialPosition.position.y + distance.y);

        // Time-based movement (smooth transition)
        float elapsedTime = 0f;
        while (elapsedTime < movementTime)
        {
            transform.position = Vector2.Lerp(initialPosition.position, targetPosition, elapsedTime / movementTime);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Final position to avoid any potential floating point inaccuracies
        transform.position = targetPosition;

        // Optionally, you can reverse the movement here if you want a back-and-forth animation, or call another function
        yield return new WaitForSeconds(0.5f); // Small pause before moving back (if needed)

        // Reset position or move back (based on your logic)
        targetPosition = initialPosition.position;
        elapsedTime = 0f;
        while (elapsedTime < movementTime)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, elapsedTime / movementTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition.position; // Ensure the final position is set back to the start
        isMoving = false;
    }
}
