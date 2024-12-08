using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollerMiscPanel : DiceRollerBasePanel
{
    //[SerializeField]
    //private DiceType currentDiceType;

    [SerializeField]
    private int diceRange;

    [SerializeField]
    private Image currentDiceImage;
    [SerializeField] protected TMP_Dropdown typeDropdown;

    public void OnDiceTypeChanged(int index)
    {
        diceType = (DiceType)index;
        currentDiceImage.sprite = typeDropdown.options[index].image;
    }

    private int GetDiceRange()
    {
        switch (diceType)
        {
            case DiceType.DiceD6:
                diceRange = 7;
                break;
            case DiceType.DiceD8:
                diceRange = 9;
                break;
            case DiceType.DiceD10:
                diceRange = 11;
                break;
            case DiceType.DiceD12:
                diceRange = 13;
                break;
            case DiceType.DiceD20:
                diceRange = 21;
                break;
            default:
                resultText.text = "Nieznany tryb rzutu.";
                return 0;
        }
        return diceRange;
    }


    public override void RollDice()
    {
        int modifier = ParseInput(modifierInputField.text);
        int diceRoll = 0;
        int totalRoll;

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
        totalRoll = diceRoll + modifier;


        resultText.text = $"{diceRoll} + {modifier}  = {totalRoll}";
      //  resultText.text = $" {totalRoll}";
    }
    protected override int RollNormal(int diceCount)
    {
        int total = 0;
        for (int i = 0; i < diceCount; i++)
        {
            total += Random.Range(1, GetDiceRange());
        }
        return total;
    }

    protected override int RollAdvantage(int diceCount)
    {
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount * 2; i++)
        {
            rolls.Add(Random.Range(1, GetDiceRange()));
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
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount * 2; i++)
        {
            rolls.Add(Random.Range(1, GetDiceRange()));
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
