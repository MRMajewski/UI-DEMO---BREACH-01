using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "NewClassData", menuName = "Classes/ClassData")]
public class ClassData : ScriptableObject, ISnapperPanelData
{
    public string className;

    [Header("Reduction Dice Infos")]
    public ReductionDiceType reductionDiceInfo;

    [Header("Saving Throw Infos")]
    public SavingThrowType savingThrowInfo;

    [Header("Ability Infos")]
    public List<AbilityType> abilityInfos;

    [TextArea(1, 10)]
    public string classDescription;
    public Sprite classIcon;

    public Color classColor;

    [Header("Ability Infos")]
    public List<LevelUpTable> levelUpTable;

    [Header("Subclasses Infos")]
    public List<SubClassInfo> subClassesTable;

    public Sprite GetMiniatureIcon()
    {
        return classIcon;
    }
    public Color GetMiniatureIconColor()
    {
        if(classColor == null)
            return Color.white;
        return classColor;
    }
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

[System.Serializable]
public struct SubClassInfo
{
    public string subClassName;
    [TextArea(1, 10)]
    public string subClassDescription;
}


