using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class NeoScienceInfoPanel : AttributesInfoPanel
{
    [SerializeField]
    private NeoScienceDatabase neoScienceDatabase;

    [SerializeField] private List<NeoScienceSubPanel> subPanels;

    [SerializeField] private NeoScienceSubPanel neoPhysicsSubPanel;
    [SerializeField] private NeoScienceSubPanel neoLinguisticSubPanel;
    [SerializeField] private NeoScienceSubPanel neoSensoricsSubPanel;
    [SerializeField] private NeoScienceSubPanel PsionicsSubPanel;

    public override void InitializePanel()
    {
        base.InitializePanel();
    }


    [ContextMenu("Initialize Sub Panels Data")]
    public void InitSubPanelsData()
    {

        neoPhysicsSubPanel.Setup(neoScienceDatabase.NeophysicsSpellsList);
        neoLinguisticSubPanel.Setup(neoScienceDatabase.NeoLinguisticsSpellsList);
        neoSensoricsSubPanel.Setup(neoScienceDatabase.NeoSensoricsSpellsList);
        PsionicsSubPanel.Setup(neoScienceDatabase.PsionicsSpellsList);
    }
}
