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
    private CameraSwitcher cameraSwitcher;
    public ExperienceManager experienceManager;

    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI agilityText;
    [SerializeField] TextMeshProUGUI defenseText;
    [SerializeField] TextMeshProUGUI luckText;
    [SerializeField] TextMeshProUGUI meleeText;
    [SerializeField] TextMeshProUGUI rangedText;

    void Start()
    {
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        cameraSwitcher.isPauseMenuActive = false;
        Time.timeScale = 1; // Resume the game
    }

    public void UpdateStatsUI()
    {
        if (StatsManager.Instance != null)
        {
            hpText.text = "HP: " + StatsManager.Instance.HP;
            agilityText.text = "Agility: " + StatsManager.Instance.agility;
            defenseText.text = "Defense: " + StatsManager.Instance.defense;
            luckText.text = "Luck: " + StatsManager.Instance.luck;
            meleeText.text = "Melee: " + StatsManager.Instance.meleeDamage;
            rangedText.text = "Ranged: " + StatsManager.Instance.rangedDamage;
        }
    }

    public void UpgradeHP()
    {
        if (experienceManager.HasAvailablePoints())
        {
            StatsManager.Instance.HP += 5; // Increase HP
            experienceManager.SpendPoint();
            UpdateStatsUI();
            UpdateButtonStates();
        }
    }

    public void UpgradeAgility()
    {
        if (experienceManager.HasAvailablePoints())
        {
            StatsManager.Instance.agility += 1; // Increase agility
            experienceManager.SpendPoint();
            UpdateStatsUI();
            UpdateButtonStates();
        }
    }

    public void UpgradeDefense()
    {
        if (experienceManager.HasAvailablePoints())
        {
            StatsManager.Instance.defense += 1; // Increase defense
            experienceManager.SpendPoint();
            UpdateStatsUI();
            UpdateButtonStates();
        }
    }

    public void UpgradeLuck()
    {
        if (experienceManager.HasAvailablePoints())
        {
            StatsManager.Instance.luck += 1; // Increase luck
            experienceManager.SpendPoint();
            UpdateStatsUI();
            UpdateButtonStates();
        }
    }

    public void UpgradeMeleeDamage()
    {
        if (experienceManager.HasAvailablePoints())
        {
            StatsManager.Instance.meleeDamage += 2; // Increase melee damage
            experienceManager.SpendPoint();
            UpdateStatsUI();
            UpdateButtonStates();
        }
    }

    public void UpgradeRangedDamage()
    {
        if (experienceManager.HasAvailablePoints())
        {
            StatsManager.Instance.rangedDamage += 2; // Increase ranged damage
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
}
