using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollerMiscPanel : DiceRollerBasePanel
{
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
        totalRoll += modifier;

        resultText.text = $"{totalRoll} \n (Rzuty: {rollDetails}) + {modifier}";
    }
}
