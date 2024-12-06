using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceRollerSavingThrowPanel : DiceRollerBasePanel
{
    [SerializeField] protected ProficiencyType proficiencyType;
    [SerializeField] protected int proficiencyMod;
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
      //  modifier =+ GetProficiencyBonus();
        int totalRoll;
        int diceRoll;
        switch (currentMode)
        {
            case DamageRollMode.Normal:
                diceRoll = RollNormal(1);
                break;
            case DamageRollMode.Advantage:
                diceRoll = RollAdvantage(1);
                break;
            case DamageRollMode.Disadvantage:
                diceRoll = RollDisadvantage(1);
                break;
            default:
                resultText.text = "Nieznany tryb rzutu.";
                return;
        }

        totalRoll = diceRoll + GetProficiencyBonus()+ modifier;
        resultText.text = $"{diceRoll} + {modifier} + {GetProficiencyBonus()} = {totalRoll}";
    }
    protected override int RollNormal(int diceCount)
    { 
        int total = 0;
        for (int i = 0; i < diceCount; i++)
        {
            total += Random.Range(1, 21);
        }
        return total;
    }

    protected override int RollAdvantage(int diceCount)
    {
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount * 2; i++)
        {
            rolls.Add(Random.Range(1, 21));
        }
        rolls.Sort((a, b) => b.CompareTo(a));
        int total = 0;
        for (int i = 0; i < diceCount; i++)
        {
            total += rolls[i];
        }
        return total;
    }

    protected override int RollDisadvantage(int diceCount)
    {
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount * 2; i++)
        {
            rolls.Add(Random.Range(1, 21));
        }
        rolls.Sort();
        int total = 0;
        for (int i = 0; i < diceCount; i++)
        {
            total += rolls[i];
        }
        return total;
    }
}
