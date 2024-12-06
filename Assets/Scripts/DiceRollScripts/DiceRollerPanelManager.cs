using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiceRollerPanelManager : MonoBehaviour
{

    [SerializeField]
    private List<CanvasGroup> diceRollerPanels; // Lista paneli, które maj¹ byæ zarz¹dzane

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
                            // Powrót do oryginalnego rozmiaru
                            buttonGameObject.transform.DOScale(1f, .15f).SetEase(Ease.InOutBounce);
                        });
    }
}
