using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.Die();
        }
        Debug.Log("PlayerKiller: " + collision.name);
    }
}
