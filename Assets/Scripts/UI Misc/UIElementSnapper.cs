using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIElementSnapper : MonoBehaviour, ISnappedElement
{
    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private RectTransform content;


    public RectTransform GetContentTransform()
    {
        return content;    
    }

    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }

    public void ResetRectScroll()
    {
        Vector2 targetPosition = new Vector2(content.anchoredPosition.x, 0f);
        content.DOAnchorPos(targetPosition, 0.3f).SetEase(Ease.InOutSine).SetUpdate(true);
    }
}
