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
        //int totalRoll;
        //int diceRoll;
        //switch (currentMode)
        //{
        //    case DamageRollMode.Normal:
        //        diceRoll = RollNormal(1);
        //        break;
        //    case DamageRollMode.Advantage:
        //        diceRoll = RollAdvantage(1);
        //        break;
        //    case DamageRollMode.Disadvantage:
        //        diceRoll = RollDisadvantage(1);
        //        break;
        //    default:
        //        resultText.text = "Nieznany tryb rzutu.";
        //        return;
        //}

        //totalRoll = diceRoll + GetProficiencyBonus()+ modifier;
        //resultText.text = $"{diceRoll} + {modifier} + {GetProficiencyBonus()} = {totalRoll}";



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

        // Wyœwietlanie szczegó³ów
        resultText.text = $"{totalRoll} \n (Rzuty: {rollDetails}) + {GetProficiencyBonus()} + {modifier}";
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
    //protected (int total, string details) RollNormalDetails(int diceCount)
    //{
    //    int total = 0;
    //    List<int> rolls = new List<int>();
    //    for (int i = 0; i < diceCount; i++)
    //    {
    //        int roll = Random.Range(1, 7);
    //        rolls.Add(roll);
    //        total += roll;
    //    }

    //    string details = string.Join(", ", rolls);
    //    return (total, details);
    //}
    //protected (int total, string details) RollAdvantageDetails(int diceCount)
    //{
    //    List<int> rolls = new List<int>();
    //    for (int i = 0; i < diceCount * 2; i++)
    //    {
    //        rolls.Add(Random.Range(1, 7));
    //    }
    //    rolls.Sort((a, b) => b.CompareTo(a)); // Sortowanie malej¹ce

    //    int total = 0;
    //    List<int> selectedRolls = rolls.GetRange(0, diceCount); // Najwiêksze wartoœci
    //    foreach (int roll in selectedRolls)
    //    {
    //        total += roll;
    //    }

    //    //    string details = $"Wszystkie: [{string.Join(", ", rolls)}], Wybrane: [{string.Join(", ", selectedRolls)}]";
    //    string details = $"{string.Join(", ", rolls)}";
    //    return (total, details);
    //}

    //protected (int total, string details) RollDisadvantageDetails(int diceCount)
    //{
    //    List<int> rolls = new List<int>();
    //    for (int i = 0; i < diceCount * 2; i++)
    //    {
    //        rolls.Add(Random.Range(1, 7));
    //    }
    //    rolls.Sort(); // Sortowanie rosn¹ce

    //    int total = 0;
    //    List<int> selectedRolls = rolls.GetRange(0, diceCount); // Najmniejsze wartoœci
    //    foreach (int roll in selectedRolls)
    //    {
    //        total += roll;
    //    }

    //    //  string details = $"Wszystkie: [{string.Join(", ", rolls)}], Wybrane: [{string.Join(", ", selectedRolls)}]";
    //    string details = $"{string.Join(", ", rolls)}";
    //    return (total, details);
    //}
}
