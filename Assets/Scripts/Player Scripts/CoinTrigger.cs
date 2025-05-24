// Contributions: Ethan Campbell
// Date Created: 5/24/2025

using PlayerCoins;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CoinTrigger : MonoBehaviour
{
    private Coins coins;
    private int coinAmount = 5;

    void Start()
    {
        coins = FindObjectOfType<Coins>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coins.AddCoins(coinAmount);
            Destroy(gameObject);
        }
    }
}
