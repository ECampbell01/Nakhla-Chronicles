// contributions: Chance Daigle
// date: 3/23/25

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayerCoins;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

class PetShopScroll : MonoBehaviour
{
    // ------------------- Shop Item Class -------------------
    // Class representing the data for each pet item in the pet shop
    [System.Serializable]
    public class ShopItem
    {
        public string petName;
        public Sprite petImage;
        public int petPrice;
        public GameObject companionPrefab;
        [HideInInspector] public bool isPurchased = false;
        [HideInInspector] public Vector3 originalPosition;
        public int hpBuff = 0;
        public int agilityBuff = 0;
        public int defenseBuff = 0;
        public int luckBuff = 0;
        public int meleeDamageBuff = 0;
        public int rangedDamageBuff = 0;
        public string description;
    }

    // ------------------- Serialized Fields -------------------
    // Fields to be assigned in the Unity inspector for pet shop management and coin tracking
    [SerializeField] private List<ShopItem> ShopItemsList;
    [SerializeField] private Coins coins;
    [SerializeField] private TextMeshProUGUI coinBalanceText;
    [SerializeField] private Transform ShopScrollView;
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI popupMessageText;
    [SerializeField] private Button globalSellButton;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI petDescriptionText;
    private Dictionary<string, GameObject> petNameToUIItem = new Dictionary<string, GameObject>();
    private GameObject companion;




    private GameObject itemTemplate;
    //private GameObject currentSpawnedCompanion = null;
    private ShopItem currentItem = null;

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
        globalSellButton.onClick.AddListener(OnGlobalSellButtonClicked);

        foreach (var item in ShopItemsList)
        {
            if (item.companionPrefab == playerData.CompanionPrefab)
            {
                //item.originalPosition = item.companionPrefab.transform.position;
                currentItem = item;
                currentItem.isPurchased = true;
            }
        }

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
            petNameToUIItem[item.petName] = newItem;

            // Set item details in the UI (name, image, and price)
            newItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.petName;
            newItem.transform.GetChild(1).GetComponent<Image>().sprite = item.petImage;
            newItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.petPrice.ToString();

            // Find the Buy button and add the event listener to handle clicks
            Button buyBtn = newItem.transform.GetChild(4).GetComponent<Button>();
            if (buyBtn != null)
            {
                buyBtn.onClick.AddListener(() => OnBuyButtonClicked(item, buyBtn));
            }

            if (playerData.CompanionPrefab != null)
            {
                globalSellButton.gameObject.SetActive(true);
                buyBtn.interactable = false;
                DisableOtherBuyButtons(item.petName);
            }
            else {
                globalSellButton.gameObject.SetActive(false);
            }

            // Add EventTrigger for hover events
            EventTrigger trigger = newItem.AddComponent<EventTrigger>();

            // On pointer enter: show item description
            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((eventData) => {
                petDescriptionText.text = item.description;
            });

            // On pointer exit: clear item description
            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((eventData) => {
                petDescriptionText.text = "";
            });

            trigger.triggers.Add(entryEnter);
            trigger.triggers.Add(entryExit);
        }
    }

    // ------------------- OnBuyButtonClicked Method -------------------
    // Handles the logic when the "Buy" button is clicked for a pet item
    private void OnBuyButtonClicked(ShopItem item, Button buyBtn)
    {
        if (item.isPurchased)
        {
            ShowPopup("You already own this companion!", Color.yellow);
            return;
        }
        
        if (playerData.CompanionPrefab != null)
        {
            ShowPopup("You can only have one companion at a time!", Color.yellow);
        }

        // Attempt to deduct coins from the player. If successful, display a success message
        if (coins.DeductCoins(item.petPrice))
        {
            item.isPurchased = true;
            playerData.CompanionPrefab = item.companionPrefab;

            if (item.companionPrefab != null && player.transform != null)
            {
                companion = Instantiate(playerData.CompanionPrefab, player.transform);
                companion.transform.localPosition = Vector3.zero;
                currentItem = item;
                ApplyCompanionBuffs(item);

            }
            CompanionMovement movementScript = companion.GetComponent<CompanionMovement>();
            if (movementScript != null)
            {
                movementScript.playerTransform = player.transform;
                movementScript.playerAnimator = player.GetComponent<Animator>();
            }

            ShowPopup($"Purchased {item.petName} for {item.petPrice} coins!", Color.green);
            UpdateCoinBalance();  // Update the coin balance display after the purchase

            globalSellButton.gameObject.SetActive(true);
            buyBtn.interactable = false;
            DisableOtherBuyButtons(item.petName);
        }
        else
        {
            // If the player doesn't have enough coins, display an error message
            ShowPopup("Not enough coins!", Color.red);
        }
    }

    private void ApplyCompanionBuffs(ShopItem item)
    {
        playerData.HP += item.hpBuff;
        playerData.Agility += item.agilityBuff;
        playerData.Defense += item.defenseBuff;
        playerData.Luck += item.luckBuff;
        playerData.MeleeDamage += item.meleeDamageBuff;
        playerData.RangedDamage += item.rangedDamageBuff;
        playerData.CompanionPrefab = item.companionPrefab;
    }

    private void OnGlobalSellButtonClicked()
    {
        if (playerData.CompanionPrefab == null) return;
        // Save info before nulling things out
        int refundAmount = currentItem.petPrice;
        string petName = currentItem.petName;

        RemoveCompanionBuffs(currentItem);

        currentItem.isPurchased = false;

        Destroy(player.transform.GetChild(2).gameObject);
        playerData.CompanionPrefab = null;
        currentItem = null;

        coins.AddCoins(refundAmount);
        ShowPopup($"Sold {petName} for {refundAmount} coins!", Color.white);
        UpdateCoinBalance();
        globalSellButton.gameObject.SetActive(false);

        ResetBuyButtons();  // Reactivate all buy buttons
    }

    private void RemoveCompanionBuffs(ShopItem item)
    {
        playerData.HP -= item.hpBuff;
        playerData.Agility -= item.agilityBuff;
        playerData.Defense -= item.defenseBuff;
        playerData.Luck -= item.luckBuff;
        playerData.MeleeDamage -= item.meleeDamageBuff;
        playerData.RangedDamage -= item.rangedDamageBuff;
        playerData.CompanionPrefab = null;  // Reset the companion prefab to null
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

    private void ResetBuyButtons()
    {
        foreach (var kvp in petNameToUIItem)
        {
            string petName = kvp.Key;
            GameObject uiItem = kvp.Value;

            ShopItem shopItem = ShopItemsList.Find(item => item.petName == petName);

            if (shopItem != null && !shopItem.isPurchased)
            {
                Button buyBtn = uiItem.transform.GetChild(4).GetComponent<Button>();
                if (buyBtn != null)
                {
                    buyBtn.interactable = true;
                }
            }
        }
    }

    private void DisableOtherBuyButtons(string purchasedPetName)
    {
        for (int i = 0; i < ShopScrollView.childCount; i++)
        {
            GameObject item = ShopScrollView.GetChild(i).gameObject;
            if (!item.activeSelf) continue;

            string itemName = item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

            if (itemName != purchasedPetName)
            {
                Button buyBtn = item.transform.GetChild(4).GetComponent<Button>();
                if (buyBtn != null)
                {
                    buyBtn.interactable = false;
                }
            }
        }
    }
}
