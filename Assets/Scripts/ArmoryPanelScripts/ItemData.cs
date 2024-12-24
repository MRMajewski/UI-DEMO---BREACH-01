using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;

    [Tooltip("Categories this item belongs to")]
    public ItemCategory[] categories;
}

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Items/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    [Tooltip("List of all items available in the game.")]
    public List<ItemData> AllItems = new List<ItemData>();
}
