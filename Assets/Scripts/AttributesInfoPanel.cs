using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesInfoPanel : SimpleUIPanelMobiles
{
    [SerializeField]
    private UIElementsSnapper snapper;

    [SerializeField]
    private MiniatureIconsChanger iconChanger;

    [SerializeField]
    private List<Button> iconButtons;

    public override void InitializePanel()
    {
        snapper.OnPanelChanged += iconChanger.SetAlphaForIndex;

        foreach (Button icon in iconButtons)
        {
            int index = iconButtons.IndexOf(icon);
            icon.onClick.AddListener(() => snapper.SnapToPanelFromButton(index));
        }
        iconChanger.SetAlphaForIndex(0);

        iconButtons[0].onClick.Invoke();
    }
}
