// Contributions: Ethan Campbell
// Date Created: 4/1/2025

using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public TileBase tile;
    public Sprite image;
    public ItemType type;
    public bool stackable = true;
    public int healAmount = 20; // For Health Potions
    public int defenseBoost = 1; // For Armor
    [TextArea] public string description;
    [SerializeField] PlayerData playerData;

    public enum ItemType 
    {
        Health,
        Tool
    }

    public void Use(GameObject player)
    {
        if (type == ItemType.Health)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(healAmount);
            }
        }
        else if (type == ItemType.Tool)
        {
            playerData.Defense += defenseBoost;
        }
    }
}
