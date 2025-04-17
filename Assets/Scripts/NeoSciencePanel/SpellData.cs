using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class SpellData : ScriptableObject
{
    public NeoScienceCategory neoScienceCategory;

    public string spellName;

    public int spellLevel;

    [TextArea(1, 10)]
    public string description;
}

[CreateAssetMenu(fileName = "NewItemData", menuName = "NeoScience/NeoLinguisticsSpellData")]
public class NeoLinguisticsSpellData : SpellData
{
    private void OnEnable()
    {
        neoScienceCategory = NeoScienceCategory.Neolinguistics; // Przypisz odpowiedni¹ wartoœæ
    }
}
[CreateAssetMenu(fileName = "NewItemData", menuName = "NeoScience/NeoSensoricsSpellData")]
public class NeoSensoricsSpellData : SpellData
{
    public NeoSensoricsCategory subCategory;
    private void OnEnable()
    {
        neoScienceCategory = NeoScienceCategory.Neosensorics; // Przypisz odpowiedni¹ wartoœæ
    }
}
[CreateAssetMenu(fileName = "NewItemData", menuName = "NeoScience/PsionicsSpellData")]
public class PsionicsSpellData : SpellData
{
    public PsionicsCategory subCategory;
    private void OnEnable()
    {
        neoScienceCategory = NeoScienceCategory.Psionics; // Przypisz odpowiedni¹ wartoœæ
    }
}

public enum NeoScienceCategory
{
    Neophysics,
    Neolinguistics,
    Neosensorics,
    Psionics
}

public enum NeoPhysicsCategory
{
    Grawiturgia,
    Chronoturgia,
    Energodynamika,
    Neooptyka,
    Neoakustyka,
    Inne
}
public enum NeoSensoricsCategory
{
    General,
    Precognition,
    Biohacking,
    Spiritism

}
public enum PsionicsCategory
{
    General,
    Telekinesis,
    Pyrokinesis,
    Psychics
}

