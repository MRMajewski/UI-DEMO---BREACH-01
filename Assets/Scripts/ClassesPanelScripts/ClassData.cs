using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "NewClassData", menuName = "Classes/ClassData")]
public class ClassData : ScriptableObject
{
    public string className;

    [Header("Reduction Dice Infos")]
    public ReductionDiceType reductionDiceInfos;

    [Header("Saving Throw Infos")]
    public SavingThrowType savingThrowInfos;

    [Header("Ability Infos")]
    public List<AbilityType> abilityInfos;

    [TextArea(1, 10)]
    public string classDescription;
    public Sprite classIcon;

    [TextArea(1, 10)]
    public string[] subclasses;

    [Header("Ability Infos")]
    public List<LevelUpTable> levelUpTable;
}
[System.Serializable]
public struct LevelUpTable
{
    public int level;
    [TextArea(1, 10)]
    public string skills;
    public int proficiencyBonus; 
    [TextArea(1, 10)]
    public string skillsDescription;
}


