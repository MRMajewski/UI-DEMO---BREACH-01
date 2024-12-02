using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUISelectionElement
{
    IUISelectionElement GetFirstSelected();

    void EnableUIElement();

    void DisableUIElement();

    void Select();

    void Deselect();
}
