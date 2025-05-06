using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [Tooltip("List of all items available in the game.")]
    public List<ItemData> AllItems = new List<ItemData>();

#if UNITY_EDITOR
    public void LoadAllFromFolder()
    {
        AllItems.Clear();
        LoadItemsData();
    }

    private void LoadItemsData()
    {
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { "Assets/Editor/Scriptables Datas/ArmoryScriptables" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject itemDataScriptable = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (itemDataScriptable is ItemData)
                AllItems.Add((ItemData)itemDataScriptable);
        }
    }
#endif
}