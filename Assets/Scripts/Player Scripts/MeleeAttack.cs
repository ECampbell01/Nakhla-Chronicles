// Contributions: Ethan Campbell
// Date Created: 3/1/2025

using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float knockbackForce = 5f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            enemy.OnHit(StatsManager.Instance.meleeDamage, knockbackDirection * knockbackForce);
        }
    }
}
