using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AttributesInfoPanel : SimpleUIPanelMobiles
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
    protected List<UIElementSnapper> snapperElementsList;

    public override void InitializePanel()
    {
        CastSnappedElements();

        snapper.OnPanelChanged += iconChanger.SetAlphaForIndex;

        foreach (Button icon in iconButtons)
        {
            int index = iconButtons.IndexOf(icon);
            icon.onClick.AddListener(() => snapper.SnapToPanelFromButton(index));
        }
        InitializeCategory(0);

        void CastSnappedElements()
        {
            List<ISnappedElement> snappedElements = snapperElementsList.Cast<ISnappedElement>().ToList();

            snapper.InitPanels(snappedElements);
        }
    }

    public void InitializeCategory(int index)
    {
        iconButtons[index].onClick.Invoke();
    }

    public override void DisablePanel()
    {
        snapper.ResetAllScrolls();
        base.DisablePanel();     
    }
}
