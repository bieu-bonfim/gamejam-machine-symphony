using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform startPosition;
    public GameObject deathUI;
    public AudioClip deathSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die() {
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath() {
        deathUI.SetActive(true);
        audioSource.PlayOneShot(deathSound);
        yield return new WaitForSeconds(2);
    }

    public void Restart() {
        transform.position = startPosition.position;
        deathUI.SetActive(false);
    }
}
