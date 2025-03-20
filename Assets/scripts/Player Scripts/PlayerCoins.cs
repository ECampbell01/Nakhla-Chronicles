namespace PlayerCoins
{
    using UnityEngine;

    public class Coins : MonoBehaviour
    {
        [SerializeField] public int coinBalance = 1000;

        public bool DeductCoins(int amount)
        {
            if (coinBalance >= amount)
            {
                coinBalance -= amount;
                return true;
            }
            return false;
        }

        public void AddCoins(int amount)
        {
            coinBalance += amount;
        }
    }
}




