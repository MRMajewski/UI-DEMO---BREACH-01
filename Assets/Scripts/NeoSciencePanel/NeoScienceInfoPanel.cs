using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class NeoScienceInfoPanel : AttributesInfoPanel
{
    [SerializeField]
    private NeoScienceDatabase neoScienceDatabase;

    [SerializeField] private List<NeoScienceSubPanel> subPanels;
    public override void InitializePanel()
    {
        base.InitializePanel();
    }


    [ContextMenu("Initialize Sub Panels Data")]
    public void InitSubPanelsData()
    {

        subPanels[0].Setup(neoScienceDatabase.NeophysicsSpellsList);
        //foreach (NeoScienceSubPanel subPanel in subPanels)
        //{
        //    subPanel.Setup();
        //}
    }
}
