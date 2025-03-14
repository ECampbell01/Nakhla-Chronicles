// Contributions: Ethan Campbell
// Date Created: 3/7/2025

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] AnimationCurve experienceCurve;
    int currentLevel;
    int totalExperience;
    int previousLevelExperience;
    int nextLevelExperience;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider experienceFill;

    void Start()
    {
        currentLevel = 1;
        previousLevelExperience = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelExperience = (int)experienceCurve.Evaluate(currentLevel + 1);
        UpdateUI();
    }

    public void AddExperience(int amount)
    {
        totalExperience += amount;
        CheckForLevelUp();
        UpdateUI();
    }

    void CheckForLevelUp()
    {
        while (totalExperience >= nextLevelExperience)
        {
            currentLevel++;
            UpdateLevel();
        }
    }

    void UpdateLevel()
    {
        previousLevelExperience = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelExperience = (int)experienceCurve.Evaluate(currentLevel + 1);
    }

    void UpdateUI()
    {
        int start = totalExperience - previousLevelExperience;
        int end = nextLevelExperience - previousLevelExperience;

        experienceFill.minValue = 0;
        experienceFill.maxValue = end;
        experienceFill.value = start;

        if (experienceFill.value == 0)
        {
            experienceFill.gameObject.SetActive(false); // Hide the bar if XP is 0
        }
        else
        {
            experienceFill.gameObject.SetActive(true); // Show the bar if XP is greater than 0
        }

        levelText.text = currentLevel.ToString();
    }

    public int GetPlayerLevel()
    {
        return currentLevel;
    }
}