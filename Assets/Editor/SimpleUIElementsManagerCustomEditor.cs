
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
        //    script.InitializePanelData()
                  script.BuildKnowledgeBase();
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
            script.InitializeTrainingPanelDatabase();
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

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ItemDatabase script = (ItemDatabase)target;

        if (GUILayout.Button(("Load All Items Scriptables Data")))
        {
            script.LoadAllFromFolder();
        }
    }
}
