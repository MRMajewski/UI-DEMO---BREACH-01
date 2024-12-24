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
        //isOpen = !isOpen;

        //if (isOpen)
        //{
        //    dropDownImage.sprite = closedSprite;

        //}
        //else
        //{
        //    dropDownImage.sprite = openSprite;
        //}
        base.SelectionClick();
        contentPanel.gameObject.SetActive(isOpen);

        //layoutGroup.enabled = false;
        //layoutGroup.enabled = true;

    }
}
