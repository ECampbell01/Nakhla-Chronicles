using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayerCoins;

public class PetShopScroll : MonoBehaviour
{
    [System.Serializable]
    class ShopItem
    {
        public string petName;
        public Sprite petImage;
        public int petPrice;
    }

    [SerializeField] private List<ShopItem> ShopItemsList;
    [SerializeField] private Coins coins;
    [SerializeField] private TextMeshProUGUI coinBalanceText;
    [SerializeField] private Transform ShopScrollView;
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI popupMessageText;

    private GameObject itemTemplate;

    void Start()
    {
        if (ShopScrollView.childCount == 0) return;

        itemTemplate = ShopScrollView.GetChild(0).gameObject;
        itemTemplate.SetActive(false);
        popupPanel.SetActive(true);

        PopulateShop();
        UpdateCoinBalance();

        Destroy(itemTemplate);
    }

    private void PopulateShop()
    {
        if (ShopItemsList == null || ShopItemsList.Count == 0) return;

        foreach (var item in ShopItemsList)
        {
            GameObject newItem = Instantiate(itemTemplate, ShopScrollView);
            newItem.SetActive(true);

            // Assign item details
            newItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.petName;
            newItem.transform.GetChild(1).GetComponent<Image>().sprite = item.petImage;
            newItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.petPrice.ToString();

            // Assign Buy Button event
            Button buyBtn = newItem.transform.GetChild(4).GetComponent<Button>();
            if (buyBtn != null)
            {
                buyBtn.onClick.AddListener(() => OnBuyButtonClicked(item));
            }
        }
    }

    private void OnBuyButtonClicked(ShopItem item)
    {
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

    private void UpdateCoinBalance()
    {
        coinBalanceText.text = coins.coinBalance.ToString();
    }

    private void ShowPopup(string message, Color textColor)
    {
        popupMessageText.text = message;
        popupMessageText.color = textColor;
        popupPanel.SetActive(true);
        StartCoroutine(HidePopupAfterDelay(2f));
    }

    private IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        popupPanel.SetActive(false);
    }
}
