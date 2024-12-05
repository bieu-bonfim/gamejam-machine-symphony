using UnityEngine;

public class PressMovement : MonoBehaviour
{
    public Transform targetTransform;  // The target Transform to move towards (set via the editor)
    public float duration = 1f;        // Total duration of movement (1 second)

    private float startY;              // Store the object's starting Y position
    private float timeElapsed = 0f;    // Track elapsed time

    void Start()
    {
        // Set the start Y position to the current position of the object
        startY = transform.position.y;
    }

    void Update()
    {
        // Calculate the time elapsed relative to the total duration
        timeElapsed += Time.deltaTime;

        // Clamp time elapsed to ensure it doesn't exceed the total duration
        if (timeElapsed > duration)
            timeElapsed = duration;

        // Calculate the custom easing for movement (last 60% takes 30% of the time)
        float t = CalculateCustomEasing(timeElapsed / duration);

        // Move the object along the Y-axis to the target position
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(startY, targetTransform.position.y, t), transform.position.z);
    }

    // Custom easing function
    private float CalculateCustomEasing(float normalizedTime)
    {
        // First 40% of the time (0-0.4s)
        if (normalizedTime <= 0.4f)
        {
            // Linear easing (gradual)
            return normalizedTime / 0.4f;
        }
        else
        {
            // Last 60% of the time (0.4-1s) accelerates the movement
            float easedTime = (normalizedTime - 0.4f) / 0.6f;  // Normalize the remaining time (0-1)
            return 0.4f + Mathf.Pow(easedTime, 3) * 0.6f;  // Accelerate the movement towards the end
        }
    }
}
