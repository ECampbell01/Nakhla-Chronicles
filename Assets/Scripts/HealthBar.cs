// Contributions: Ethan Campbell
// Date Created: 2/15/2025

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public CanvasGroup canvasGroup;

    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;

        if (health <= 0)
        {
            canvasGroup.alpha = 0f;
        }
        else
        {
            canvasGroup.alpha = 1f;
        }
    }
}
