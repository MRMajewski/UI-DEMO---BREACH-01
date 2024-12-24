using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class KnowledgePanel : SimpleUIPanelMobiles
{

    [SerializeField]
    private KnowledgeBaseData knowledgeBaseData;

    public List<KnowledgeNodeBase> knowledgeBaseNodes = new List<KnowledgeNodeBase>();

    public List<KnowledgeSectionNode> knowledgeSectionNodes = new List<KnowledgeSectionNode>();

    [SerializeField]
    KnowledgeNode exampleKnowledgeNode;

    [SerializeField]
    KnowledgeSectionNode exampleKnowledgeSection;

    [SerializeField]
    Transform content;

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
        BuildKnowledgeBase();
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
     //   BuildKnowledgeBase();
        panelsCanvasGroup.DOFade(1, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine);
    }

    void BuildKnowledgeBase()
    {
        DestroyPreviousNodes();

        exampleKnowledgeNode.gameObject.SetActive(true);
        exampleKnowledgeSection.gameObject.SetActive(true);
        foreach (KnowledgeBaseDataSection faqDataSection in knowledgeBaseData.KnowledgeDataSectionList)
        {
            BuildKnowledgeSection(faqDataSection);
        }
        exampleKnowledgeNode.gameObject.SetActive(false);
        exampleKnowledgeSection.gameObject.SetActive(false);
    }

    private void BuildKnowledgeSection(KnowledgeBaseDataSection knowledgeBaseDataSection)
    {
        KnowledgeSectionNode newKnowledgeSectionHeader = Instantiate(exampleKnowledgeSection, content);

        newKnowledgeSectionHeader.TitleText.text = knowledgeBaseDataSection.knowledgeDataSectionName;
        knowledgeBaseNodes.Add(newKnowledgeSectionHeader);
        knowledgeSectionNodes.Add(newKnowledgeSectionHeader);

        foreach (KnowledgeBaseDataNode knowledgeDataNode in knowledgeBaseDataSection.KnowledgeBaseDataNodesList)
        {
            BuildKnowledgeBaseNode(knowledgeDataNode, newKnowledgeSectionHeader.ContentPanel);       
        }

    }
    private void BuildKnowledgeBaseNode(KnowledgeBaseDataNode knowledgeBaseDataNode, Transform sectionContent)
    {
        KnowledgeNode newKnowledgeNode = Instantiate(exampleKnowledgeNode, sectionContent);

        newKnowledgeNode.TitleText.text = knowledgeBaseDataNode.titleText;
        newKnowledgeNode.ContentText.text = knowledgeBaseDataNode.contentInfoText;
        knowledgeBaseNodes.Add(newKnowledgeNode);
    }

    private void DestroyPreviousNodes()
    {
        foreach (var node in knowledgeSectionNodes)
        {
            DestroyImmediate(node.gameObject);
        }
        knowledgeBaseNodes.Clear();
        knowledgeSectionNodes.Clear();
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
                if(knowledgeNode.GetComponent<KnowledgeSectionNode>()!=null)
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
