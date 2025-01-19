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
  //  [SerializeField] private UnityAction buttonAction; // Akcja przycisku

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

    //public void OnSelect(BaseEventData eventData)
    //{
    //    HandleTooltipEnter();
    //}

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    HandleTooltipEnter();
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    HandleTooltipExit();
    //}

    //public void OnDeselect(BaseEventData eventData)
    //{
    //    HandleTooltipExit();
    //}

    
    public void OnPointerClick(PointerEventData eventData)
    {
        HandleTooltipEnter();
    }

private void HandleTooltipEnter()
    {
        TooltipManager.Instance.InspectedRectTransform = GetComponent<RectTransform>();

        TooltipManager.Instance.CreateCurrentTooltip();

        if(this.actionName=="")
        {
            TooltipManager.Instance.CurrentTooltip.SetTooltipText(tooltipText, null);
        }
        else
        TooltipManager.Instance.CurrentTooltip.SetTooltipText(tooltipText, OnTriggerButtonAddAction);

        TooltipManager.Instance.CurrentTooltip.ResizeTooltip();


        // TooltipManager.Instance.RepositionToolTip();
        TooltipManager.Instance.ShowTooltip();
    }

    public void OnTriggerButtonAddAction()
    {
        TooltipManager.Instance.ExecuteAction(this.actionName);
    }

    private void HandleTooltipExit()
    {
      //  TooltipManager.Instance.HideTooltip();
          
    }

}
