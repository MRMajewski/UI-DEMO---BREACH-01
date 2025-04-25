using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DiceRollerPanelManager : SimpleUIPanelMobiles, ISnapperPanel
{
    [SerializeField]
    private List<CanvasGroup> diceRollerPanels;

    //[SerializeField]
    //private Button firstTab;

    [Header("Snapper references")]
    [SerializeField]
    protected UIElementsSnapper snapper;

    [SerializeField]
    protected RectTransform viewportRect;

    [SerializeField]
    protected MiniatureIconsChanger iconChanger;

    [SerializeField]
    protected List<Button> iconButtons;

    [SerializeField]
    protected List<RectTransform> snapperPanelsList;

    public override void DisablePanel()
    {
        panelsCanvasGroup.DOFade(0, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine).
        OnComplete(() => this.gameObject.SetActive(false));
    }

    public override void InitializePanel()
    {

        snapper.OnPanelChanged += iconChanger.SetAlphaForIndex;

        foreach (Button icon in iconButtons)
        {
            int index = iconButtons.IndexOf(icon);
            icon.onClick.AddListener(() => snapper.SnapToPanelFromButton(index));
        }
        InitializeCategory(0);
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

    public void InitializeCategory(int index)
    {
        iconButtons[index].onClick.Invoke();
    }

    protected void OnEnable()
    {
        ResizeSnappedPanel();
    }
    public void ResizeSnappedPanel()
    {
        foreach (RectTransform classData in snapperPanelsList)
        {
            classData.sizeDelta =
                new Vector2(viewportRect.rect.width, classData.sizeDelta.y);
        }
    }
}
