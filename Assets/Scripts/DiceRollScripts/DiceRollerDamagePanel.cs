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
        diceType = DiceType.DiceD6;
        GetDiceRange();
    }

    public void OnDiceValueChanged(string text)
    {
        if (int.TryParse(text, out int value))
        {
            if (value < 0) value = 0;
            if (value > 10) value = 10;
            diceInputField.text = value.ToString();
        }
        else
        {
            diceInputField.text = "0";
        }
        diceAmount = ParseInput(diceInputField.text);
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
            resultText.text = "Wprowad� poprawn� liczb� ko�ci.";
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

        RollDiceAnimation();
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
