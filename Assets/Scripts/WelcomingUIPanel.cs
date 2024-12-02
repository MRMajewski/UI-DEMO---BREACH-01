using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WelcomingUIPanel : SimpleUIPanelMobiles
{
    [SerializeField]
    private CanvasGroup panelCanvasGroup;
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

        panelCanvasGroup.DOFade(1, blinkDuration).SetEase(Ease.InOutSine);

       continueTextGameObject.DOFade(0, blinkDuration)
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

        panelCanvasGroup.DOFade(0, blinkDuration).SetEase(Ease.InOutSine).
           OnComplete(() => this.gameObject.SetActive(false));
        
    }
}
