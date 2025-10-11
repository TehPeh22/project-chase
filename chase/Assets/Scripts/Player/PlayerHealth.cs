using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currHealth;

    public Health healthUI;
    private SpriteRenderer spriteRenderer;
    public static event Action OnPlayerDied;
    void Start()
    {
        currHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            TakeDamage(enemy.damage);
        }
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthUI.UpdateHeart(currHealth);

        // Flash red 
        StartCoroutine(FlashRed());

        if (currHealth <= 0)
        {
            OnPlayerDied.Invoke();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.2f);
        spriteRenderer.color = Color.white;
    }

    public void ResetHealth()
    {
        currHealth = maxHealth;
        // Debug.Log("Health reset to " + currHealth);
        healthUI.UpdateHeart(currHealth);
    }
}
