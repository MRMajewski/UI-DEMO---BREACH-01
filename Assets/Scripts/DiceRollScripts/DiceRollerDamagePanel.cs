using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceRollerDamagePanel : DiceRollerBasePanel
{

    private void Start()
    {
        modeDropdown.onValueChanged.AddListener(OnModeChanged);
        OnModeChanged(modeDropdown.value);
        diceInputField.text =diceAmount.ToString();
        GetDiceRange();
    }
    public override void RollDice()
    {
        diceAmount = ParseInput(diceInputField.text);

        if(diceAmount>10)
        {
            diceAmount = 10;
            diceInputField.text=diceAmount.ToString();
        }
        int modifier = ParseInput(modifierInputField.text);

        if (diceAmount <= 0)
        {
            resultText.text = "WprowadŸ poprawn¹ liczbê koœci.";
            return;
        }

        string rollDetails="";
        int totalRoll=0;

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

        totalRoll += modifier;

        // Wyœwietlanie szczegó³ów
        resultText.text = $"{totalRoll} \n (Rzuty: {rollDetails}) + {modifier}";
    }

    public void IncreaseDiceAmount()
    {
        diceAmount = diceAmount + 1;
        if (diceAmount > 10)
        {
            diceAmount = 10;
         
        }
        diceInputField.text = diceAmount.ToString();
    }
    public void DecreaseDiceAmount()
    {
        diceAmount = diceAmount - 1;
        if (diceAmount < 1)
        {
            diceAmount = 1;
          
        }
        diceInputField.text = diceAmount.ToString();
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
    //protected  (int total, string details) RollAdvantageDetails(int diceCount)
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

    ////    string details = $"Wszystkie: [{string.Join(", ", rolls)}], Wybrane: [{string.Join(", ", selectedRolls)}]";
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

    //  //  string details = $"Wszystkie: [{string.Join(", ", rolls)}], Wybrane: [{string.Join(", ", selectedRolls)}]";
    //    string details = $"{string.Join(", ", rolls)}";
    //    return (total, details);
    //}
    //public override void RollDice()
    //{
    //    int diceCount = ParseInput(diceInputField.text);
    //    int modifier = ParseInput(modifierInputField.text);

    //    if (diceCount <= 0)
    //    {
    //        resultText.text = "WprowadŸ poprawn¹ liczbê koœci.";
    //        return;
    //    }

    //    int totalRoll;

    //    switch (currentMode)
    //    {
    //        case DamageRollMode.Normal:
    //            totalRoll = RollNormal(diceCount);
    //            break;
    //        case DamageRollMode.Advantage:
    //            totalRoll = RollAdvantage(diceCount);
    //            break;
    //        case DamageRollMode.Disadvantage:
    //            totalRoll = RollDisadvantage(diceCount);
    //            break;
    //        default:
    //            resultText.text = "Nieznany tryb rzutu.";
    //            return;
    //    }
    //    totalRoll = totalRoll + modifier;

    // //   totalRoll = diceRoll + GetProficiencyBonus() + modifier;
    // //   resultText.text = $"{diceRoll} + {modifier} + {GetProficiencyBonus()} = {totalRoll}";
    //    resultText.text = $"Wynik ({currentMode}): {totalRoll}";
    //}


    //protected override int RollNormal(int diceCount)
    //{
    //    int total = 0;
    //    for (int i = 0; i < diceCount; i++)
    //    {
    //        total += Random.Range(1, 7); 
    //    }
    //    return total;
    //}

    protected override int RollAdvantage(int diceCount)
    {
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount * 2; i++)
        {
            rolls.Add(Random.Range(1, 7));
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
            rolls.Add(Random.Range(1, 7));
        }
        rolls.Sort(); 
        int total = 0;
        for (int i = 0; i < diceCount; i++) 
        {
            total += rolls[i];
        }
        return total;
    }

    //protected override int ParseInput(string input)
    //{
    //    if (int.TryParse(input, out int result))
    //    {
    //        return result;
    //    }
    //    return 0;
    //}
}
