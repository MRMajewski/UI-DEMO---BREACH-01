using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleUIPanelMobiles : MonoBehaviour, ISimpleUIPanelMobiles
{
    [SerializeField]
    protected CanvasGroup panelsCanvasGroup;
    public List<IUISelectionElement> SelectionQueue => throw new System.NotImplementedException();

    public virtual void DisablePanel()
    {     
    }

    public virtual void EnablePanel()
    {    
    }

}
