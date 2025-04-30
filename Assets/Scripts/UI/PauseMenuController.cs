// Contributions: Ethan Campbell
// Date Created: 3/18/2025

using TMPro;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject hpButton;
    public GameObject agilityButton;
    public GameObject defenseButton;
    public GameObject luckButton;
    public GameObject meleeButton;
    public GameObject rangedButton;
    public GameObject achievementsMenu;
    public GameObject playerStatsMenu;
    public GameObject playerInventory;
    public GameObject playerToolbar;
    public CameraSwitcher cameraSwitcher;
    public ExperienceManager experienceManager;
    public TutorialPromptTrigger promptTrigger;
    public TutorialExitPromptTrigger exitTrigger;

    [SerializeField] private TextMeshProUGUI tutorialAchievementText;
    [SerializeField] private TextMeshProUGUI dungeonAchievementText;
    [SerializeField] private TextMeshProUGUI level10AchievementText;
    [SerializeField] private TextMeshProUGUI level25AchievementText;
    [SerializeField] private TextMeshProUGUI level50AchievementText;

    [SerializeField] PlayerData playerData;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI agilityText;
    [SerializeField] TextMeshProUGUI defenseText;
    [SerializeField] TextMeshProUGUI luckText;
    [SerializeField] TextMeshProUGUI meleeText;
    [SerializeField] TextMeshProUGUI rangedText;


    private void UpdateAchievementStatus()
    {
        bool tutorialComplete = PlayerPrefs.GetInt("Achievement_TutorialComplete", 0) == 1;
        bool dungeonComplete = PlayerPrefs.GetInt("Achievement_DungeonComplete", 0) == 1;
        bool level10Complete = PlayerPrefs.GetInt("Achievement_Level10", 0) == 1;
        bool level25Complete = PlayerPrefs.GetInt("Achievement_Level25", 0) == 1;
        bool level50Complete = PlayerPrefs.GetInt("Achievement_Level50", 0) == 1;

        // Show that the tutorial achievement is complete when it is completed
        if (tutorialComplete)
        {
            tutorialAchievementText.text = "Completed";
        }
        else
        {
            tutorialAchievementText.text = "";
        }

        // Show that the dungeon achievement is complete when it is completed
        if (dungeonComplete)
        {
            dungeonAchievementText.text = "Completed";
        }
        else
        {
            dungeonAchievementText.text = "";
        }

        // Show that the level 10 achievement is complete when it is completed
        if (level10Complete)
        {
            level10AchievementText.text = "Completed";
        }
        else
        {
            level10AchievementText.text = "";
        }

        // Show that the level 25 achievement is complete when it is completed
        if (level25Complete)
        {
            level25AchievementText.text = "Completed";
        }
        else
        {
            level25AchievementText.text = "";
        }

        // Show that the level 50 achievement is complete when it is completed
        if (level50Complete)
        {
            level50AchievementText.text = "Completed";
        }
        else
        {
            level50AchievementText.text = "";
        }
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        playerToolbar.SetActive(true);
        cameraSwitcher.isPauseMenuActive = false;
        Time.timeScale = 1; // Resume the game

        if (promptTrigger != null) 
        {
            promptTrigger.CloseInventory();
        }

        if (exitTrigger != null)
        {
            exitTrigger.CloseInventory();
        }
    }

    public void UpdateStatsUI()
    {
        hpText.text = "HP: " + playerData.HP;
        agilityText.text = "Agility: " + playerData.Agility;
        defenseText.text = "Defense: " + playerData.Defense;
        luckText.text = "Luck: " + playerData.Luck;
        meleeText.text = "Melee: " + playerData.MeleeDamage;
        rangedText.text = "Ranged: " + playerData.RangedDamage;
    }

    public void UpgradeHP()
    {
        if (experienceManager.HasAvailablePoints())
        {
            playerData.HP += 5; // Increase HP
            experienceManager.SpendPoint();
            UpdateStatsUI();
            UpdateButtonStates();
        }
    }

    public void UpgradeAgility()
    {
        if (experienceManager.HasAvailablePoints())
        {
            playerData.Agility += 1; // Increase agility
            experienceManager.SpendPoint();
            UpdateStatsUI();
            UpdateButtonStates();
        }
    }

    public void UpgradeDefense()
    {
        if (experienceManager.HasAvailablePoints())
        {
            playerData.Defense += 1; // Increase defense
            experienceManager.SpendPoint();
            UpdateStatsUI();
            UpdateButtonStates();
        }
    }

    public void UpgradeLuck()
    {
        if (experienceManager.HasAvailablePoints())
        {
            playerData.Luck += 1; // Increase luck
            experienceManager.SpendPoint();
            UpdateStatsUI();
            UpdateButtonStates();
        }
    }

    public void UpgradeMeleeDamage()
    {
        if (experienceManager.HasAvailablePoints())
        {
            playerData.MeleeDamage += 2; // Increase melee damage
            experienceManager.SpendPoint();
            UpdateStatsUI();
            UpdateButtonStates();
        }
    }

    public void UpgradeRangedDamage()
    {
        if (experienceManager.HasAvailablePoints())
        {
            playerData.RangedDamage += 2; // Increase ranged damage
            experienceManager.SpendPoint();
            UpdateStatsUI();
            UpdateButtonStates();
        }
    }
    public void UpdateButtonStates()
    {
        // If the player has no available points to spend, hide the buttons
        bool hasPoints = experienceManager.HasAvailablePoints();

        hpButton.gameObject.SetActive(hasPoints);
        agilityButton.gameObject.SetActive(hasPoints);
        defenseButton.gameObject.SetActive(hasPoints);
        luckButton.gameObject.SetActive(hasPoints);
        meleeButton.gameObject.SetActive(hasPoints);
        rangedButton.gameObject.SetActive(hasPoints);
    }

    public void ShowAchievementsMenu() 
    {
        achievementsMenu.SetActive(true);
        playerStatsMenu.SetActive(false);
        playerInventory.SetActive(false);
        playerToolbar.SetActive(false);
        UpdateAchievementStatus();
    }

    public void ShowPlayerStatsMenu() 
    {
        playerStatsMenu.SetActive(true);
        achievementsMenu.SetActive(false);
        playerInventory.SetActive(false);
        playerToolbar.SetActive(false);
    }

    public void ShowPlayerInventory() 
    {
        playerInventory.SetActive(true);
        achievementsMenu.SetActive(false);
        playerStatsMenu.SetActive(false);
        playerToolbar.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
