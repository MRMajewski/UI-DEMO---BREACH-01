using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISnapperPanelElement
{
    public Sprite GetMiniatureIcon();

}

public interface ISnapperPanel
{
    public void ResizeSnappedPanel();
}

