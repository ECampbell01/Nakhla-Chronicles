// Contributions: Ethan Campbell
// Date Created: 2/22/2025

using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private float shootingRange = 5f;

    private PlayerAwarenessController playerAwareness;
    private float nextFireTime;

    private void Awake()
    {
        playerAwareness = GetComponent<PlayerAwarenessController>();
    }

    private void Update()
    {
        if (playerAwareness.playerInRange && Time.time >= nextFireTime)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerAwareness.transform.position);
            if (distanceToPlayer <= shootingRange)
            {
                Shoot();
                nextFireTime = Time.time + fireRate; // Set next fire time
            }
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, aimPoint.position, Quaternion.identity);
        RangedEnemyProjectile projectileScript = projectile.GetComponent<RangedEnemyProjectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(playerAwareness.directionToPlayer);
        }
    }
}