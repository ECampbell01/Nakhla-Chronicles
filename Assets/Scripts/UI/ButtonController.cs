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

    private void Start()
    {
        // Later will check if the player has a saved game or not. If the player has a saved
        // game the button will be enabled, if the player doesn't have a saved game it will
        // be disabled.
        continueButton.interactable = false;
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

    public void LoadPrologue()
    {
        SceneManager.LoadScene("Prologue");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
