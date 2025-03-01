// Contributions: Ethan Campbell
// Date Created: 3/1/2025

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxHealth = 50f;
    private float currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        currentHealth = maxHealth;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        TakeDamage(damage, knockback);
    }

    private void TakeDamage(float damage, Vector2 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject); // Enemy dies
    }
}
