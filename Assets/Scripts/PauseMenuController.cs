// Contributions: Ethan Campbell
// Date Created: 3/18/2025

using TMPro;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    private CameraSwitcher cameraSwitcher;

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
}
