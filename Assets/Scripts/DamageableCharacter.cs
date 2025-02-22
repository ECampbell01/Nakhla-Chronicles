// Contributions: Ethan Campbell
// Date Created: 2/11/2025

using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableCharacter : MonoBehaviour
{
    private Rigidbody2D rb;
    public float playerHealth = 100f;
    private float currentHealth;
    public HealthBar healthBar;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        currentHealth = playerHealth;
        healthBar.SetMaxHealth(playerHealth);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse);
        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver"); // Load Game Over screen
        }
        else
        {
            Destroy(gameObject); // Destroy enemy on death
        }
    }
}
