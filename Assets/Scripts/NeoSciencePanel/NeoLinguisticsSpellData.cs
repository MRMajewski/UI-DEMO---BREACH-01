using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "NeoScience/NeoLinguisticsSpellData")]
public class NeoLinguisticsSpellData : SpellData
{
    public int spellLevel;
    public string spellCastingTime;
    public string spellRange;
    private void OnEnable()
    {
        neoScienceCategory = NeoScienceCategory.Neolinguistics; 
    }
}
