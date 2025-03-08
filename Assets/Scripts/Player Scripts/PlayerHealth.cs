// Contributions: Ethan Campbell
// Date Created: 3/1/2025

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private Rigidbody2D rb;
    private float currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        currentHealth = StatsManager.Instance.HP;

        if (healthBar == null)
        {
            healthBar = FindObjectOfType<HealthBar>();
        }

        healthBar.SetMaxHealth(StatsManager.Instance.HP);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        TakeDamage(damage, knockback);
    }

    private void TakeDamage(float damage, Vector2 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("GameOver"); // Load Game Over screen
    }
}