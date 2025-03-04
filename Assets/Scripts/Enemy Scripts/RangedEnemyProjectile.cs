// Contributions: Ethan Campbell
// Date Created: 2/22/2025

using UnityEngine;

public class RangedEnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float knockbackForce = 20f;
    [SerializeField] private float lifetime = 3f;

    private Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Start()
    {
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth damageable = collision.GetComponent<PlayerHealth>();
        if (damageable != null)
        {
            Vector2 knockback = direction * knockbackForce;
            damageable.OnHit(damage, knockback);
            Destroy(gameObject); // Destroy projectile on hit
        }
    }
}