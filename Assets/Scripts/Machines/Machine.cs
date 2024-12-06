using System.Collections;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public AudioClip attackSound;  
    private AudioSource audioSource;
    private PressMovement pressMovement;

    private bool attackTriggered = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pressMovement = GetComponent<PressMovement>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = attackSound;
    }

    public void ActivateMachine()
    {
        // Play sound
        if (audioSource != null)
        {
            audioSource.Play();
        }

        pressMovement.MoveDown();
        attackTriggered = true;
        StartCoroutine(LiftMachine());
        Debug.Log($"{gameObject.name} activated!");
    }

    private IEnumerator LiftMachine()
    {
        // Wait for 1 second (or any other time you choose)
        yield return new WaitForSeconds(1f);

        // Move the press back up after the delay
        pressMovement.MoveUp();
    }
}
