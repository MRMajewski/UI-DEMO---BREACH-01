using System.Collections.Generic;
using System.Linq;
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
