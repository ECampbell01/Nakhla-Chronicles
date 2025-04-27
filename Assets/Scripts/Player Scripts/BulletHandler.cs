// Contributions: Ryan Lebato and Ethan Campbell

using System;
using UnityEngine;
using System.Collections;

public class BulletHandler : MonoBehaviour
{
    Animator anim;

    public float critMultiplier = 1.5f; // Critical hit deals 1.5x damage

    [SerializeField] PlayerData playerData;

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
            int damage = playerData.RangedDamage;

            if (IsCriticalHit())
            {
                damage = Mathf.RoundToInt(damage * critMultiplier);
            }
            enemy.OnHit(damage, knockbackDirection);
        }

        // Disable collider so it doesn't trigger more collisions
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        // Start coroutine to destroy after animation finishes
        StartCoroutine(DestroyAfterAnimation());
    }

    void Animate()
    {
        anim.SetTrigger("Impact");
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }


    bool IsCriticalHit()
    {
        float critChance = playerData.Luck * 0.01f;
        return UnityEngine.Random.value < critChance;
    }
}