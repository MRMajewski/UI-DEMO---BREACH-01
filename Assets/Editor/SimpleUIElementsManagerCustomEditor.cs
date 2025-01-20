
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleUIPanelMobilesManager))]
public class SimpleUIElementsManagerCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Wyœwietl domyœlny inspektor
        DrawDefaultInspector();

        // Odnoœnik do skryptu
        SimpleUIPanelMobilesManager script = (SimpleUIPanelMobilesManager)target;

        // Dodanie przycisku
        if (GUILayout.Button("Wykonaj Inicjacjê Danych na Panelach"))
        {
            script.InitPanelsData();
        }
    }
}

[CustomEditor(typeof(AttributesInfoPanel))]
public class AttributesInfoPanelCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Wyœwietl domyœlny inspektor
        DrawDefaultInspector();

        // Odnoœnik do skryptu
        AttributesInfoPanel script = (AttributesInfoPanel)target;

        // Dodanie przycisku
        if (GUILayout.Button("Wykonaj Inicjacjê Danych na Panelach"))
        {
            script.InitializePanel();
        }
    }
}

