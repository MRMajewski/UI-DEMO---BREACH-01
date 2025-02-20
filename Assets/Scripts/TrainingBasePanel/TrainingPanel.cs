using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingPanel : AttributesInfoPanel
{
    [SerializeField]
    private TrainingBaseData trainingBaseData;

    [SerializeField]
    TrainingSectionPanel exampleTrainingSectionPanel;

    [SerializeField]
    private RectTransform container;

    [SerializeField]
    private MiniatureTrainingIconsChanger trainingsIconChanger;

    [SerializeField]
    private List<TrainingSectionPanel> trainingSectionPanels = new List<TrainingSectionPanel>();

    //[Header("Expand All references")]
    //[SerializeField]
    //private bool areAllTabsExpanded = true;
    //[SerializeField]
    //private Sprite ExpandAllSprite;
    //[SerializeField]
    //private Sprite CollapseAllSprite;
    //[SerializeField]
    //private Image ExpandAllImage;
    //[SerializeField]
    //private TextMeshProUGUI ExpandAllText;
    //[SerializeField]
    //private Button ExpandAllButton;

    public override void EnablePanel()
    {
        this.gameObject.SetActive(true);
        panelsCanvasGroup.DOFade(1, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine);

        ResizeSnappedPanel();

        snapper.OnPanelChanged += trainingsIconChanger.SetAlphaForIndex;
        trainingsIconChanger.SetAlphaForIndex(0);

        foreach (Button icon in iconButtons)
        {
            int index = iconButtons.IndexOf(icon);
            icon.onClick.AddListener(() => snapper.SnapToPanelFromButton(index));
        }
    }

    private void Start()
    {
        InitializeCategory(0);
    }

    public override void DisablePanel()
    {
        base.DisablePanel();
        snapper.OnPanelChanged -= trainingsIconChanger.SetAlphaForIndex;
    }

    [ContextMenu("BUILD TRAININGS MENU")]
    public void InitailizeTrainingPanelDatabase()
    {
        DestroyPreviousNodes();
      
        tooltipTriggerAdders.Clear();
        tooltipTriggerAdders.TrimExcess();

        iconButtons.Clear();
        iconButtons.TrimExcess();

        exampleTrainingSectionPanel.gameObject.SetActive(true);

        foreach (TrainingBaseDataSection sectionData in trainingBaseData.TrainingDataSectionList)
        {
            TrainingSectionPanel newElement = Instantiate(exampleTrainingSectionPanel, container);

            newElement.InitializeTrainingSection(sectionData);
            trainingSectionPanels.Add(newElement);
            snapperPanelsList.Add(newElement.GetComponent<RectTransform>());

            tooltipTriggerAdders.AddRange(newElement.TriggerGameObjectAdder);
        }
        exampleTrainingSectionPanel.gameObject.SetActive(false);

        snapper.InitPanels(trainingSectionPanels);
        trainingsIconChanger.CreateIcons(trainingBaseData);

        for (int i = 0; i < trainingsIconChanger.IconList.Count; i++)
        {
            iconButtons.Add(trainingsIconChanger.IconList[i].GetComponent<Button>());
        }

        void DestroyPreviousNodes()
        {
            foreach (TrainingSectionPanel trainingSection in trainingSectionPanels)
            {
                if (trainingSection != null)
                    DestroyImmediate(trainingSection.gameObject);
            }
            trainingSectionPanels.Clear();
            trainingSectionPanels.TrimExcess();

            snapperPanelsList.Clear();
            snapperPanelsList.TrimExcess();
        }
    }

    //public void RefreshExpandAll()
    //{
    //    areAllTabsExpanded = AreAllTabsExpandedCheck();

    //    foreach (KnowledgeNodeBase knowledgeNode in knowledgeBaseNodes)
    //    {
    //        if (knowledgeNode.IsOpen == areAllTabsExpanded)
    //        {
    //            continue;
    //        }
    //        else
    //        {
    //            if (knowledgeNode.GetComponent<KnowledgeNodeBase>() != null)
    //            {
    //                knowledgeNode.SelectionClick();
    //            }
    //        }
    //    }
    //    areAllTabsExpanded = !areAllTabsExpanded;
    //    RefreshExpandAllIconAndText();
    //}

    //public bool AreAllTabsExpandedCheck()
    //{
    //    foreach (KnowledgeNodeBase knowledgeNode in knowledgeBaseNodes)
    //    {
    //        if (!knowledgeNode.IsOpen)
    //        {
    //            continue;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    //public void RefreshExpandAllIconAndText()
    //{
    //    if (areAllTabsExpanded)
    //    {
    //        ExpandAllImage.sprite = ExpandAllSprite;
    //        ExpandAllText.text = "Otwórz wszystko";
    //    }
    //    else
    //    {
    //        ExpandAllImage.sprite = CollapseAllSprite;
    //        ExpandAllText.text = "Zamknij wszystko";
    //    }
    //    ExpandAllText.text = ExpandAllText.text.ToUpper();
    //}

    //public void OnSelectionExpandAllCheck()
    //{
    //    areAllTabsExpanded = AreAllTabsExpandedCheck();
    //    RefreshExpandAllIconAndText();
    //}
}
