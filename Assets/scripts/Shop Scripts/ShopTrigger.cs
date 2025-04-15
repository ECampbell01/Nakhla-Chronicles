// contributions: Chance Daigle
// date: 3/23/25

using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    public bool isShopOpen { get; private set; } = false;

    void Start()
    {
        shopUI.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(true);
            isShopOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (shopUI != null && other.CompareTag("Player"))
        {
            shopUI.SetActive(false);
            isShopOpen = false;
        }
    }

}
