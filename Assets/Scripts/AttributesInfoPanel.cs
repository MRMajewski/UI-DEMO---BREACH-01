using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesInfoPanel : SimpleUIPanelMobiles, ISnapperPanel
{
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

    public override void InitializePanel()
    {
        snapper.OnPanelChanged += iconChanger.SetAlphaForIndex;

        foreach (Button icon in iconButtons)
        {
            int index = iconButtons.IndexOf(icon);
            icon.onClick.AddListener(() => snapper.SnapToPanelFromButton(index));
        }
        //    iconChanger.SetAlphaForIndex(0);

        InitializeCategory(0);
        // iconButtons[0].onClick.Invoke();
       // ProcessTooltipTriggers();
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
        Debug.Log(viewportRect.rect.width);

        foreach (RectTransform classData in snapperPanelsList)
        {
            classData.sizeDelta =
                new Vector2(viewportRect.rect.width, classData.sizeDelta.y);
        }
    }
}
