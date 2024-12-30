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
    
    [SerializeField] private TextMeshProUGUI openingText;
    [SerializeField] private float typeSpeed = 0.1f; 
    [SerializeField] private float bounceDuration = 0.5f;   
    private string fullText = "";

    [SerializeField]
    private bool isAfterOpeningAnim=false;

    private void Init()
    {
        if(!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }
        isAfterOpeningAnim = false;
        panelsCanvasGroup.DOFade(1, blinkDuration).SetEase(Ease.InOutSine);

        AnimateOpeningText();

        continueTextGameObject.alpha = 1;

       continueTextGameObject.DOFade(0, SimpleUIPanelMobilesManager.Instance.TransitionTime)
           .SetLoops(-1,LoopType.Yoyo)
           .SetEase(Ease.InOutSine);
    }
    private void AnimateOpeningText()
    {
        fullText=openingText.text;
        Sequence textSequence = DOTween.Sequence();
        openingText.text = ""; 

        for (int i = 0; i < fullText.Length; i++)
        {
            int charIndex = i; 
            textSequence.AppendCallback(() =>
            {
                openingText.text += fullText[charIndex]; 
                openingText.alpha = 0; 
                openingText.DOFade(1, typeSpeed / 2).SetEase(Ease.InOutSine);
            });
            textSequence.AppendInterval(typeSpeed); 
        }
        textSequence.Append(openingText.transform.DOScale(1.1f, bounceDuration)
              .SetEase(Ease.OutFlash)
              .SetLoops(1, LoopType.Yoyo))
              .OnComplete(() =>
              {
                  openingText.transform.localScale = Vector3.one;
                  isAfterOpeningAnim = true;
              });    
    }

    public override void EnablePanel()
    {
        Init();
    }
    public void SwitchPanelFirst()
    {
        if(isAfterOpeningAnim)
        SimpleUIPanelMobilesManager.Instance.SwitchPanel("Main");
    }

    public override void DisablePanel()
    {   
        continueTextGameObject.DOKill(false);

        panelsCanvasGroup.DOFade(0, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine).
        OnComplete(() => this.gameObject.SetActive(false));
        continueTextGameObject.alpha = 0;
    }
}
