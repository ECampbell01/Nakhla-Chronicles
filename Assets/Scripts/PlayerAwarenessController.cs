// Contributors: Ethan Campbell
// Date Created: 2/9/2025

using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public float damage = 20f;
    public float knockbackForce = 50f;
    public bool playerInRange {  get; private set; }
    public Vector2 directionToPlayer { get; private set; }

    [SerializeField]
    private float playerAwarenessDistance;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayer = player.position - transform.position;
        directionToPlayer = enemyToPlayer.normalized;

        if (enemyToPlayer.magnitude <= playerAwarenessDistance)
        {
            playerInRange = true;
        }
        else 
        {
            playerInRange = false;
        }
    }

    // Collide with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        DamageableCharacter damageable = collider.GetComponent<DamageableCharacter>();

        if (damageable != null)
        {
            Vector2 direction = (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            damageable.OnHit(damage, knockback);
        }
    }
}
