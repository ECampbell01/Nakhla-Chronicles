// contributions: Chance Daigle
// date: 3/23/25

namespace PlayerCoins
{
    using System;
    using UnityEngine;

    public class Coins : MonoBehaviour
    {
        [SerializeField]
        public PlayerData playerData;
        public event Action OnCoinBalanceChanged;

        public bool DeductCoins(int amount)
        {
            if (playerData.Coins >= amount)
            {
                playerData.Coins -= amount;
                OnCoinBalanceChanged?.Invoke();
                return true;
            }
            return false;
        }

        public void AddCoins(int amount)
        {
            playerData.Coins += amount;
            OnCoinBalanceChanged?.Invoke();
        }
    }
}




