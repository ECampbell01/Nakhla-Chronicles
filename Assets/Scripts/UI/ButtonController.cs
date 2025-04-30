// Contributors: Ethan Campbell
// Date Created: 1/18/2025

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject achievementsScreen;
    public GameObject creditsScreen;
    public GameObject howToPlayScreen;
    public GameObject characterCreationMenu;
    public Button continueButton;

    [SerializeField] PlayerData playerData;

    private void Start()
    {
        // Later will check if the player has a saved game or not. If the player has a saved
        // game the button will be enabled, if the player doesn't have a saved game it will
        // be disabled.
        //continueButton.interactable = false;
    }

    public void ContinueGame()
    {
        // Load the last saved game
        // This is a placeholder. You will need to implement the actual loading logic.
        SceneManager.LoadScene("Hubworld");
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
        playerData.CompanionPrefab = null;
        SceneManager.LoadScene("Prologue");
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

    public void LoadCharacterCreation()
    {
        mainMenu.SetActive(false);
        characterCreationMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
