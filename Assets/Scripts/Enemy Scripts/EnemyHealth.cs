// Contributions: Ethan Campbell
// Date Created: 3/1/2025

using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject experiencePoint;
    [SerializeField] private int baseXP = 5;
    [SerializeField] private int xpDropCount = 3;
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
        TakeDamage(damage);
        GetComponent<EnemyMovement>().ApplyKnockback(knockback);
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            DropXP();
        }
    }

    private void Die()
    {
        Destroy(gameObject); // Enemy dies
    }

    private void DropXP()
    {
        int playerLevel = FindObjectOfType<ExperienceManager>().GetPlayerLevel();
        int totalXP = Mathf.RoundToInt(baseXP * (1 + 0.1f * playerLevel)); // XP scales with level

        int xpPerDrop = totalXP / xpDropCount; // Distribute XP across multiple dots

        for (int i = 0; i < xpDropCount; i++)
        {
            Vector2 dropPosition = (Vector2)transform.position + Random.insideUnitCircle * 0.5f;
            GameObject xpDot = Instantiate(experiencePoint, dropPosition, Quaternion.identity);
            xpDot.GetComponent<ExperienceOrb>().SetXPValue(xpPerDrop);
        }
    }
}
