using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(KnowledgeBaseData))]
public class KnowledgeBaseDataEditor : Editor
{
    private List<bool> foldoutStates = new List<bool>();

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty sectionList = serializedObject.FindProperty("knowledgeDataSectionList");

        if (sectionList != null)
        {
            EditorGUILayout.LabelField("Knowledge Data Sections", EditorStyles.boldLabel);

            EnsureFoldoutStateListSize(sectionList.arraySize);

            for (int i = 0; i < sectionList.arraySize; i++)
            {
                SerializedProperty section = sectionList.GetArrayElementAtIndex(i);
                SerializedProperty sectionName = section.FindPropertyRelative("knowledgeDataSectionName");
                SerializedProperty nodesList = section.FindPropertyRelative("knowledgeBaseDataNodesList");

                foldoutStates[i] = EditorGUILayout.Foldout(foldoutStates[i], sectionName.stringValue != "" ? sectionName.stringValue : $"Section {i + 1}", true, EditorStyles.foldoutHeader);

                if (foldoutStates[i])
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    sectionName.stringValue = EditorGUILayout.TextField("Section Name", sectionName.stringValue);
                    EditorGUILayout.Space(5);

                    if (nodesList != null && nodesList.isArray)
                    {
                        EditorGUILayout.LabelField("Nodes", EditorStyles.boldLabel);

                        for (int j = 0; j < nodesList.arraySize; j++)
                        {
                            SerializedProperty node = nodesList.GetArrayElementAtIndex(j);
                            SerializedProperty title = node.FindPropertyRelative("titleText");
                            SerializedProperty content = node.FindPropertyRelative("contentInfoText");

                            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                            title.stringValue = EditorGUILayout.TextField("Title", title.stringValue);
                            EditorGUILayout.Space(5);
                            content.stringValue = DrawExpandableWrappedTextArea("Content", content.stringValue);
                            EditorGUILayout.EndVertical();
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.Space(10);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void EnsureFoldoutStateListSize(int requiredSize)
    {
        while (foldoutStates.Count < requiredSize)
        {
            foldoutStates.Add(true);
        }

        while (foldoutStates.Count > requiredSize)
        {
            foldoutStates.RemoveAt(foldoutStates.Count - 1);
        }
    }

    private string DrawExpandableWrappedTextArea(string label, string text)
    {
        GUIStyle textAreaStyle = new GUIStyle(EditorStyles.textArea)
        {
            wordWrap = true
        };

        float minHeight = EditorGUIUtility.singleLineHeight * 2;
        float calculatedHeight = textAreaStyle.CalcHeight(new GUIContent(text), EditorGUIUtility.currentViewWidth - 20f);
        float finalHeight = Mathf.Max(minHeight, calculatedHeight);

        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        return EditorGUILayout.TextArea(text, textAreaStyle, GUILayout.Height(finalHeight));
    }
}
