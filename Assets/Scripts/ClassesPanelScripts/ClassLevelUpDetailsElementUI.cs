using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class ClassLevelUpDetailsElementUI : ClassDetailsElementUI
{
    [SerializeField]
    private TextMeshProUGUI levelIndexText;
    [SerializeField]
    private TextMeshProUGUI levelProficiencyBonusText;

    [SerializeField]
    private List<TooltipTriggerGameObjectAdder> triggerAdders;
    public List<TooltipTriggerGameObjectAdder> TriggerGameObjectAdder { get { return triggerAdders; } }

    public void Initialize(LevelUpTable data)
    {
        levelIndexText.text = data.level.ToString();

        SimpleDescription.text = data.skills;

        levelProficiencyBonusText.text = "+ " + data.proficiencyBonus;

        DetailedDescriptionText.text = data.skillsDescription;

        DetailedDescriptionGameObject.SetActive(isOpen);
    }

}
