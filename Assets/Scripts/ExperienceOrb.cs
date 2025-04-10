// Contributions: Ethan Campbell
// Date Created: 3/7/2025

using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    private int xpValue;
    private ExperienceManager experienceManager;

    void Start()
    {
        experienceManager = FindObjectOfType<ExperienceManager>();
    }
    public void SetXPValue(int value)
    {
        xpValue = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            experienceManager.AddExperience(xpValue);
            Destroy(gameObject);
        }
    }
}
