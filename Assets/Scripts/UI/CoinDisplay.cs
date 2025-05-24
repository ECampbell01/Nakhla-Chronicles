// Contributions: Ethan Campbell
// Date Created: 5/17/2025

using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private PlayerCoins.Coins playerCoins;

    private void Start()
    {
        if (playerCoins != null)
        {
            playerCoins.OnCoinBalanceChanged += UpdateCoinText;
            UpdateCoinText();
        }
    }

    private void OnDestroy()
    {
        if (playerCoins != null)
        {
            playerCoins.OnCoinBalanceChanged -= UpdateCoinText;
        }
    }

    private void UpdateCoinText()
    {
        coinText.text = $"{playerCoins.playerData.Coins}";
    }
}
