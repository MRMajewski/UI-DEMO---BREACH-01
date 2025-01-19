using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class TooltipUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tooltipText;
    [SerializeField]
    private Button actionButton;

    [SerializeField]
    private CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroup=>canvasGroup;

    WaitForEndOfFrame wait = new WaitForEndOfFrame();

    [SerializeField]
    private GameObject gameObjectTooltip;
    public GameObject GameObjectTooltip { get => gameObjectTooltip; set => gameObjectTooltip = value; }

    public void SetTooltipText(string text, UnityAction buttonAction=null)
    {
        tooltipText.text = text;

        if (buttonAction!=null)
        {
            actionButton.gameObject.SetActive(true);
            actionButton.onClick.AddListener(buttonAction);
        }
        else
        {
            actionButton.gameObject.SetActive(false);
        }

    }

    public void ResizeTooltip()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipText.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
       // StartCoroutine(ResizeCoroutine());
    }
    IEnumerator ResizeCoroutine()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipText.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
        yield return wait;

    }

    public void HandleTooltipExit()
    {
        TooltipManager.Instance.HideTooltip(this);

    }
}
