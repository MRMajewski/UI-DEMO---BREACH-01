
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleUIPanelMobilesManager))]
public class SimpleUIElementsManagerCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Wy�wietl domy�lny inspektor
        DrawDefaultInspector();

        // Odno�nik do skryptu
        SimpleUIPanelMobilesManager script = (SimpleUIPanelMobilesManager)target;

        // Dodanie przycisku
        if (GUILayout.Button("Wykonaj Inicjacj� Danych na Panelach"))
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
        // Wy�wietl domy�lny inspektor
        DrawDefaultInspector();

        // Odno�nik do skryptu
        AttributesInfoPanel script = (AttributesInfoPanel)target;

        // Dodanie przycisku
        if (GUILayout.Button("Wykonaj Inicjacj� Danych na Panelach"))
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
        // Wy�wietl domy�lny inspektor
        DrawDefaultInspector();

        // Odno�nik do skryptu
        IntroInfoPanel script = (IntroInfoPanel)target;

        // Dodanie przycisku
        if (GUILayout.Button("Wykonaj Inicjacj� Danych na Panelach"))
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
        // Wy�wietl domy�lny inspektor
        DrawDefaultInspector();

        // Odno�nik do skryptu
        KnowledgePanel script = (KnowledgePanel)target;

        // Dodanie przycisku
        if (GUILayout.Button("Wykonaj Inicjacj� Danych na Panelach"))
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
        // Wy�wietl domy�lny inspektor
        DrawDefaultInspector();

        // Odno�nik do skryptu
        ClassPanel script = (ClassPanel)target;

        // Dodanie przycisku
        if (GUILayout.Button("Wykonaj Inicjacj� Danych na Panelach"))
        {
            script.InitializeClasses();
        }
        if (GUILayout.Button("Wykonaj Inicjacj� Tooltip Trigger�w"))
        {
            script.InitializePanelData();
        }
    }
}

[CustomEditor(typeof(TrainingPanel))]
public class TrainingPanelCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Wy�wietl domy�lny inspektor
        DrawDefaultInspector();

        // Odno�nik do skryptu
        TrainingPanel script = (TrainingPanel)target;

        // Dodanie przycisku
        if (GUILayout.Button("Wykonaj Inicjacj� Danych na Panelach"))
        {
            script.InitailizeTrainingPanelDatabase();
        }
        if (GUILayout.Button("Wykonaj Inicjacj� Tooltip Trigger�w"))
        {
            script.InitializePanelData();
        }
    }
}

