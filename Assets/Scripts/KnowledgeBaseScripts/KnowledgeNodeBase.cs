using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class KnowledgeNodeBase : MonoBehaviour
{
    [SerializeField]
    protected bool isOpen = false;
    public bool IsOpen { get => isOpen; }

    [Header("Text references")]
    [SerializeField]
    protected TextMeshProUGUI titleText;

    [SerializeField]
    public TextMeshProUGUI TitleText { get => titleText; set => titleText = value; }

    [Header("Image references")]
    [SerializeField]
    protected Image dropDownImage;

    [SerializeField]
    protected Sprite openSprite;

    [SerializeField]
    protected Sprite closedSprite;

    [SerializeField]
    protected VerticalLayoutGroup layoutGroup;

    public virtual void SelectionClick()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            dropDownImage.sprite = closedSprite;

        }
        else
        {
            dropDownImage.sprite = openSprite;
        }
    }
}
