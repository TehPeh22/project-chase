using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Respawn playerRespawn = collision.GetComponent<Respawn>();
            if (playerRespawn != null)
            {
                playerRespawn.SetCheckpoint(transform);
            }
        }
    }
}
