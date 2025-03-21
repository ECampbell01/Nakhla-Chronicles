using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] GameObject shopUI;

    void Start()
    {
        shopUI.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(false);
        }
    }

}
