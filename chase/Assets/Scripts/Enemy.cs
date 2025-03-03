using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    // public GameObject deathEffect;

    // Use public to allow references from another script
    public void TakeDamage(int damage) // Para specify amount of dmg 
    {
        health -= damage;
        Debug.Log("Enemy Current Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
