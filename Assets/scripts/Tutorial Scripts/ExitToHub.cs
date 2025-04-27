using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToHub : MonoBehaviour
{
    [SerializeField] private string sceneName = "Hubworld";
    private bool hasExited = false;
    [SerializeField] private PlayerData playerData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasExited && collision.CompareTag("Player"))
        {
            hasExited = true;
            playerData.HP = 100;
            playerData.Agility = 2;
            playerData.Defense = 1;
            playerData.Luck = 1;
            playerData.MeleeDamage = 10;
            playerData.RangedDamage = 10;
            playerData.Experience = 0;
            playerData.Level = 1;
            playerData.AvailablePoints = 0;
            SceneManager.LoadScene(sceneName);
        }
    }
}
