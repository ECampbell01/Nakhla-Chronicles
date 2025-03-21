using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayerCoins;
using Unity.VisualScripting;
using System.Collections;

public class PetShopScroll : MonoBehaviour
{
    [System.Serializable]
    class ShopItem
    {
        public string petName;
        public Sprite petImage;
        public int petPrice;
    }

    [SerializeField] List<ShopItem> ShopItemsList;
    [SerializeField] Coins coins;
    [SerializeField] TextMeshProUGUI coinBalanceText;
    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;
    [SerializeField] GameObject popupPanel;
    [SerializeField] TextMeshProUGUI popupMessageText;

    void Start()
    {
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        ItemTemplate.SetActive(false);
        popupPanel.SetActive(true);

        int len = ShopItemsList.Count;

        if (ShopScrollView == null)
        {
            Debug.LogError("🚨 ShopScrollView is NULL! Assign it in the Inspector.");
            return;
        }

        Debug.Log("🛠 ShopScrollView child count: " + ShopScrollView.childCount);

        if (ShopScrollView.childCount == 0)
        {
            Debug.LogError("🚨 ShopScrollView has NO children! Make sure there's an ItemTemplate inside it.");
            return;
        }

        ItemTemplate = ShopScrollView.GetChild(0).gameObject;

        if (ItemTemplate == null)
        {
            Debug.LogError("🚨 ItemTemplate is NULL! Make sure it's inside ShopScrollView.");
            return;
        }

        ItemTemplate.SetActive(false);

        if (ShopItemsList == null || ShopItemsList.Count == 0)
        {
            Debug.LogError("🚨 ShopItemsList is NULL or EMPTY! Add items in the Inspector.");
            return;
        }

        UpdateCoinBalance();

        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.SetActive(true);

            ShopItem currentItem = ShopItemsList[i];

            var itemNameText = g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].petName;
            if (itemNameText == null) Debug.LogError("🚨 TextMeshPro for item name is missing!");
            var itemImage = g.transform.GetChild(1).GetComponent<Image>().sprite = ShopItemsList[i].petImage;
            if (itemImage == null) Debug.LogError("🚨 Image for item image is missing!");
            var itemPriceText = g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].petPrice.ToString();
            if (itemPriceText == null) Debug.LogError("🚨 TextMeshPro for item price is missing!");

            buyBtn = g.transform.GetChild(4).GetComponent<Button>();
            if (buyBtn != null)
            {
                int itemIndex = i;
                buyBtn.onClick.AddListener(() => OnBuyButtonClicked(ShopItemsList[itemIndex]));
            }
            else
            {
                Debug.LogError("🚨 Buy button is missing or null!");
            }
        }

        Destroy(ItemTemplate);
    }

    void OnBuyButtonClicked(ShopItem item)
    {
        // Debug.Log($"Purchased {item.itemName} for {item.itemPrice}!");

        if (coins.DeductCoins(item.petPrice))
        {
            ShowPopup($"Purchased {item.petName} for {item.petPrice} coins!", Color.green);
            UpdateCoinBalance();
        }
        else
        {
            ShowPopup("Not enough coins!", Color.red);
        }
    }

    void UpdateCoinBalance()
    {
        Debug.Log("Current Coin Balance: " + coins.coinBalance);
        coinBalanceText.text = coins.coinBalance.ToString();
    }

    void ShowPopup(string message, Color textColor)
    {
        popupMessageText.text = message;
        popupMessageText.color = textColor;
        popupPanel.SetActive(true);

        StartCoroutine(HidePopupAfterDelay(2f));
    }

    IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        popupPanel.SetActive(false);
    }

}
