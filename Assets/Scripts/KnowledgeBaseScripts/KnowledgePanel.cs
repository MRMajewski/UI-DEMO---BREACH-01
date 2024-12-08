using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Icons;
using UnityEngine.UI;
using DG.Tweening;

public class KnowledgePanel : SimpleUIPanelMobiles
{
    private bool refrehAnswers = true;

    [SerializeField]
    private KnowledgeBaseData knowledgeBaseData;

    public List<KnowledgeNode> knowledgeBaseNodes = new List<KnowledgeNode>();

    [SerializeField]
    KnowledgeNode exampleKnowledgeNode;

    [SerializeField]
    KnowledgeNode exampleKnowledgeSection;

    [SerializeField]
    Transform content;


    //[SerializeField]
    //private ScrollRectScroller scrollRectScroller;

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

    //[SerializeField]
    //private TabGroupCanvasKnowledge tabGroup;

    //[TabGroup("Budowanie FAQ")]
    //[Button("Build FAQ"), GUIColor(0, .5f, 0f)]

    private void OnEnable()
    {
        if (refrehAnswers)
        {
            foreach (KnowledgeNode node in knowledgeBaseNodes)
            {
                if (!node.IsSectionName)
                {
                    //node.answerTranslate.Refresh();
                }
            }
            refrehAnswers = false;
        }
    }
    public override void DisablePanel()
    {
        this.gameObject.SetActive(false);
        //   panelsCanvasGroup.DOFade(0, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine).
        //  OnComplete(() => this.gameObject.SetActive(false));
    }

    public override void EnablePanel()
    {
        this.gameObject.SetActive(true);
        BuildFAQ();
        panelsCanvasGroup.DOFade(1, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine);
    }

    void BuildFAQ()
    {
        DestroyPreviousNodes();

        exampleKnowledgeNode.gameObject.SetActive(true);
        exampleKnowledgeSection.gameObject.SetActive(true);
        foreach (KnowledgeBaseDataSection faqDataSection in knowledgeBaseData.KnowledgeDataSectionList)
        {
            BuildFAQSection(faqDataSection);
        }
        exampleKnowledgeNode.gameObject.SetActive(false);
        exampleKnowledgeSection.gameObject.SetActive(false);
     //   RefreshButtonNavigations();

      //  changePanel.firstSelected = faqNodes[0].QuestionButton.gameObject;
    }

    private void BuildFAQSection(KnowledgeBaseDataSection faqDataSection)
    {
        KnowledgeNode newFAQSectionHeader = Instantiate(exampleKnowledgeSection, content);

        newFAQSectionHeader.TitleText.text = faqDataSection.knowledgeDataSectionName;
        knowledgeBaseNodes.Add(newFAQSectionHeader);

        foreach (KnowledgeBaseDataNode faqDataNode in faqDataSection.KnowledgeBaseDataNodesList)
        {
            BuildFAQNode(faqDataNode);
        }
    }
    private void BuildFAQNode(KnowledgeBaseDataNode faqDataNode)
    {
        KnowledgeNode newFAQNode = Instantiate(exampleKnowledgeNode, content);

        newFAQNode.TitleText.text = faqDataNode.titleText;
        newFAQNode.ContentText.text = faqDataNode.contentInfoText;
        knowledgeBaseNodes.Add(newFAQNode);
    }
    private void DestroyPreviousNodes()
    {
        foreach (var node in knowledgeBaseNodes)
        {
            DestroyImmediate(node.gameObject);
        }
        knowledgeBaseNodes.Clear();
    }

    public void RefreshExpandAll()
    {
        areAllTabsExpanded = AreAllTabsExpandedCheck();

        foreach (KnowledgeNode faqNode in knowledgeBaseNodes)
        {
            if (faqNode.IsOpen == areAllTabsExpanded)
            {
                continue;
            }
            else
            {
                if (!faqNode.IsSectionName)
                    faqNode.SelectionClick();
            }
        }
        areAllTabsExpanded = !areAllTabsExpanded;
        RefreshExpandAllIconAndText();

      //  SetSelectedFirstFAQNode();
    }

    public bool AreAllTabsExpandedCheck()
    {
        foreach (KnowledgeNode faqNode in knowledgeBaseNodes)
        {
            if (!faqNode.IsOpen)
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








    //public void RefreshButtonNavigations(Selectable selectableAboveFirstNode = null)
    //{
    //    List<FAQNode> activeNodes = faqNodes.Where(x => x.gameObject.activeSelf && !x.IsSectionName).ToList();

    //    switch (activeNodes.Count)
    //    {
    //        case 1:
    //            selectableAboveFirstNode.SetNavigationDown(activeNodes[0].QuestionButton);
    //            ExpandAllButton.SetNavigationDown(activeNodes[0].QuestionButton);
    //            activeNodes[0].QuestionButton.SetNavigationUp(selectableAboveFirstNode);
    //            break;
    //        case 2:
    //            activeNodes[0].QuestionButton.SetNavigationDown(activeNodes[1].QuestionButton);

    //            activeNodes[activeNodes.Count - 1].QuestionButton.SetNavigationDown(null);
    //            activeNodes[activeNodes.Count - 1].QuestionButton.SetNavigationUp(activeNodes[activeNodes.Count - 2].QuestionButton);
    //            goto case 1;
    //        case >= 3:
    //            for (int i = 1; i < activeNodes.Count - 1; i++)
    //            {
    //                activeNodes[i].QuestionButton.SetNavigationDown(activeNodes[i + 1].QuestionButton);
    //                activeNodes[i].QuestionButton.SetNavigationUp(activeNodes[i - 1].QuestionButton);
    //            }
    //            goto case 2;
    //        default:
    //            return;
    //    }
    //}

    //private void SetSelectedFirstFAQNode()
    //{
    //    Touchscreen.Instance.eventSystem.SetSelectedGameObject(GetFirstActiveObject());

    //}
    //public virtual GameObject GetFirstActiveObject()
    //{
    //    GameObject gameObject = null;
    //    int i = 0;

    //    int activeChildrenCount = content.GetComponentsInChildren<FAQNode>().GetLength(0);

    //    while (i < activeChildrenCount)
    //    {
    //        if (content.transform.GetChild(i).gameObject.activeSelf)
    //        {
    //            gameObject = content.transform.GetChild(i).GetComponent<FAQNode>().QuestionButton.gameObject;
    //            return gameObject;
    //        }
    //        i++;
    //    }
    //    return gameObject;
    //}
    public void OnSelectionExpandAllCheck()
    {
        areAllTabsExpanded = AreAllTabsExpandedCheck();
        RefreshExpandAllIconAndText();
    }
}
