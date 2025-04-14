// Contributions: Ryan Lebato and Ethan Campbell

using System;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    Animator anim;

    public float critMultiplier = 1.5f; // Critical hit deals 1.5x damage

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Animate();
        EnemyHealth enemy = collision.collider.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            int damage = StatsManager.Instance.rangedDamage;

            // Check if shot is a critical hit
            if (IsCriticalHit())
            {
                damage = Mathf.RoundToInt(damage * critMultiplier);
            }
            enemy.OnHit(damage, knockbackDirection);
        }

        Destroy(gameObject);
    }

    void Animate()
    {
        anim.SetTrigger("Impact");
    }

    bool IsCriticalHit()
    {
        int luck = StatsManager.Instance.luck;
        float critChance = luck * 0.01f;

        return UnityEngine.Random.value < critChance;
    }
}