using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNeophysicsSpellData", menuName = "NeoScience/NeophysicsSpell")]
public class NeophysicsSpellData : SpellData
{
    public int spellLevel;
    public NeoPhysicsCategory spellType;
    public string spellCastingTime;
    public string spellRange;
    private void OnEnable()
    {
        neoScienceCategory = NeoScienceCategory.Neophysics; // Przypisz odpowiedni¹ wartoœæ
    }
}

