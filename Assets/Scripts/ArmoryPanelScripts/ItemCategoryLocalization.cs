using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemCategory
{
    Weapon,
    Armor,
    Accessory,
    Firearm,
    Melee,
    Civilian,
    Military,
    Experimental,
    Pistol,
    SMG,
    Rifle,
    AssaultRifle,
    HeavyWeapon,
    Shotgun,
    Tool,
    Other
}

public class ItemCategoryLocalization : MonoBehaviour
{
    public static readonly Dictionary<ItemCategory, string> ItemCategoryNames = new()
    {
        { ItemCategory.Weapon, "Broñ" },
        { ItemCategory.Armor, "Pancerz" },
        { ItemCategory.Accessory, "Akcesorium" },
        { ItemCategory.Firearm, "Broñ palna" },
        { ItemCategory.Melee, "Broñ bia³a" },
        { ItemCategory.Civilian, "Cywilny" },
        { ItemCategory.Military, "Militarny" },
        { ItemCategory.Experimental, "Eksperymentalny" },
        { ItemCategory.Pistol, "Pistolet" },
        { ItemCategory.SMG, "SMG" },
        { ItemCategory.Rifle, "Karabin" },
        { ItemCategory.AssaultRifle, "Karabin szturmowy" },
        { ItemCategory.HeavyWeapon, "Broñ ciê¿ka" },
        { ItemCategory.Shotgun, "Strzelba" },
        { ItemCategory.Tool, "Narzêdzie" },
        { ItemCategory.Other, "Inne" }
    };

    public static string GetCategoryName(ItemCategory category)
    {
        return ItemCategoryNames.TryGetValue(category, out var name) ? name : category.ToString();
    }
}
