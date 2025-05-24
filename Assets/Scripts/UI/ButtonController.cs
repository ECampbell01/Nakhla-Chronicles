// Contributors: Ethan Campbell
// Date Created: 1/18/2025

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject achievementsScreen;
    public GameObject creditsScreen;
    public GameObject howToPlayScreen;
    public Button continueButton;
    public InventoryManager inventoryManager;

    [SerializeField] private TextMeshProUGUI tutorialAchievementText;
    [SerializeField] private TextMeshProUGUI dungeonAchievementText;
    [SerializeField] private TextMeshProUGUI level10AchievementText;
    [SerializeField] private TextMeshProUGUI level25AchievementText;
    [SerializeField] private TextMeshProUGUI level50AchievementText;
    [SerializeField] private TextMeshProUGUI kill100AchievementText;
    [SerializeField] private TextMeshProUGUI kill200AchievementText;
    [SerializeField] private TextMeshProUGUI bossAchievementText;

    [SerializeField] PlayerData playerData;
    [SerializeField] InventoryData inventoryData;


    void Start()
    {
        if (PlayerPrefs.HasKey("HasSaveData") && PlayerPrefs.GetInt("HasSaveData") == 1)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("HasSaveData") && PlayerPrefs.GetInt("HasSaveData") == 1)
        {
            SceneManager.LoadScene("Hubworld");
        }
    }

    public void NewGame()
    {
        // Load the character creation menu
        playerData.HP = 100;
        playerData.Agility = 2;
        playerData.Defense = 1;
        playerData.Luck = 1;
        playerData.MeleeDamage = 10;
        playerData.RangedDamage = 10;
        playerData.Level = 1;
        playerData.Experience = 0;
        playerData.AvailablePoints = 0;
        playerData.Coins = 0;
        playerData.CompanionPrefab = null;
        inventoryData.ClearInventory();
        inventoryManager.LoadInventory();
        SceneManager.LoadScene("Prologue");
    }

    private void UpdateAchievementStatus()
    {
        bool tutorialComplete = PlayerPrefs.GetInt("Achievement_TutorialComplete", 0) == 1;
        bool dungeonComplete = PlayerPrefs.GetInt("Achievement_DungeonComplete", 0) == 1;
        bool level10Complete = PlayerPrefs.GetInt("Achievement_Level10", 0) == 1;
        bool level25Complete = PlayerPrefs.GetInt("Achievement_Level25", 0) == 1;
        bool level50Complete = PlayerPrefs.GetInt("Achievement_Level50", 0) == 1;
        bool kill100Complete = PlayerPrefs.GetInt("Achievement_Kill100Enemies", 0) == 1;
        bool kill200Complete = PlayerPrefs.GetInt("Achievement_Kill200Enemies", 0) == 1;
        bool bossComplete = PlayerPrefs.GetInt("Achievement_BossDefeated", 0) == 1;

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

        // Show that the kill 100 enemies achievement is complete when it is completed
        if (kill100Complete)
        {
            kill100AchievementText.text = "Completed";
        }
        else
        {
            kill100AchievementText.text = "";
        }

        // Show that the kill 200 enemies achievement is complete when it is completed
        if (kill200Complete)
        {
            kill200AchievementText.text = "Completed";
        }
        else
        {
            kill200AchievementText.text = "";
        }

        // Show that the boss achievement is complete when it is completed
        if (bossComplete)
        {
            bossAchievementText.text = "Completed";
        }
        else
        {
            bossAchievementText.text = "";
        }
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        achievementsScreen.SetActive(false);
        creditsScreen.SetActive(false);
        howToPlayScreen.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ShowAchievementsScreen()
    {
        UpdateAchievementStatus();
        mainMenu.SetActive(false);
        achievementsScreen.SetActive(true);
    }

    public void ShowCreditsScreen()
    {
        mainMenu.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void ShowHowToPlayScreen()
    {
        mainMenu.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
