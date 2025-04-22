using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "NeoScience/PsionicsSpellData")]
public class PsionicsSpellData : SpellData
{
    public TypesCategory Spellcategory;
    public PsionicsCategory spellType;
    public int spellCost;

    private void OnEnable()
    {
        neoScienceCategory = NeoScienceCategory.Psionics; 
    }
}
