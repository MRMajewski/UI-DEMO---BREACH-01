using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NeoScienceInfoPanel : AttributesInfoPanel
{
    [SerializeField]
    private NeoScienceDatabase neoScienceDatabase;

    [SerializeField] private UIElementSnapper generalSubPanel;
    [SerializeField] private NeoScienceSubPanel neoPhysicsSubPanel;
    [SerializeField] private NeoScienceSubPanel neoLinguisticSubPanel;
    [SerializeField] private NeoScienceSubPanel neoSensoricsSubPanel;
    [SerializeField] private NeoScienceSubPanel PsionicsSubPanel;

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
            List<ISnappedElement> snappedElements = new List<ISnappedElement>();
            snappedElements.Add((ISnappedElement)generalSubPanel);
            snappedElements.Add((ISnappedElement)neoPhysicsSubPanel);
            snappedElements.Add((ISnappedElement)neoSensoricsSubPanel);
            snappedElements.Add((ISnappedElement)PsionicsSubPanel);
            snappedElements.Add((ISnappedElement)neoLinguisticSubPanel);
            snapper.InitPanels(snappedElements);
        }
    }

    public void InitSubPanelsData()
    {

        neoPhysicsSubPanel.Setup(neoScienceDatabase.NeophysicsSpellsList);
        neoLinguisticSubPanel.Setup(neoScienceDatabase.NeoLinguisticsSpellsList);
        neoSensoricsSubPanel.Setup(neoScienceDatabase.NeoSensoricsSpellsList);
        PsionicsSubPanel.Setup(neoScienceDatabase.PsionicsSpellsList);
    }

    public override void DisablePanel()
    {
        base.DisablePanel();
        neoPhysicsSubPanel.CloseAllSpells();
        neoLinguisticSubPanel.CloseAllSpells();
        neoSensoricsSubPanel.CloseAllSpells();
        PsionicsSubPanel.CloseAllSpells();
    }
}
