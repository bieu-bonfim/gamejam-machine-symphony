using System.Collections;
using UnityEngine;

public class MachineMover : MonoBehaviour
{
    public Vector2 movementAmount = new Vector2(2f, 0f); // Amount to move on the X and Y axis
    public float moveDuration = 1f; // Time to move to the target position
    public float waitDuration = 2f; // Time to wait at the target position before returning
    public AudioSource movementEndSound; // Reference to the AudioSource for the sound
    private bool soundPlayed = false;
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
            yield return StartCoroutine(MoveToPosition(originalPosition + movementAmount, moveDuration, true));

            yield return new WaitForSeconds(waitDuration);

            yield return StartCoroutine(MoveToPosition(originalPosition, moveDuration, false));

            yield return new WaitForSeconds(waitDuration);
        }
    }

    private IEnumerator MoveToPosition(Vector2 targetPosition, float duration, bool playSound)
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (elapsedTime > duration*0.8 && soundPlayed == false && playSound){
                PlaySound();
                soundPlayed = true;
            }
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        soundPlayed = false;
    }

    private void PlaySound()
    {
        movementEndSound.Play();

    }
}