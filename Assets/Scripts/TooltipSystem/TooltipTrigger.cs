using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class TooltipTrigger : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TooltipType tooltipType;
    [SerializeField] private string tooltipText;
    [SerializeField] private string actionName="";

    private bool isTriggered = false;

    public TooltipType Type
    {
        get => tooltipType;
        set => tooltipType = value;
    }
    public string TooltipText
    {
        get => tooltipText;
        set => tooltipText = value;
    }
    public string ActionName
    {
        get => actionName;
        set => actionName = value;
    }
    public bool IsTriggered
    {
        get => isTriggered;
        set => isTriggered = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isTriggered)
        HandleTooltipEnter();
    }

private void HandleTooltipEnter()
    {
        isTriggered = true;
        TooltipManager.Instance.InspectedRectTransform = GetComponent<RectTransform>();
        TooltipManager.Instance.CurrentlyInspectedTrigger = this;

        TooltipManager.Instance.CreateCurrentTooltip(this);

        if(this.actionName=="")
        {
            TooltipManager.Instance.CurrentTooltip.SetTooltipTextAndAction(tooltipText, null);
        }
        else
        TooltipManager.Instance.CurrentTooltip.SetTooltipTextAndAction(tooltipText, OnTriggerButtonAddAction);

        TooltipManager.Instance.CurrentTooltip.ResizeTooltip();

        TooltipManager.Instance.ShowTooltip();
        isTriggered = true;
    }

    public void OnTriggerButtonAddAction()
    {
        TooltipManager.Instance.ExecuteAction(this.actionName);
    }

}
