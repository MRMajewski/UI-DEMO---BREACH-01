using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class SimpleUIPanelMobiles : MonoBehaviour, ISimpleUIPanelMobiles
{
    [SerializeField]
    protected CanvasGroup panelsCanvasGroup;
    public List<IUISelectionElement> SelectionQueue => throw new System.NotImplementedException();

    public virtual void DisablePanel()
    {
        TooltipManager.Instance.HideAllTooltips();
        panelsCanvasGroup.DOFade(0, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine).
        OnComplete(() => this.gameObject.SetActive(false));

       
    }

    public virtual void EnablePanel()
    {
        this.gameObject.SetActive(true);

        panelsCanvasGroup.DOFade(1, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine);
    }
    public virtual void InitializePanel()
    {

    }

}
