// Contributions: Ethan Campbell
// Date Created: 3/1/2025

using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float knockbackForce = 5f;
    public float critMultiplier = 1.5f; // Critical hit deals 1.5x damage
    [SerializeField] PlayerData playerData;

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            int damage = playerData.MeleeDamage;

            // Determine if the hit is a critical hit
            if (IsCriticalHit())
            {
                damage = Mathf.RoundToInt(damage * critMultiplier);
            }

            enemy.OnHit(damage, knockbackDirection * knockbackForce);
        }
    }

    bool IsCriticalHit()
    {
        int luck = playerData.Luck;
        float critChance = luck * 0.01f;

        return Random.value < critChance;
    }
}
