// Contributions: Ryan Lebato and Ethan Campbell

using System;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHealth enemy = collision.collider.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            enemy.OnHit(StatsManager.Instance.rangedDamage, knockbackDirection);
        }

        Animate();
        Destroy(gameObject);
    }
    void Animate()
    {
        anim.SetTrigger("Impact");
    }
}
