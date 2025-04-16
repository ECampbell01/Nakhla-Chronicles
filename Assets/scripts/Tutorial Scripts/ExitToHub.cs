using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToHub : MonoBehaviour
{
    [SerializeField] private string sceneName = "Hubworld";
    private bool hasExited = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasExited && collision.CompareTag("Player"))
        {
            hasExited = true;
            SceneManager.LoadScene(sceneName);
        }
    }
}
