using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutorialPromptTrigger : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject prompt;
    private bool isMapOpen = false;
    private bool isInventoryOpen = false;

    private void Start()
    {
        prompt.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapOpen = !isMapOpen;

            if (isMapOpen)
            {
                prompt.SetActive(false);
            }
            else if (playerInRange)
            {
                prompt.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isInventoryOpen = !isInventoryOpen;
            if (isInventoryOpen)
            {
                prompt.SetActive(false);
            }
            else if (playerInRange)
            {
                prompt.SetActive(true);
            }
        }

        if (playerInRange && !isMapOpen && !isInventoryOpen && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Tutorial");
        }
    }

    public void CloseInventory()
    {
        isInventoryOpen = false;
        if (playerInRange)
        {
            prompt.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (prompt != null)
            {
                prompt.SetActive(true);
            }
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (prompt != null)
            {
                prompt.SetActive(false);
            }
            playerInRange = false;
        }
    }
}


