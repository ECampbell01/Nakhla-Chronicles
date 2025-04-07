// contributions: Chance Daigle
// date: 3/23/25

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayerCoins;

public class PetShopScroll : MonoBehaviour
{
    // ------------------- Shop Item Class -------------------
    // Class representing the data for each pet item in the pet shop
    [System.Serializable]
    class ShopItem
    {
        public string petName;
        public Sprite petImage;
        public int petPrice;
    }

    // ------------------- Serialized Fields -------------------
    // Fields to be assigned in the Unity inspector for pet shop management and coin tracking
    [SerializeField] private List<ShopItem> ShopItemsList;
    [SerializeField] private Coins coins;
    [SerializeField] private TextMeshProUGUI coinBalanceText;
    [SerializeField] private Transform ShopScrollView;
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI popupMessageText;

    private GameObject itemTemplate;

    // ------------------- Start Method -------------------
    // Initializes the pet shop by setting up the UI and updating the coin balance
    void Start()
    {
        // Exit early if the ShopScrollView has no child elements (items)
        if (ShopScrollView.childCount == 0) return;

        // Set the item template to the first child in the ShopScrollView and hide it
        itemTemplate = ShopScrollView.GetChild(0).gameObject;
        itemTemplate.SetActive(false);
        popupPanel.SetActive(true);  // Show the popup panel initially

        // Populate the pet shop with items and update the coin balance display
        PopulateShop();
        UpdateCoinBalance();

        // Subscribe to the event that updates the coin balance when it changes
        coins.OnCoinBalanceChanged += UpdateCoinBalance;

        // Destroy the item template as it is no longer needed after instantiation
        Destroy(itemTemplate);
    }

    // ------------------- OnDestroy Method -------------------
    // Unsubscribes from the event when the object is destroyed
    void OnDestroy()
    {
        // Unsubscribe from the coin balance changed event to avoid memory leaks
        coins.OnCoinBalanceChanged -= UpdateCoinBalance;
    }

    // ------------------- PopulateShop Method -------------------
    // Populates the pet shop UI by creating and setting up each pet item
    private void PopulateShop()
    {
        // Exit if the shop has no items
        if (ShopItemsList == null || ShopItemsList.Count == 0) return;

        // Loop through each pet item in the shop list
        foreach (var item in ShopItemsList)
        {
            // Instantiate a new item from the template and make it visible
            GameObject newItem = Instantiate(itemTemplate, ShopScrollView);
            newItem.SetActive(true);

            // Set item details in the UI (name, image, and price)
            newItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.petName;
            newItem.transform.GetChild(1).GetComponent<Image>().sprite = item.petImage;
            newItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.petPrice.ToString();

            // Find the Buy button and add the event listener to handle clicks
            Button buyBtn = newItem.transform.GetChild(4).GetComponent<Button>();
            if (buyBtn != null)
            {
                buyBtn.onClick.AddListener(() => OnBuyButtonClicked(item));
            }
        }
    }

    // ------------------- OnBuyButtonClicked Method -------------------
    // Handles the logic when the "Buy" button is clicked for a pet item
    private void OnBuyButtonClicked(ShopItem item)
    {
        // Attempt to deduct coins from the player. If successful, display a success message
        if (coins.DeductCoins(item.petPrice))
        {
            ShowPopup($"Purchased {item.petName} for {item.petPrice} coins!", Color.green);
            UpdateCoinBalance();  // Update the coin balance display after the purchase
        }
        else
        {
            // If the player doesn't have enough coins, display an error message
            ShowPopup("Not enough coins!", Color.red);
        }
    }

    // ------------------- UpdateCoinBalance Method -------------------
    // Updates the UI to reflect the current coin balance
    private void UpdateCoinBalance()
    {
        coinBalanceText.text = coins.coinBalance.ToString();  // Update the coin balance text on the UI
    }

    // ------------------- ShowPopup Method -------------------
    // Displays a message in the popup UI with the provided message and color
    private void ShowPopup(string message, Color textColor)
    {
        popupMessageText.text = message;  // Set the message text in the popup
        popupMessageText.color = textColor;  // Set the text color based on the message type
        popupPanel.SetActive(true);  // Show the popup panel
        StartCoroutine(HidePopupAfterDelay(2f));  // Hide the popup after a short delay
    }

    // ------------------- HidePopupAfterDelay Coroutine -------------------
    // Coroutine that waits for a specified delay before hiding the popup
    private IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified amount of time
        popupPanel.SetActive(false);  // Hide the popup panel after the delay
    }
}
