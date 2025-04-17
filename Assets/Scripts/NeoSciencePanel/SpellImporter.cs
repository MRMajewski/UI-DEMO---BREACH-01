using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SpellImporter : EditorWindow
{
    [MenuItem("Tools/Import Spells from JSON (manual file)")]
    public static void ImportSpells()
    {
        string path = EditorUtility.OpenFilePanel("Select spells JSON file", "", "json");

        if (string.IsNullOrEmpty(path))
        {
            Debug.Log("Import canceled.");
            return;
        }

        string json = File.ReadAllText(path);
        SpellList spellList = JsonUtility.FromJson<SpellList>(json);

        string folderPath = "Assets/Scripts/NeoSciencePanel/NeoScienceScriptables/Neophysics";
        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets/Scripts/NeoSciencePanel/NeoScienceScriptables", "Neophysics");

        foreach (SpellEntry entry in spellList.spells)
        {
            // Tworzymy instancjê NeophysicsSpellData, a nie SpellData
            NeophysicsSpellData spell = ScriptableObject.CreateInstance<NeophysicsSpellData>();

            spell.spellName = entry.name;
            spell.spellLevel = entry.level;
            spell.description = entry.description;
            spell.spellType = NeoPhysicsCategoryFromString(entry.type);
            spell.spellCastingTime = entry.castingTime;
            spell.spellRange = entry.range;

            string assetName = entry.name.Replace(" ", "_").Replace("/", "_");
            string assetPath = $"{folderPath}/{assetName}.asset";

            // Logowanie informacji o tworzeniu obiektu
            Debug.Log($"Creating asset: {assetName} at path {assetPath}");

            AssetDatabase.CreateAsset(spell, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Successfully imported {spellList.spells.Count} spells from: {path}");
    }

    [System.Serializable]
    private class SpellEntry
    {
        public string name;
        public int level;
        public string type;
        public string castingTime;
        public string range;
        public string description;
    }

    [System.Serializable]
    private class SpellList
    {
        public List<SpellEntry> spells;
    }

    private static NeoPhysicsCategory NeoPhysicsCategoryFromString(string type)
    {
        foreach (NeoPhysicsCategory cat in System.Enum.GetValues(typeof(NeoPhysicsCategory)))
        {
            if (cat.ToString().ToLower() == type.ToLower())
                return cat;
        }

        Debug.LogWarning($"Unknown NeoPhysicsCategory: {type}, using default.");
        return default;
    }
}
