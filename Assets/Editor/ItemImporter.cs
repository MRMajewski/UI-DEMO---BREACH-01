using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ItemImporter : EditorWindow
{
    [MenuItem("Data Import/Import Items from JSON")]
    public static void ImportSpells()
    {
        string path = EditorUtility.OpenFilePanel("Select Items JSON file", "", "json");

        if (string.IsNullOrEmpty(path)) return;

        string json = File.ReadAllText(path);
        ItemList itemList = JsonUtility.FromJson<ItemList>(json);

        string folderPath = "Assets/Resources/ArmoryScriptables";
        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets/Resources", "ArmoryScriptables");

        foreach (ItemEntry entry in itemList.items)
        {
            ItemData item = ScriptableObject.CreateInstance<ItemData>();

            item.itemName = entry.name;
            item.description = entry.description;
            item.categories=ItemCategoriesFromString(entry.categories);

            string assetName = entry.name.Replace(" ", "_");
            string assetPath = $"{folderPath}/{assetName}.asset";

            AssetDatabase.CreateAsset(item, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"✅ Items: Imported {itemList.items.Count} items.");
    }
    private static ItemCategory[] ItemCategoriesFromString(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return new ItemCategory[0];

        string[] parts = str.Split(',');

        List<ItemCategory> categories = new List<ItemCategory>();

        foreach (var part in parts)
        {
            if (System.Enum.TryParse(part.Trim(), true, out ItemCategory result))
            {
                categories.Add(result);
            }
            else
            {
                Debug.LogWarning($"Could not parse '{part}' to ItemCategory enum.");
            }
        }

        return categories.ToArray();
    }

    [System.Serializable]
    private class ItemEntry
    {
        public string name;
        public string description;
        public string categories;
    }

    [System.Serializable]
    private class ItemList
    {
        public List<ItemEntry> items;
    }
}
