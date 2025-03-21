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
        Debug.Log("🚶 Something entered the shop trigger: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the shop trigger");
            shopUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("🏃 Something exited the shop trigger: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the shop trigger");
            shopUI.SetActive(false);
        }
    }

}
