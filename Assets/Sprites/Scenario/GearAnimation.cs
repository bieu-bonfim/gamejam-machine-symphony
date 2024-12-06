using System.Collections;
using UnityEngine;

public class GearAnimation : MonoBehaviour
{
    public float interval = 1.0f; // Time between rotations
    public float rotationDuration = 0.5f; // Duration of the smooth rotation
    public AudioClip rotationSound; // Audio clip to play on rotation
    [SerializeField]
    private int rotationAngle = 45;
    public AudioSource movementEndSound;

    private bool isRotating = false;

    private void Start()
    {

        // Start the rhythm logic
        StartCoroutine(RhythmLogic());
    }

    private IEnumerator RhythmLogic()
    {
        while (true)
        {
            // Wait for the interval
            yield return new WaitForSeconds(interval);

            // Trigger smooth rotation
            if (!isRotating)
            {
                StartCoroutine(SmoothRotate(rotationAngle)); // Rotate 45 degrees
                if (movementEndSound != null){
                    movementEndSound.Play();
                }
            }
        }
    }

    private IEnumerator SmoothRotate(float angle)
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, angle);

        float elapsedTime = 0;

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation; // Ensure it finishes exactly at the target rotation
        isRotating = false;
    }
}