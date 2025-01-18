// Contributors: Ethan Campbell
// Date Created: 1/18/2025

using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject achievementsScreen;
    public GameObject creditsScreen;
    public GameObject howToPlayScreen;

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
}
