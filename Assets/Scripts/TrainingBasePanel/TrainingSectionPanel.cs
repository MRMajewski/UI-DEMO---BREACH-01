using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingSectionPanel : MonoBehaviour, ISnappedElement
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

    [SerializeField]
    private RectTransform viewportRectTransform;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private RectTransform content;

    public void InitializeTrainingSection(TrainingBaseDataSection trainingBaseDataSection)
    {
        triggerAdders.Clear();
        triggerAdders.TrimExcess();

        Debug.Log("INITIALIZE " + trainingBaseDataSection.TrainingDataSectionName);

        trainingSectionName.text = trainingBaseDataSection.TrainingDataSectionName;

        trainingSectionImage.sprite = trainingBaseDataSection.TrainingDataSectionIcon;

        trainingSectionDescription.text = trainingBaseDataSection.TrainingDataSectionDescription;

    
        trainingSectionImage.color = new Color(trainingBaseDataSection.TrainingDataSectionIconColor.r, trainingBaseDataSection.TrainingDataSectionIconColor.g, trainingBaseDataSection.TrainingDataSectionIconColor.b, 50f / 255f);

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

    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }
    public RectTransform GetContentTransform()
    {
        return content;
    }
    public void ResetRectScroll()
    {
        Vector2 targetPosition = new Vector2(content.anchoredPosition.x, 0f);
        content.DOAnchorPos(targetPosition, 0.3f).SetEase(Ease.InOutSine).SetUpdate(true);
    }

    public RectTransform GetViewportTransform()
    {
        return viewportRectTransform;
    }
}

