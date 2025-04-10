// Contributions: Ethan Campbell
// Date Created: 3/29/2025

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadScene("Hubworld");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
