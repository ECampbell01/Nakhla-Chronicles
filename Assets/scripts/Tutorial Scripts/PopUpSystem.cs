using TMPro;
using UnityEngine;

public class Pop : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;

    string triggerText = "Now get ready for enemies\n" + "\nUse right click to melee\n" +
     "\nUse left click to shoot\n";

    bool hasTriggered = false;
    bool hasSpawned = false;

    public GameObject[] prefabsToSpawn;
    public Transform spawnPoint;

    public PlayerController playerController;

    public void PopUp(string text)
    {
        if (playerController != null)
        {
            playerController.StopMovement();
            playerController.canMove = false;
        }

        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("pop");
    }

    public void Start()
    {
        popUpBox.SetActive(true);
        animator.SetTrigger("pop");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (!hasTriggered)
        {
            hasTriggered = true;
            animator.SetTrigger("pop");
            PopUp(triggerText);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasSpawned)
        {
            foreach (GameObject prefab in prefabsToSpawn)
            {
                if (prefab != null)
                {
                    Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
                    Instantiate(prefab, spawnPos, Quaternion.identity);
                }
            }
            hasSpawned = true;
        }
    }
    public void ClosePopUp()
    {
        AllowMovement();
    }
    public void AllowMovement()
    {
        if (playerController != null)
        {
            playerController.canMove = true;
        }
    }
}
