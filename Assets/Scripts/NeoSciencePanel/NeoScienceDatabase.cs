using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class NeoScienceDatabase : MonoBehaviour
{
    [Tooltip("List of all items available in the game.")]
    public List<NeophysicsSpellData> NeophysicsSpellsList = new List<NeophysicsSpellData>();

    public List<NeoLinguisticsSpellData> NeoLinguisticsSpellsList = new List<NeoLinguisticsSpellData>();

    public List<NeoSensoricsSpellData> NeoSensoricsSpellsList = new List<NeoSensoricsSpellData>();

    public List<PsionicsSpellData> PsionicsSpellsList = new List<PsionicsSpellData>();

#if UNITY_EDITOR
    public void LoadAllFromFolder()
    {

        NeophysicsSpellsList.Clear();
        NeoLinguisticsSpellsList.Clear();
        NeoSensoricsSpellsList.Clear();
        PsionicsSpellsList.Clear();

        LoadNeophysicsData();
        LoadLinguisticssData();
        LoadNeoSensoricsData();
        LoadPsionicsData();

        Debug.Log($"Znaleziono: Neofizyka {NeophysicsSpellsList.Count}, Linguistics {NeoLinguisticsSpellsList.Count}, Sensorics {NeoSensoricsSpellsList.Count}, Psionika {PsionicsSpellsList.Count}");
    }

    private void LoadNeophysicsData()
    {
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { "Assets/Scripts/NeoSciencePanel/NeoScienceScriptables/Neophysics" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (so is NeophysicsSpellData)
                NeophysicsSpellsList.Add((NeophysicsSpellData)so);
        }
    }

    private void LoadLinguisticssData()
    {
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { "Assets/Scripts/NeoSciencePanel/NeoScienceScriptables/NeoLinguistics" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (so is NeoLinguisticsSpellData)
                NeoLinguisticsSpellsList.Add((NeoLinguisticsSpellData)so);
        }
    }

    private void LoadNeoSensoricsData()
    {
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { "Assets/Scripts/NeoSciencePanel/NeoScienceScriptables/NeoSensorics" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (so is NeoSensoricsSpellData)
                NeoSensoricsSpellsList.Add((NeoSensoricsSpellData)so);
        }
    }
    private void LoadPsionicsData()
    {
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { "Assets/Scripts/NeoSciencePanel/NeoScienceScriptables/Psionics" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (so is PsionicsSpellData)
                PsionicsSpellsList.Add((PsionicsSpellData)so);
        }
    }
#endif
}