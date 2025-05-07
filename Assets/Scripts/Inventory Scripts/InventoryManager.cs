// Contributions: Ethan Campbell
// Date Created: 4/13/2025

using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 1;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    int selectedSlot = -1;
    public InventoryData inventoryData;

    public static InventoryManager Instance;

    private void Awake()
    {
        if (inventoryData == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
        LoadInventory();
    }

    private void Update()
    {
        if(Input.inputString != null) 
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 8) 
            {
                ChangeSelectedSlot(number - 1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                UseSelectedItem();
            }
        }
    }

    void ChangeSelectedSlot(int newValue) 
    {
        if (selectedSlot >= 0) 
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public void LoadInventory()
    {
        // Clear all inventory slots
        foreach (var slot in inventorySlots)
        {
            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
        }

        // Reload items from saved data
        foreach (var entry in inventoryData.items)
        {
            int remainingCount = entry.count;

            while (remainingCount > 0)
            {
                int stackToAdd = Mathf.Min(remainingCount, maxStackedItems);

                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    InventorySlot slot = inventorySlots[i];
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                    if (itemInSlot == null)
                    {
                        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
                        InventoryItem newItem = newItemGO.GetComponent<InventoryItem>();
                        newItem.InitializeItem(entry.item);
                        newItem.count = stackToAdd;
                        newItem.RefreshCount();

                        remainingCount -= stackToAdd;
                        break;
                    }
                }
            }
        }
    }

    public bool AddItem(Item item)
    {
        // Check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // Find an empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot) 
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
        inventoryData.AddItem(item);
    }

    public Item GetSelectedItem(bool use) 
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null) 
        {
            Item item = itemInSlot.item;
            if(use == true) 
            {
                itemInSlot.count--;
                if(itemInSlot.count <= 0) 
                {
                    Destroy(itemInSlot.gameObject);
                }
                else 
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }

    void UseSelectedItem()
    {
        Item item = GetSelectedItem(true);
        if (item != null)
        {
            item.Use(GameObject.FindWithTag("Player"));
            inventoryData.RemoveItem(item);
        }
    }
}
