using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISimpleUIPanelMobiles
{
    public void EnablePanel();
    public void DisablePanel();
    public  List<IUISelectionElement> SelectionQueue { get; }
}
