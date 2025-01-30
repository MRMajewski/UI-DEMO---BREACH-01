
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
            script.InitializePanelData();
        }
    }
}

[CustomEditor(typeof(IntroInfoPanel))]
public class IntroInfoPanelCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Wyœwietl domyœlny inspektor
        DrawDefaultInspector();

        // Odnoœnik do skryptu
        IntroInfoPanel script = (IntroInfoPanel)target;

        // Dodanie przycisku
        if (GUILayout.Button("Wykonaj Inicjacjê Danych na Panelach"))
        {
            script.InitializePanelData();
        }
    }
}

[CustomEditor(typeof(KnowledgePanel))]
public class KnowledgePanelCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Wyœwietl domyœlny inspektor
        DrawDefaultInspector();

        // Odnoœnik do skryptu
        KnowledgePanel script = (KnowledgePanel)target;

        // Dodanie przycisku
        if (GUILayout.Button("Wykonaj Inicjacjê Danych na Panelach"))
        {
            script.InitializePanelData();
        }
    }
}

[CustomEditor(typeof(ClassPanel))]
public class ClassPanelCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Wyœwietl domyœlny inspektor
        DrawDefaultInspector();

        // Odnoœnik do skryptu
        ClassPanel script = (ClassPanel)target;

        // Dodanie przycisku
        if (GUILayout.Button("Wykonaj Inicjacjê Danych na Panelach"))
        {
            script.InitPanel();
        }
        if (GUILayout.Button("Wykonaj Inicjacjê Tooltip Triggerów"))
        {
            script.InitializePanelData();
        }
    }
}

