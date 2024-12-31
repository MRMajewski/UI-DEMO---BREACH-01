using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KnowledgeBaseData))]
public class KnowledgeBaseDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty sectionList = serializedObject.FindProperty("knowledgeDataSectionList");

        if (sectionList != null)
        {
            EditorGUILayout.LabelField("Knowledge Data Sections", EditorStyles.boldLabel);

            for (int i = 0; i < sectionList.arraySize; i++)
            {
                SerializedProperty section = sectionList.GetArrayElementAtIndex(i);
                SerializedProperty nodesList = section.FindPropertyRelative("knowledgeBaseDataNodesList");

                // Wyœwietlenie sekcji
                EditorGUILayout.Space(20);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.PropertyField(section.FindPropertyRelative("knowledgeDataSectionName"), new GUIContent("Section Name"));

                if (nodesList != null && nodesList.isArray)
                {
                    EditorGUILayout.LabelField("Nodes", EditorStyles.boldLabel);

                    for (int j = 0; j < nodesList.arraySize; j++)
                    {
                        SerializedProperty node = nodesList.GetArrayElementAtIndex(j);

                        // Wyœwietlanie ka¿dego wêz³a
                        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                        SerializedProperty title = node.FindPropertyRelative("titleText");
                        SerializedProperty content = node.FindPropertyRelative("contentInfoText");

                        // Automatycznie zwiêkszaj¹ce siê pole TextArea z zawijaniem tekstu
                        title.stringValue = EditorGUILayout.TextField("Title", title.stringValue);
                        EditorGUILayout.Space(5);
                        content.stringValue = DrawExpandableWrappedTextArea("Content", content.stringValue);

                        EditorGUILayout.EndVertical();
                    }
                }

                EditorGUILayout.EndVertical();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private string DrawExpandableWrappedTextArea(string label, string text)
    {
        // Styl TextArea z w³¹czonym zawijaniem tekstu
        GUIStyle textAreaStyle = new GUIStyle(EditorStyles.textArea)
        {
            wordWrap = true // Zawijanie tekstu
        };

        // Dynamiczne obliczanie wysokoœci na podstawie d³ugoœci tekstu
        float minHeight = EditorGUIUtility.singleLineHeight * 2; // Minimalna wysokoœæ (2 wiersze)
        float calculatedHeight = textAreaStyle.CalcHeight(new GUIContent(text), EditorGUIUtility.currentViewWidth);
        float finalHeight = Mathf.Max(minHeight, calculatedHeight); // U¿yj wy¿szej wartoœci

        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        return EditorGUILayout.TextArea(text, textAreaStyle, GUILayout.Height(finalHeight));
    }
}
