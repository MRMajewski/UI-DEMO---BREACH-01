using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollerMiscPanel : DiceRollerBasePanel
{
    //[SerializeField]
    //private DiceType currentDiceType;

    //[SerializeField]
    //private int diceRange;

    [SerializeField]
    private Image currentDiceImage;
    [SerializeField] protected TMP_Dropdown typeDropdown;

    public void OnDiceTypeChanged(int index)
    {
        diceType = (DiceType)index;
        currentDiceImage.sprite = typeDropdown.options[index].image;
        GetDiceRange();
    }


    public override void RollDice()
    {
        int modifier = ParseInput(modifierInputField.text);
      //  int diceRoll = 0;
        int totalRoll=0;
        string rollDetails = "";

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
      //  totalRoll = diceRoll + modifier;
        totalRoll += modifier;

        //// Wyœwietlanie szczegó³ów
        resultText.text = $"{totalRoll} \n (Rzuty: {rollDetails}) + {modifier}";

     //   resultText.text = $"{diceRoll} + {modifier}  = {totalRoll}";
        //  resultText.text = $" {totalRoll}";




        //string rollDetails = "";
        //int totalRoll = 0;

        //switch (currentMode)
        //{
        //    case DamageRollMode.Normal:
        //        (totalRoll, rollDetails) = RollNormalDetails(diceAmount);
        //        break;
        //    case DamageRollMode.Advantage:
        //        (totalRoll, rollDetails) = RollAdvantageDetails(diceAmount);
        //        break;
        //    case DamageRollMode.Disadvantage:
        //        (totalRoll, rollDetails) = RollDisadvantageDetails(diceAmount);
        //        break;
        //    default:
        //        resultText.text = "Nieznany tryb rzutu.";
        //        return;
        //}

        //totalRoll += modifier;

        //// Wyœwietlanie szczegó³ów
        //resultText.text = $"{totalRoll} \n (Rzuty: {rollDetails}) + {modifier}";

    }
    protected override int RollNormal(int diceCount)
    {
        int total = 0;
        for (int i = 0; i < diceCount; i++)
        {
            total += Random.Range(1, diceRange);
        }
        return total;
    }

    protected override int RollAdvantage(int diceCount)
    {
        GetDiceRange();
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount * 2; i++)
        {
            rolls.Add(Random.Range(1, diceRange));
        }
        rolls.Sort((a, b) => b.CompareTo(a));
        int total = 0;
        for (int i = 0; i < diceCount; i++)
        {
            total += rolls[i];
        }
        string details = $"{string.Join(", ", rolls)}";
        return total;
    }

    protected override int RollDisadvantage(int diceCount)
    {
        GetDiceRange();
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount * 2; i++)
        {
            rolls.Add(Random.Range(1, diceRange));
        }
        rolls.Sort();
        int total = 0;
        for (int i = 0; i < diceCount; i++)
        {
            total += rolls[i];
        }
        string details = $"{string.Join(", ", rolls)}";
        return total;
    }
}
