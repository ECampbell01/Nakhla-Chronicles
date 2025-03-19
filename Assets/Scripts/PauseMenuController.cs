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
}
