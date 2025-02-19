using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISnapperPanelData
{
    public Sprite GetMiniatureIcon();

}

public interface ISnapperPanelElement
{

}

public interface ISnapperPanel
{
    public void ResizeSnappedPanel();
}

