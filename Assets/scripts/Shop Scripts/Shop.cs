// contributions: Chance Daigle
// date: 3/23/25

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayerCoins;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ShopScroll : MonoBehaviour
{
    // ------------------- Shop Item Class -------------------
    // Class representing the data for each item in the shop
    [System.Serializable]
    class ShopItem
    {
        public string itemName;
        public Sprite itemImage;
        public int itemPrice;
        public Item itemToGive;
    }

    // ------------------- Serialized Fields -------------------
    // Fields to be assigned in the Unity inspector for shop and coin management
    [SerializeField] private List<ShopItem> ShopItemsList;
    [SerializeField] private Coins coins;
    [SerializeField] private TextMeshProUGUI coinBalanceText;
    [SerializeField] private Transform ShopScrollView;
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI popupMessageText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;

    private GameObject itemTemplate;
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    // ------------------- Start Method -------------------
    // This method runs when the script is first initialized, setting up the shop
    void Start()
    {
        // Check if the ShopScrollView has any child elements (items)
        if (ShopScrollView.childCount == 0) return;

        // Set the item template to the first child in the ShopScrollView and hide it
        itemTemplate = ShopScrollView.GetChild(0).gameObject;
        itemTemplate.SetActive(false);
        popupPanel.SetActive(true);  // Show the popup panel initially

        // Populate the shop with items and update the coin balance display
        PopulateShop();
        UpdateCoinBalance();

        // Subscribe to the event that updates the coin balance
        coins.OnCoinBalanceChanged += UpdateCoinBalance;

        // Destroy the item template as it is no longer needed
        Destroy(itemTemplate);
    }

    // ------------------- OnDestroy Method -------------------
    // This method is called when the object is destroyed. It unsubscribes from events.
    void OnDestroy()
    {
        // Unsubscribe from the coin balance changed event to avoid memory leaks
        coins.OnCoinBalanceChanged -= UpdateCoinBalance;
    }

    // ------------------- PopulateShop Method -------------------
    // This method instantiates and sets up each shop item in the UI from the list
    private void PopulateShop()
    {
        // Exit if there are no items in the shop
        if (ShopItemsList == null || ShopItemsList.Count == 0) return;

        // Loop through each item in the shop list
        foreach (var item in ShopItemsList)
        {
            // Instantiate a new item from the template and make it visible
            GameObject newItem = Instantiate(itemTemplate, ShopScrollView);
            newItem.SetActive(true);

            // Set item details in the UI (name, image, and price)
            newItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.itemName;
            newItem.transform.GetChild(1).GetComponent<Image>().sprite = item.itemImage;
            newItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.itemPrice.ToString();

            // Find the Buy button and add the event listener to handle clicks
            Button buyBtn = newItem.transform.GetChild(4).GetComponent<Button>();
            if (buyBtn != null)
            {
                buyBtn.onClick.AddListener(() => OnBuyButtonClicked(item));
            }

            // Add EventTrigger for hover events
            EventTrigger trigger = newItem.AddComponent<EventTrigger>();

            // On pointer enter: show item description
            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((eventData) => {
                itemDescriptionText.text = item.itemToGive.description;
            });

            // On pointer exit: clear item description
            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((eventData) => {
                itemDescriptionText.text = "";
            });

            trigger.triggers.Add(entryEnter);
            trigger.triggers.Add(entryExit);
        }
    }

    // ------------------- OnBuyButtonClicked Method -------------------
    // This method handles purchasing an item when the "Buy" button is clicked
    private void OnBuyButtonClicked(ShopItem item)
    {
        // Attempt to deduct coins from the player. If successful, display success message
        if (coins.DeductCoins(item.itemPrice))
        {
            ShowPopup($"Purchased {item.itemName} for {item.itemPrice} coins!", Color.green);
            UpdateCoinBalance();  // Update coin balance after purchase
            bool added = inventoryManager.AddItem(item.itemToGive);
            if (!added)
            {
                ShowPopup("Inventory full!", Color.red);
            }
        }
        else
        {
            // If not enough coins, show error message
            ShowPopup("Not enough coins!", Color.red);
        }
    }

    // ------------------- UpdateCoinBalance Method -------------------
    // This method updates the UI to show the current coin balance
    private void UpdateCoinBalance()
    {
        coinBalanceText.text = coins.playerData.Coins.ToString();  // Set coin balance UI text
    }

    // ------------------- ShowPopup Method -------------------
    // This method displays a message in the popup UI with a specified color
    private void ShowPopup(string message, Color textColor)
    {
        popupMessageText.text = message;  // Set the message text
        popupMessageText.color = textColor;  // Set the text color
        popupPanel.SetActive(true);  // Show the popup
        StartCoroutine(HidePopupAfterDelay(2f));  // Hide the popup after a delay
    }

    // ------------------- HidePopupAfterDelay Coroutine -------------------
    // Coroutine to hide the popup after a certain delay (e.g., 2 seconds)
    private IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay
        popupPanel.SetActive(false);  // Hide the popup panel
    }
}
