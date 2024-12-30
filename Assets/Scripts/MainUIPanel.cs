using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIPanel : SimpleUIPanelMobiles
{
    [SerializeField]
    private List<Button> categoryButtons;

    [SerializeField]
    private Ease easeType;

    public override void DisablePanel()
    {
        panelsCanvasGroup.DOFade(0, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(easeType).
        OnComplete(() => this.gameObject.SetActive(false));
    }

    public override void EnablePanel()
    {
       this.gameObject.SetActive(true);

        panelsCanvasGroup.DOFade(1, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(easeType);
    }
}
