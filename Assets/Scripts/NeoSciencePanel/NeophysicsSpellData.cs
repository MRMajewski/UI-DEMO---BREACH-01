using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNeophysicsSpellData", menuName = "NeoScience/NeophysicsSpell")]
public class NeophysicsSpellData : SpellData
{

    public NeoPhysicsCategory spellType;
    public string spellCastingTime;
    public string spellRange;
    private void OnEnable()
    {
        neoScienceCategory = NeoScienceCategory.Neolinguistics; // Przypisz odpowiedni¹ wartoœæ
    }
}

