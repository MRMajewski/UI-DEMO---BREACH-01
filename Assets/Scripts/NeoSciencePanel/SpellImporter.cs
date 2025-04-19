using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SpellImporter : EditorWindow
{
    [MenuItem("Tools/Import Spells from JSON (Neophysics)")]
    public static void ImportSpells()
    {
        string path = EditorUtility.OpenFilePanel("Select Neophysics JSON file", "", "json");

        if (string.IsNullOrEmpty(path)) return;

        string json = File.ReadAllText(path);
        NeoPhysicsSpellList spellList = JsonUtility.FromJson<NeoPhysicsSpellList>(json);

        string folderPath = "Assets/Scripts/NeoSciencePanel/NeoScienceScriptables/Neophysics";
        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets/Scripts/NeoSciencePanel/NeoScienceScriptables", "Neophysics");

        foreach (NeoPhysicsSpellEntry entry in spellList.spells)
        {
            NeophysicsSpellData spell = ScriptableObject.CreateInstance<NeophysicsSpellData>();

            spell.spellName = entry.name;
            spell.spellLevel = entry.level;
            spell.spellDescription = entry.description;
            spell.spellType = NeoPhysicsCategoryFromString(entry.type);
            spell.spellCastingTime = entry.castingTime;
            spell.spellRange = entry.range;

            string assetName = entry.name.Replace(" ", "_");
            string assetPath = $"{folderPath}/{assetName}.asset";

            AssetDatabase.CreateAsset(spell, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"✅ Neophysics: Imported {spellList.spells.Count} spells.");
    }

    [MenuItem("Tools/Import Spells from JSON (Psionics)")]
    public static void ImportPsionicsSpells()
    {
        string path = EditorUtility.OpenFilePanel("Select Psionics JSON file", "", "json");
        if (string.IsNullOrEmpty(path)) return;

        string json = File.ReadAllText(path);
        GeneralSpellList spellList = JsonUtility.FromJson<GeneralSpellList>(json);

        string folderPath = "Assets/Scripts/NeoSciencePanel/NeoScienceScriptables/Psionics";
        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets/Scripts/NeoSciencePanel/NeoScienceScriptables", "Psionics");

        foreach (GeneralSpellEntry entry in spellList.spells)
        {
            PsionicsSpellData spell = ScriptableObject.CreateInstance<PsionicsSpellData>();

            spell.spellName = entry.name;
            spell.Spellcategory = TypesCategoryFromString(entry.category);
            spell.spellType = PsionicsCategoryFromString(entry.type);
            spell.spellDescription = entry.description;
            spell.spellCost = entry.cost;

            string assetName = entry.name.Replace(" ", "_");
            string assetPath = $"{folderPath}/{assetName}.asset";

            AssetDatabase.CreateAsset(spell, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"✅ Psionics: Imported {spellList.spells.Count} spells.");
    }

    [MenuItem("Tools/Import Spells from JSON (NeoSensorics)")]
    public static void ImportNeoSensoricsSpells()
    {
        string path = EditorUtility.OpenFilePanel("Select NeoSensorics JSON file", "", "json");
        if (string.IsNullOrEmpty(path)) return;

        string json = File.ReadAllText(path);
        GeneralSpellList spellList = JsonUtility.FromJson<GeneralSpellList>(json);

        string folderPath = "Assets/Scripts/NeoSciencePanel/NeoScienceScriptables/NeoSensorics";
        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets/Scripts/NeoSciencePanel/NeoScienceScriptables", "NeoSensorics");

        foreach (GeneralSpellEntry entry in spellList.spells)
        {
            NeoSensoricsSpellData spell = ScriptableObject.CreateInstance<NeoSensoricsSpellData>();

            spell.spellName = entry.name;
            spell.Spellcategory = TypesCategoryFromString(entry.category);
            spell.spellType = NeoSensoricsCategoryFromString(entry.type);
            spell.spellDescription = entry.description;
            spell.spellCost = entry.cost;

            string assetName = entry.name.Replace(" ", "_");
            string assetPath = $"{folderPath}/{assetName}.asset";

            AssetDatabase.CreateAsset(spell, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"✅ NeoSensorics: Imported {spellList.spells.Count} spells.");
    }

    [MenuItem("Tools/Import Spells from JSON (NeoLinguistics)")]
    public static void ImportNeoLinguisticsSpells()
    {
        string path = EditorUtility.OpenFilePanel("Select NeoLinguistics JSON file", "", "json");
        if (string.IsNullOrEmpty(path)) return;

        string json = File.ReadAllText(path);
        LinguisticsSpellList spellList = JsonUtility.FromJson<LinguisticsSpellList>(json);

        string folderPath = "Assets/Scripts/NeoSciencePanel/NeoScienceScriptables/NeoLinguistics";
        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets/Scripts/NeoSciencePanel/NeoScienceScriptables", "NeoLinguistics");

        foreach (LinguisticsSpellEntry entry in spellList.spells)
        {
            NeoLinguisticsSpellData spell = ScriptableObject.CreateInstance<NeoLinguisticsSpellData>();

            spell.spellName = entry.name;
            spell.spellLevel = entry.level;
            spell.spellCastingTime = entry.castingTime;
            spell.spellRange = entry.range;
            spell.spellDescription = entry.description;

            string assetName = entry.name.Replace(" ", "_");
            string assetPath = $"{folderPath}/{assetName}.asset";

            AssetDatabase.CreateAsset(spell, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"✅ NeoLinguistics: Imported {spellList.spells.Count} spells.");
    }

    // ---------- ENUM CONVERSIONS ----------
    private static TypesCategory TypesCategoryFromString(string str)
    {
        System.Enum.TryParse(str, true, out TypesCategory result);
        return result;
    }

    private static PsionicsCategory PsionicsCategoryFromString(string str)
    {
        System.Enum.TryParse(str, true, out PsionicsCategory result);
        return result;
    }

    private static NeoSensoricsCategory NeoSensoricsCategoryFromString(string str)
    {
        System.Enum.TryParse(str, true, out NeoSensoricsCategory result);
        return result;
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

    // ---------- JSON STRUCTURES ----------

    [System.Serializable]
    private class NeoPhysicsSpellEntry
    {
        public string name;
        public int level;
        public string type;
        public string castingTime;
        public string range;
        public string description;
    }

    [System.Serializable]
    private class NeoPhysicsSpellList
    {
        public List<NeoPhysicsSpellEntry> spells;
    }

    [System.Serializable]
    private class GeneralSpellEntry
    {
        public string name;
        public string category;
        public string type;
        public string description;
        public int cost;
    }

    [System.Serializable]
    private class GeneralSpellList
    {
        public List<GeneralSpellEntry> spells;
    }

    [System.Serializable]
    private class LinguisticsSpellEntry
    {
        public string name;
        public int level;
        public string castingTime;
        public string range;
        public string description;
    }

    [System.Serializable]
    private class LinguisticsSpellList
    {
        public List<LinguisticsSpellEntry> spells;
    }
}
