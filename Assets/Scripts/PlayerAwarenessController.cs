// Contributors: Ethan Campbell
// Date Created: 2/9/2025

using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool playerInRange {  get; private set; }
    public Vector2 directionToPlayer { get; private set; }

    [SerializeField]
    private float playerAwarenessDistance;

    private Transform player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayer = player.position - transform.position;
        directionToPlayer = enemyToPlayer.normalized;

        if (enemyToPlayer.magnitude <= playerAwarenessDistance)
        {
            playerInRange = true;
        }
        else 
        {
            playerInRange = false;
        }
    }
}
