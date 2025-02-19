using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrainingSectionData", menuName = "TrainingBaseData/TrainingSectionData")]
public class TrainingBaseDataSection : ScriptableObject, ISnapperPanelData
{
    [SerializeField]
    private string trainingDataSectionName;

    public string TrainingDataSectionName { get => trainingDataSectionName; }

    [SerializeField]
    private Sprite trainingDataSectionIcon;

    public Sprite TrainingDataSectionIcon { get => trainingDataSectionIcon; }
    [TextArea(1, 30)]
    [SerializeField]
    private string trainingDataSectionDescription;

    public string TrainingDataSectionDescription { get => trainingDataSectionDescription; }

    [SerializeField]
    private List<TrainingBaseDataNode> trainingBaseDataNodesList;

    [SerializeField]
    public List<TrainingBaseDataNode> TrainingBaseDataNodesList { get => trainingBaseDataNodesList; }

    public Sprite GetMiniatureIcon()
    {
        return trainingDataSectionIcon;
    }

}
