using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISnapperPanelData
{
    public Sprite GetMiniatureIcon();

    public Color GetMiniatureIconColor();
}

public interface ISnappedElement
{
    public RectTransform GetRectTransform();
    public RectTransform GetContentTransform();
    public void ResetRectScroll();
}

