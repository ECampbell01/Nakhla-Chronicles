// contributions: Chance Daigle
// date: 3/23/25

namespace PlayerCoins
{
    using System;
    using UnityEngine;

    public class Coins : MonoBehaviour
    {
        [SerializeField] public int coinBalance = 1000;
        public event Action OnCoinBalanceChanged;

        public bool DeductCoins(int amount)
        {
            if (coinBalance >= amount)
            {
                coinBalance -= amount;
                OnCoinBalanceChanged?.Invoke();
                return true;
            }
            return false;
        }

        public void AddCoins(int amount)
        {
            coinBalance += amount;
            OnCoinBalanceChanged?.Invoke();
        }
    }
}




