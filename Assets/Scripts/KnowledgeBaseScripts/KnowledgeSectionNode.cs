using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KnowledgeSectionNode : KnowledgeNodeBase
{
    [SerializeField]
    private bool isSectionName = false;
    public bool IsSectionName { get => isSectionName; }

    [SerializeField]
    private Transform contentPanel;

    public Transform ContentPanel { get => contentPanel; }

    public override void SelectionClick()
    {
        base.SelectionClick();
        contentPanel.gameObject.SetActive(isOpen);

        if (isOpen)
            CheckVisibilityDelayed();
    }
}
