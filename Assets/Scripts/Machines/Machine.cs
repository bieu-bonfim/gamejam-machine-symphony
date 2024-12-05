using UnityEngine;

public class Machine : MonoBehaviour
{
    public AudioClip attackSound;  
    public Animator animator;     
    private AudioSource audioSource;

    private bool attackTriggered = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

        // Trigger attack animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
            attackTriggered = true;
        }

        Debug.Log($"{gameObject.name} activated!");
    }
}
