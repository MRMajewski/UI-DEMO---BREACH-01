using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KnowledgeNode : KnowledgeNodeBase
{
    [SerializeField]
    private TextMeshProUGUI contentText;

    [SerializeField]
    public TextMeshProUGUI ContentText { get => contentText; set => contentText = value; }

    [SerializeField]
    private Button questionButton;

    [SerializeField]
    public Button QuestionButton { get => questionButton; set => questionButton = value; }

    public override void SelectionClick()
    {
        base.SelectionClick();
        contentText.gameObject.SetActive(isOpen);

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.transform.parent.GetComponent<RectTransform>());

        CheckVisibilityDelayed();
    }
}
