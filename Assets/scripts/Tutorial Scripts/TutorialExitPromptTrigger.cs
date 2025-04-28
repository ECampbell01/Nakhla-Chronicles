using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutorialExitPromptTrigger : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject prompt;
    private bool isMapOpen = false;
    private bool isInventoryOpen = false;
    [SerializeField] PlayerData playerData;

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
            playerData.HP = 100;
            playerData.Agility = 2;
            playerData.Defense = 1;
            playerData.Luck = 1;
            playerData.MeleeDamage = 10;
            playerData.RangedDamage = 10;
            playerData.Experience = 0;
            playerData.Level = 1;
            playerData.AvailablePoints = 0;
            SceneManager.LoadScene("Hubworld");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompt.SetActive(true);
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


