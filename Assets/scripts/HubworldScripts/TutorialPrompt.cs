using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPrompt : MonoBehaviour
{
    public GameObject prompUI;
    private bool playerInRange = false;
    void Start()
    {
        prompUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Tutorial");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompUI.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompUI.SetActive(false);
            playerInRange = false;
        }
    }
}
