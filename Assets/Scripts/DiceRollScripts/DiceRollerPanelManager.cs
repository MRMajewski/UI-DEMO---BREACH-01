using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiceRollerPanelManager : SimpleUIPanelMobiles
{
    [SerializeField]
    private List<CanvasGroup> diceRollerPanels; 
    public override void DisablePanel()
    {
        panelsCanvasGroup.DOFade(0, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine).
        OnComplete(() => this.gameObject.SetActive(false));
    }

    public override void InitializePanel()
    {
        ShowPanel(diceRollerPanels[0]);
    }

    public override void EnablePanel()
    {
        this.gameObject.SetActive(true);

        panelsCanvasGroup.DOFade(1, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine);
    }
    public void ShowPanel(CanvasGroup panelToShow)
    {
        foreach (var panel in diceRollerPanels)
        {
           if(panel == panelToShow)
            {
                panel.alpha = 1;
                panel.interactable = true;
                panel.blocksRaycasts = true;
            }
           else
            {
                panel.alpha = 0;
                panel.interactable = false;
                panel.blocksRaycasts = false;
            }
        }
    }

    public void OnRollClickTween(GameObject buttonGameObject)
    {
        buttonGameObject.transform.DOScale(1.15f,0.15f).SetEase(Ease.InOutBounce)
                        .OnComplete(() =>
                        {
                            buttonGameObject.transform.DOScale(1f, .15f).SetEase(Ease.InOutBounce);
                        });
    }
}
