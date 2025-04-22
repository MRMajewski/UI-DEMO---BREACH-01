using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class TrainingNode : KnowledgeNodeBase
{
    [SerializeField]
    private TextMeshProUGUI contentText;

    [SerializeField]
    public TextMeshProUGUI ContentText { get => contentText; set => contentText = value; }

    [SerializeField]
    protected Button questionButton;

    [SerializeField]
    public Button QuestionButton { get => questionButton; set => questionButton = value; }

    [SerializeField]
    private List<TooltipTriggerGameObjectAdder> triggerAdders;
    public List<TooltipTriggerGameObjectAdder> TriggerGameObjectAdder { get { return triggerAdders; } }

    public override void SelectionClick()
    {
        base.SelectionClick();
        contentText.gameObject.SetActive(isOpen);

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.transform.parent.GetComponent<RectTransform>());

        CheckVisibilityDelayed();
    }

    private void Start()
    {
        contentText.gameObject.SetActive(false);
    }
}
