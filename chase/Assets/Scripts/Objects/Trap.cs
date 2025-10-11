using UnityEngine;

public class Trap : MonoBehaviour
{
    public float bounceForce = 10f;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            HandlePlayerBounce(collision.gameObject);
            DamagePlayer(collision.gameObject);
        }
    }

    private void HandlePlayerBounce(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb)
        {
            // Reset player velocity
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            // Apply bounce force
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }
    }

    private void DamagePlayer(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
