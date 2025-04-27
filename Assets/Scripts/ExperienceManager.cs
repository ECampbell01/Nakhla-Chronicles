// Contributions: Ethan Campbell
// Date Created: 3/7/2025

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] AnimationCurve experienceCurve;
    //int currentLevel;
    //int totalExperience;
    int previousLevelExperience;
    int nextLevelExperience;
    //int availablePoints = 0;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI availablePointsText;
    [SerializeField] Slider experienceFill;
    [SerializeField] PlayerData playerData;

    void Start()
    {
        previousLevelExperience = (int)experienceCurve.Evaluate(playerData.Level);
        nextLevelExperience = (int)experienceCurve.Evaluate(playerData.Level + 1);
        UpdateUI();
    }

    public void AddExperience(int amount)
    {
        playerData.Experience += amount;
        CheckForLevelUp();
        UpdateUI();
    }

    void CheckForLevelUp()
    {
        while (playerData.Experience >= nextLevelExperience)
        {
            playerData.Level++;
            playerData.AvailablePoints++;
            UpdateLevel();
        }
    }

    void UpdateLevel()
    {
        previousLevelExperience = (int)experienceCurve.Evaluate(playerData.Level);
        nextLevelExperience = (int)experienceCurve.Evaluate(playerData.Level + 1);
    }

    void UpdateUI()
    {
        int start = playerData.Experience - previousLevelExperience;
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

        levelText.text = playerData.Level.ToString();
        availablePointsText.text = $"Available Points: {playerData.AvailablePoints}";
    }

    public int GetPlayerLevel()
    {
        return playerData.Level;
    }

    public bool HasAvailablePoints()
    {
        return playerData.AvailablePoints > 0;
    }

    public void SpendPoint()
    {
        if (playerData.AvailablePoints > 0)
        {
            playerData.AvailablePoints--;
            UpdateUI();
        }
    }
}