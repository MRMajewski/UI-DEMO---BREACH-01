using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "NeoScience/NeoSensoricsSpellData")]
public class NeoSensoricsSpellData : SpellData
{
    public TypesCategory Spellcategory;
    public NeoSensoricsCategory spellType;
    public int spellCost;

    private void OnEnable()
    {
        neoScienceCategory = NeoScienceCategory.Neosensorics; 
    }
}
