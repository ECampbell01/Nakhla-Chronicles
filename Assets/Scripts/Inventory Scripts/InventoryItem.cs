// Contributions: Ethan Campbell
// Date Created: 4/1/2025

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Text countText;
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;
    private CameraSwitcher cameraSwitcher;

    void Start()
    {
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
    }

    public void InitializeItem(Item newItem) 
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount() 
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
    public void OnBeginDrag(PointerEventData eventData) 
    {
        // Don't allow drag and drop outside of pause menu
        if (!cameraSwitcher.isPauseMenuActive) return;
        image.raycastTarget = false;
        countText.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Don't allow drag and drop outside of pause menu
        if (!cameraSwitcher.isPauseMenuActive) return;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Don't allow drag and drop outside of pause menu
        if (!cameraSwitcher.isPauseMenuActive) return;
        image.raycastTarget = true;
        countText.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
