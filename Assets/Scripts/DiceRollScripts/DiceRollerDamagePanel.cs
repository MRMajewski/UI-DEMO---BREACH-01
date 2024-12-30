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

        resultText.text = $"<size=200%>{totalRoll}</size><br> (Rzuty: {rollDetails}) + {modifier}";
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
}
