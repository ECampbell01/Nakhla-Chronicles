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

    public enum ItemType 
    {
        Health,
        Tool
    }
}
