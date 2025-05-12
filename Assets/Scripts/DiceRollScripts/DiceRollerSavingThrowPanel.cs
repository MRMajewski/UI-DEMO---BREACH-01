using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceRollerSavingThrowPanel : DiceRollerBasePanel
{
    protected ProficiencyType proficiencyType;
    protected int proficiencyMod;
    public enum ProficiencyType
    {
        NoBonus,
        Bonus2,
        Bonus3,
        Bonus4
    }

    public void OnProficiencyTypeChanged(int index)
    {
        proficiencyType = (ProficiencyType)index;      
    }

    private int GetProficiencyBonus()
    {
        switch (proficiencyType)
        {
            case ProficiencyType.NoBonus:
                proficiencyMod = 0;
                break;
            case ProficiencyType.Bonus2:
                proficiencyMod = 2;
                break;
            case ProficiencyType.Bonus3:
                proficiencyMod = 3;
                break;
            case ProficiencyType.Bonus4:
                proficiencyMod = 4;
                break;
            default:
                resultText.text = "Nieznany tryb rzutu.";
                return 0;
        }
        return proficiencyMod;
    }
    public override void RollDice()
    {    
        int modifier = ParseInput(modifierInputField.text);

        string rollDetails = "";
        int totalRoll = 0;

        switch (currentMode)
        {
            case DamageRollMode.Normal:
                (totalRoll, rollDetails) = RollNormalDetails(diceAmount);
                break;
            case DamageRollMode.Advantage:
                (totalRoll, rollDetails) = RollAdvantageDetails(diceAmount);
                break;
            case DamageRollMode.Disadvantage:
                (totalRoll, rollDetails) = RollDisadvantageDetails(diceAmount);
                break;
            default:
                resultText.text = "Nieznany tryb rzutu.";
                return;
        }
        totalRoll = totalRoll+modifier + GetProficiencyBonus();

        resultText.text = $"<size=200%>{totalRoll}</size><br>(Rzuty: {rollDetails}) + {GetProficiencyBonus()} + {modifier}";

        RollDiceAnimation();
    }
}
