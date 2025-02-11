// Contributions: Ethan Campbell
// Date Created: 2/11/2025

using UnityEngine;

public class DamageableCharacter : MonoBehaviour
{
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
}
