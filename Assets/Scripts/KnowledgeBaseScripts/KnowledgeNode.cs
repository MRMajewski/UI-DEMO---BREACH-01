using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class KnowledgeNode : MonoBehaviour
{
    [SerializeField]
    private bool isSectionName = false;
    public bool IsSectionName { get => isSectionName; }

    [SerializeField]
    private bool isOpen = false;
    public bool IsOpen { get => isOpen; }


    [Header("Text references")]
    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI contentText;

    //[SerializeField]
    //public Translate questionTranslate;

    //[SerializeField]
    //public Translate answerTranslate;

    [SerializeField]
    public TextMeshProUGUI TitleText { get => titleText; set => titleText = value; }

    [SerializeField]
    public TextMeshProUGUI ContentText { get => contentText; set => contentText = value; }

    [SerializeField]
    private Button questionButton;

    [SerializeField]
    public Button QuestionButton { get => questionButton; set => questionButton = value; }


    [Header("Image references")]
    [SerializeField]
    private Image dropDownImage;

    [SerializeField]
    private Sprite openSprite;

    [SerializeField]
    private Sprite closedSprite;

    public void SelectionClick()
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
        if (isSectionName)
        {

        }
        else
        {
            contentText.gameObject.SetActive(isOpen);
        }

    }
}
