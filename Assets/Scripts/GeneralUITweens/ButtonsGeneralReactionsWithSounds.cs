using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsGeneralReactionsWithSounds : ButtonsGeneralReactions
{
    [SerializeField]
    private string OnSelectSFX ="";

    [SerializeField]
    private string OnHoverSFX="";


    public new void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
    }

    public new void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
    }

    public new void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }

    public new void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }
}

