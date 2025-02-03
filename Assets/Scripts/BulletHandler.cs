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
        Animate();
        Destroy(gameObject, 0.5f);
    }
    void Animate()
    {
        anim.SetTrigger("Impact");
    }
}
