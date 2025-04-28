using System;
using TMPro;
using UnityEngine;

public class Pop : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;

    string triggerText = "Here come the aliens!\n" + "Aliens drop green XP orbs when they die.\n" + "Pick them up to level up\n";

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
            Vector3 baseSpawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
            float radius = 1f;
            int enemyCount = prefabsToSpawn.Length;

            for (int i = 0; i < enemyCount; i++)
            {
                if (prefabsToSpawn[i] != null)
                {
                    float angle = i * Mathf.PI * 2f / enemyCount;
                    float spawnX = Mathf.Cos(angle) * radius;
                    float spawnY = Mathf.Sin(angle) * radius;

                    Vector3 spawnPos = baseSpawnPos + new Vector3(spawnX, spawnY, 0f);

                    Instantiate(prefabsToSpawn[i], spawnPos, Quaternion.identity);
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
