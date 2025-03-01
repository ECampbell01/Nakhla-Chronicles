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
            enemy.OnHit(15f, Vector2.zero);
        }

        Animate();
        Destroy(gameObject, 0.5f);
    }
    void Animate()
    {
        anim.SetTrigger("Impact");
    }
}
