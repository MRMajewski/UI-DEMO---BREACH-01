using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipInteractionHandler : MonoBehaviour, IPointerExitHandler, IDeselectHandler
{
    [SerializeField]
    private List<TooltipUI> tooltipsQueque;
    public List<TooltipUI> TooltipsQueque { get => tooltipsQueque; set => tooltipsQueque = value; }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetAllTooltips();
    }
    public void OnDeselect(BaseEventData eventData)
    {
        ResetAllTooltips();
    }

    private void ResetAllTooltips()
    {
        TooltipsQueque.Clear();
    }
}
