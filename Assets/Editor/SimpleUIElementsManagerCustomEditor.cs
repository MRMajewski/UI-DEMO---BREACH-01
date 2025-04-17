
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleUIPanelMobilesManager))]
public class SimpleUIElementsManagerCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SimpleUIPanelMobilesManager script = (SimpleUIPanelMobilesManager)target;

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
        DrawDefaultInspector();

        AttributesInfoPanel script = (AttributesInfoPanel)target;

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
        DrawDefaultInspector();

        IntroInfoPanel script = (IntroInfoPanel)target;

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
        DrawDefaultInspector();

        KnowledgePanel script = (KnowledgePanel)target;

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
        DrawDefaultInspector();

        ClassPanel script = (ClassPanel)target;

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
        DrawDefaultInspector();

        TrainingPanel script = (TrainingPanel)target;

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

[CustomEditor(typeof(NeoScienceInfoPanel))]
public class NeoScienceInfoPanelCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NeoScienceInfoPanel script = (NeoScienceInfoPanel)target;

        if (GUILayout.Button("Wykonaj Inicjacj� Spelli na SubPanelach"))
        {
            script.InitSubPanelsData();
        }
        if (GUILayout.Button("Wykonaj Inicjacj� Danych na Panelach"))
        {
            script.InitializePanel ();
        }
        if (GUILayout.Button("Wykonaj Inicjacj� Tooltip Trigger�w"))
        {
            script.InitializePanelData();
        }
    }
}

[CustomEditor(typeof(NeoScienceDatabase))]
public class NeoScienceDatabaseCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        NeoScienceDatabase script = (NeoScienceDatabase)target;

        if (GUILayout.Button(("Load All NeoScience Scriptables Data")))
        {
            script.LoadAllFromFolder();
        }
    }
}
