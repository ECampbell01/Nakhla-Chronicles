// Contributions: Ethan Campbell
// Date Created: 3/1/2025

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private Rigidbody2D rb;
    private float currentHealth;
    public HealthBar healthBar;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private InventoryData inventoryData;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        currentHealth = playerData.HP;

        if (healthBar == null)
        {
            healthBar = FindObjectOfType<HealthBar>();
        }

        healthBar.SetMaxHealth(playerData.HP);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        float reducedDamage = ApplyDefense(damage);
        TakeDamage(reducedDamage, knockback);
    }

    private float ApplyDefense(float damage)
    {
        int defense = playerData.Defense;
        float finalDamage = damage / (1 + (defense / 10f));
        return finalDamage;
    }

    private void TakeDamage(float damage, Vector2 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > StatsManager.Instance.HP)
            currentHealth = StatsManager.Instance.HP;

        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        playerData.HP = 100;
        playerData.Agility = 2;
        playerData.Defense = 0;
        playerData.Luck = 1;
        playerData.MeleeDamage = 10;
        playerData.RangedDamage = 10;
        playerData.Level = 1;
        playerData.Experience = 0;
        playerData.AvailablePoints = 0;
        playerData.CompanionPrefab = null;
        inventoryData.ClearInventory();
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}