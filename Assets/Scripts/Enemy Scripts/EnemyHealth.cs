// Contributions: Ethan Campbell
// Date Created: 3/1/2025

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject experiencePoint;
    [SerializeField] private int baseXP = 5;
    [SerializeField] private int xpDropCount = 3;
    [SerializeField] private GameObject coin;
    [SerializeField] private float coinDropChance = 0.5f; // 50% chance
    [SerializeField] private int minCoins = 1;
    [SerializeField] private int maxCoins = 3;
    private Rigidbody2D rb;
    public float maxHealth = 50f;
    private float currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        currentHealth = maxHealth;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        TakeDamage(damage);
        GetComponent<EnemyMovement>().ApplyKnockback(knockback);
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            DropXP();
            DropCoins();
        }
    }

    private void Die()
    {

        // Check for boss kill achievement
        if (CompareTag("Boss") && PlayerPrefs.GetInt("Achievement_BossDefeated", 0) == 0)
        {
            PlayerPrefs.SetInt("Achievement_BossDefeated", 1);
            PlayerPrefs.Save();
        }
        // Increment kill count
        int currentKills = PlayerPrefs.GetInt("EnemiesKilled", 0);
        currentKills++;
        PlayerPrefs.SetInt("EnemiesKilled", currentKills);

        // Check for 100 kills achievement
        if (currentKills >= 100 && PlayerPrefs.GetInt("Achievement_Kill100Enemies", 0) == 0)
        {
            PlayerPrefs.SetInt("Achievement_Kill100Enemies", 1);
            PlayerPrefs.Save();
        }

        // Check for 200 kills achievement
        if (currentKills >= 200 && PlayerPrefs.GetInt("Achievement_Kill200Enemies", 0) == 0)
        {
            PlayerPrefs.SetInt("Achievement_Kill200Enemies", 1);
            PlayerPrefs.Save();
        }

        Destroy(gameObject); // Enemy dies
    }

    private void DropXP()
    {
        int playerLevel = FindObjectOfType<ExperienceManager>().GetPlayerLevel();
        int totalXP = Mathf.RoundToInt(baseXP * (1 + 0.1f * playerLevel)); // XP scales with level

        int xpPerDrop = totalXP / xpDropCount; // Distribute XP across multiple dots

        for (int i = 0; i < xpDropCount; i++)
        {
            Vector2 dropPosition = (Vector2)transform.position + Random.insideUnitCircle * 0.5f;
            GameObject xpDot = Instantiate(experiencePoint, dropPosition, Quaternion.identity);
            xpDot.GetComponent<ExperienceOrb>().SetXPValue(xpPerDrop);
        }
    }

    private void DropCoins()
    {
        if (coin == null) return;

        float roll = Random.value;
        if (roll <= coinDropChance)
        {
            int coinCount = Random.Range(minCoins, maxCoins + 1);

            for (int i = 0; i < coinCount; i++)
            {
                Vector2 dropPos = (Vector2)transform.position + Random.insideUnitCircle * 0.3f;
                Instantiate(coin, dropPos, Quaternion.identity);
            }
        }
    }
}
