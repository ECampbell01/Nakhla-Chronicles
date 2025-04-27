using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEnterTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName = "Dungeon";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
