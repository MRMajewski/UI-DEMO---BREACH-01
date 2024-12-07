using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WelcomingUIPanel : SimpleUIPanelMobiles
{
    [SerializeField]
    private CanvasGroup continueTextGameObject;

    [SerializeField] 
    private float blinkDuration = 0.5f;

    private void Init()
    {
        if(!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }

        panelsCanvasGroup.DOFade(1, blinkDuration).SetEase(Ease.InOutSine);

        continueTextGameObject.alpha = 1;

       continueTextGameObject.DOFade(0, SimpleUIPanelMobilesManager.Instance.TransitionTime)
           .SetLoops(-1,LoopType.Yoyo)
           .SetEase(Ease.InOutSine);
    }

    public override void EnablePanel()
    {
        Init();
    }

    public override void DisablePanel()
    {
        continueTextGameObject.DOKill(false);

        panelsCanvasGroup.DOFade(0, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine).SetUpdate(UpdateType.Normal).SetUpdate(true);
        //   OnComplete(() => this.gameObject.SetActive(false));
        continueTextGameObject.alpha = 0;
    }
}
