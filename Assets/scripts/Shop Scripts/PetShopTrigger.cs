using UnityEngine;

public class PetShopTrigger : MonoBehaviour
{
    [SerializeField] GameObject petshopUI;

    void Start()
    {
        petshopUI.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            petshopUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (petshopUI != null && other.CompareTag("Player"))
        {
            petshopUI.SetActive(false);
        }
    }

}
