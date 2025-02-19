using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TrainingBaseDataNode
{
    [TextArea(1, 2)]
    public string titleText;
    [TextArea(1, 30)]
    public string contentInfoText;

    public TrainingBaseDataNode(string title, string info)
    {
        titleText = title;
        contentInfoText = info;
    }
}

public class TrainingBaseData : MonoBehaviour, IDataBase
{
    [SerializeField]
    private List<TrainingBaseDataSection> trainingDataSectionList;

    [SerializeField]
    public List<TrainingBaseDataSection> TrainingDataSectionList { get => trainingDataSectionList; }
}