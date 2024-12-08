using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [System.Serializable]
    public class KnowledgeBaseDataNode
    {
        public string titleText;
        public string contentInfoText;

        public KnowledgeBaseDataNode(string title, string info)
        {

            titleText = title;
            contentInfoText = info;
        }
    }

    [System.Serializable]
    public class KnowledgeBaseDataSection
    {
        public string knowledgeDataSectionName;

        [SerializeField]
        private List<KnowledgeBaseDataNode> knowledgeBaseDataNodesList;

        [SerializeField]
        public List<KnowledgeBaseDataNode> KnowledgeBaseDataNodesList { get => knowledgeBaseDataNodesList; }

        public KnowledgeBaseDataSection(string sectionName, List<KnowledgeBaseDataNode> knowledgeDataNodes)
        {
            knowledgeDataSectionName = sectionName;
            knowledgeBaseDataNodesList = knowledgeDataNodes;
        }
    }

    [CreateAssetMenu(fileName = "KnowledgeBaseData", menuName = "KnowledgeBaseData")]
    [System.Serializable]
    public class KnowledgeBaseData : ScriptableObject
    {
        [SerializeField]
        private List<KnowledgeBaseDataSection> knowledgeDataSectionList;

        [SerializeField]
        public List<KnowledgeBaseDataSection> KnowledgeDataSectionList { get => knowledgeDataSectionList; }
    }
