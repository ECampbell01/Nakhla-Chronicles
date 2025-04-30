using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonExitTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName = "Hubworld";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("Achievement_DungeonComplete", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneName);
        }
    }
}
