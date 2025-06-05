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

    public override void DisablePanel()
    {
        this.gameObject.SetActive(false);
    }

    public override void EnablePanel()
    {
        this.gameObject.SetActive(true);
        panelsCanvasGroup.DOFade(1, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine);
        OnStartCloseAll();
    }

    public void BuildKnowledgeBase()
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
                if (knowledgeNode.GetComponent<KnowledgeNodeBase>() != null)
                {
                    knowledgeNode.SelectionClick();
                }
            }
        }
        areAllTabsExpanded = !areAllTabsExpanded;
        RefreshExpandAllIconAndText();
    }

    private void OnStartCloseAll()
    {

        foreach (KnowledgeNodeBase knowledgeNode in knowledgeBaseNodes)
        {
            if (knowledgeNode.IsOpen == true)
            {
                if (knowledgeNode.GetComponent<KnowledgeNodeBase>() != null)
                {
                    knowledgeNode.SelectionClick();
                }
            }
            else
            {
                continue;
            }
        }
        areAllTabsExpanded = true;

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
