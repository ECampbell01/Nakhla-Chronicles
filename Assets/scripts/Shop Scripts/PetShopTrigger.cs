// contributions: Chance Daigle
// date: 3/23/25

using UnityEngine;

public class PetShopTrigger : MonoBehaviour
{
    [SerializeField] GameObject petshopUI;
    public bool isPetShopOpen { get; private set; } = false;

    void Start()
    {
        petshopUI.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            petshopUI.SetActive(true);
            isPetShopOpen = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (petshopUI != null && other.CompareTag("Player"))
        {
            petshopUI.SetActive(false);
            isPetShopOpen = false;
        }
    }

}
