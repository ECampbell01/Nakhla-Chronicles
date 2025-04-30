using UnityEngine;

public class HubworldStartScript : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerData.CompanionPrefab != null)
        {
            GameObject companion = Instantiate(playerData.CompanionPrefab, player.transform);
            CompanionMovement movementScript = companion.GetComponent<CompanionMovement>();
            if (movementScript != null)
            {
                movementScript.playerTransform = player.transform;
                movementScript.playerAnimator = player.GetComponent<Animator>();
            }
        }
    }
}
