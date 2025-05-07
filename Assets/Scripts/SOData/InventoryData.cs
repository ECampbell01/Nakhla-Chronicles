using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Scriptable Objects/InventoryData")]
public class InventoryData : ScriptableObject
{
    [System.Serializable]
    public class ItemEntry
    {
        public Item item;
        public int count;
    }

    public List<ItemEntry> items = new List<ItemEntry>();

    public void ClearInventory()
    {
        items.Clear();
    }

    public void AddItem(Item item)
    {
        items.Add(new ItemEntry { item = item, count = 1 });   
    }

    public void RemoveItem(Item item)
    {
        var existing = items.Find(x => x.item == item);
        if (existing != null)
        {
            existing.count--;
            if (existing.count <= 0)
            {
                items.Remove(existing);
            }
        }
    }
}
