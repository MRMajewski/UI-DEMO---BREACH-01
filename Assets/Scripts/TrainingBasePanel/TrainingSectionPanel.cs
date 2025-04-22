using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingSectionPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI trainingSectionName;

    [SerializeField]
    private TextMeshProUGUI trainingSectionDescription;

    [SerializeField]
    private Image trainingSectionImage;

    [SerializeField]
    private List<TrainingNode> trainingNodesList;

    [SerializeField]
    private TrainingNode exampleTrainingNode;

    [SerializeField]
    private Transform trainingNodesContainer;


    [SerializeField]
    private List<TooltipTriggerGameObjectAdder> triggerAdders;

    public List<TooltipTriggerGameObjectAdder> TriggerGameObjectAdder { get { return triggerAdders; } }

    [SerializeField]
    protected UIScrollViewFitter uIScrollViewFitter;

    public void InitializeTrainingSection(TrainingBaseDataSection trainingBaseDataSection)
    {
        triggerAdders.Clear();
        triggerAdders.TrimExcess();


        Debug.Log("INITIALIZE " + trainingBaseDataSection.TrainingDataSectionName);

        trainingSectionName.text = trainingBaseDataSection.TrainingDataSectionName;

        trainingSectionImage.sprite = trainingBaseDataSection.TrainingDataSectionIcon;

        trainingSectionDescription.text = trainingBaseDataSection.TrainingDataSectionDescription;

        trainingNodesList.Clear();
        trainingNodesList.TrimExcess();
        exampleTrainingNode.gameObject.SetActive(true);
        InitializeTrainingNodes(trainingBaseDataSection.TrainingBaseDataNodesList);

        exampleTrainingNode.gameObject.SetActive(false);

        void InitializeTrainingNodes(List<TrainingBaseDataNode> trainingBaseNodesList)
        {
            foreach (TrainingNode item in trainingNodesList)
            {
                DestroyImmediate(item.gameObject);
            }

            foreach (TrainingBaseDataNode trainingNode in trainingBaseNodesList)
            {
                InitializeTrainingNode(trainingNode);
            }
        }

        void InitializeTrainingNode(TrainingBaseDataNode trainingDataNode)
        {
            TrainingNode newNode = Instantiate(exampleTrainingNode, trainingNodesContainer);

            newNode.TitleText.text = trainingDataNode.titleText;
            newNode.ContentText.text = trainingDataNode.contentInfoText;
            trainingNodesList.Add(newNode);

            triggerAdders.AddRange(newNode.TriggerGameObjectAdder);
        }
    }
}

