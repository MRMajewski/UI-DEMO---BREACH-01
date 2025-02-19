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

    public List<KnowledgeNodeBase> knowledgeBaseNodes = new List<KnowledgeNodeBase>();

    public List<KnowledgeSectionNode> knowledgeSectionNodes = new List<KnowledgeSectionNode>();

    [SerializeField]
    KnowledgeNode exampleTrainingNode;

    [SerializeField]
    TrainingSectionPanel exampleTrainingSectionPanel;

    [SerializeField]
    Transform content;

    [SerializeField]
    private RectTransform container;

    [SerializeField]
    private MiniatureTrainingIconsChanger trainingsIconChanger;

    [SerializeField]
    private List<TrainingSectionPanel> trainingSectionPanels = new List<TrainingSectionPanel>();

    [Header("Expand All references")]
    [SerializeField]
    private bool areAllTabsExpanded = true;
    [SerializeField]
    private Sprite ExpandAllSprite;
    [SerializeField]
    private Sprite CollapseAllSprite;
    [SerializeField]
    private Image ExpandAllImage;
    [SerializeField]
    private TextMeshProUGUI ExpandAllText;
    [SerializeField]
    private Button ExpandAllButton;

    public override void InitializePanel()
    {
        //snapper.OnPanelChanged += trainingsIconChanger.SetAlphaForIndex;

        //foreach (Button icon in iconButtons)
        //{
        //    int index = iconButtons.IndexOf(icon);
        //    icon.onClick.AddListener(() => snapper.SnapToPanelFromButton(index));
        //}
        ////    iconChanger.SetAlphaForIndex(0);

        //InitializeCategory(0);


        // iconButtons[0].onClick.Invoke();
        // ProcessTooltipTriggers();
    }

    public override void InitializePanelData()
    {
    //    BuildKnowledgeBase();
      //  ProcessTooltipTriggers();
    }

    public override void EnablePanel()
    {
        this.gameObject.SetActive(true);
        panelsCanvasGroup.DOFade(1, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine);

        ResizeSnappedPanel();

        snapper.OnPanelChanged += trainingsIconChanger.SetAlphaForIndex;
        trainingsIconChanger.SetAlphaForIndex(0);

        //  snapper.OnPanelChanged += trainingsIconChanger.SetAlphaForIndex;

        foreach (Button icon in iconButtons)
        {
            int index = iconButtons.IndexOf(icon);
            icon.onClick.AddListener(() => snapper.SnapToPanelFromButton(index));
        }
        //    iconChanger.SetAlphaForIndex(0);
    }

    private void Start()
    {
 

        InitializeCategory(0);
    }

    public override void DisablePanel()
    {
        this.gameObject.SetActive(false);
        snapper.OnPanelChanged -= trainingsIconChanger.SetAlphaForIndex;
    }

    //public void OnDestroy()
    //{
    //    snapper.OnPanelChanged -= trainingsIconChanger.SetAlphaForIndex;
    //}


    [ContextMenu("BUILD TRAININGS MENU")]
    private void BuildTrainingBase()
    {
        DestroyPreviousNodes();
      
     //   tooltipTriggerAdders.Clear();
      //  tooltipTriggerAdders.TrimExcess();

        exampleTrainingSectionPanel.gameObject.SetActive(true);

        foreach (TrainingBaseDataSection sectionData in trainingBaseData.TrainingDataSectionList)
        {
            TrainingSectionPanel newElement = Instantiate(exampleTrainingSectionPanel, container);

            newElement.InitializeTrainingSection(sectionData);
            trainingSectionPanels.Add(newElement);

          //  tooltipTriggerAdders.AddRange(newElement.TriggerGameObjectAdder);
        }
        exampleTrainingSectionPanel.gameObject.SetActive(false);

        snapper.InitPanels(trainingSectionPanels);
        trainingsIconChanger.CreateIcons(trainingBaseData);

        void DestroyPreviousNodes()
        {
            foreach (TrainingSectionPanel trainingSection in trainingSectionPanels)
            {
                if (trainingSection != null)
                    DestroyImmediate(trainingSection.gameObject);
            }
            trainingSectionPanels.Clear();
            trainingSectionPanels.TrimExcess();
        }
    }

    public void RefreshExpandAll()
    {
        areAllTabsExpanded = AreAllTabsExpandedCheck();

        foreach (KnowledgeNodeBase knowledgeNode in knowledgeBaseNodes)
        {
            if (knowledgeNode.IsOpen == areAllTabsExpanded)
            {
                continue;
            }
            else
            {
                if (knowledgeNode.GetComponent<KnowledgeNodeBase>() != null)
                {
                    knowledgeNode.SelectionClick();
                }
            }
        }
        areAllTabsExpanded = !areAllTabsExpanded;
        RefreshExpandAllIconAndText();
    }

    public bool AreAllTabsExpandedCheck()
    {
        foreach (KnowledgeNodeBase knowledgeNode in knowledgeBaseNodes)
        {
            if (!knowledgeNode.IsOpen)
            {
                continue;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public void RefreshExpandAllIconAndText()
    {
        if (areAllTabsExpanded)
        {
            ExpandAllImage.sprite = ExpandAllSprite;
            ExpandAllText.text = "Otwórz wszystko";
        }
        else
        {
            ExpandAllImage.sprite = CollapseAllSprite;
            ExpandAllText.text = "Zamknij wszystko";
        }
        ExpandAllText.text = ExpandAllText.text.ToUpper();
    }

    public void OnSelectionExpandAllCheck()
    {
        areAllTabsExpanded = AreAllTabsExpandedCheck();
        RefreshExpandAllIconAndText();
    }
}
