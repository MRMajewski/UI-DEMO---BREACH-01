using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceRollerBasePanel : MonoBehaviour
{
    [SerializeField] protected TMP_InputField diceInputField;
    [SerializeField] protected DiceType diceType;
    [SerializeField] protected TMP_InputField modifierInputField;
    [SerializeField] protected TMP_Dropdown modeDropdown;
    [SerializeField] protected TextMeshProUGUI resultText;


    protected int diceAmount=1; 
    protected int modifierValue = 0;
    protected int diceRange;

    public enum DamageRollMode
    {
        Normal,
        Advantage,
        Disadvantage
    }

    public enum DiceType
    {
        DiceD4,
        DiceD6,
        DiceD8,
        DiceD10,
        DiceD12,
        DiceD20
    }

    [SerializeField]
    protected DamageRollMode currentMode;

    private void Start()
    {
        modeDropdown.onValueChanged.AddListener(OnModeChanged);
        OnModeChanged(modeDropdown.value);
        GetDiceRange();
    }

    public void OnModeChanged(int index)
    {
        currentMode = (DamageRollMode)index;
    }

    protected void GetDiceRange()
    {
        switch (diceType)
        {
            case DiceType.DiceD4:
                diceRange = 5;
                break;
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
                return;
        }
    }

    public void IncreaseModifierValue()
    {
        modifierValue = modifierValue + 1;
        modifierInputField.text = modifierValue.ToString();
    }

    public void DecreaseModifierValue()
    {
        modifierValue = modifierValue - 1;
        if (modifierValue < 0)
            modifierValue = 0;
        modifierInputField.text = modifierValue.ToString();
    }

    public virtual void RollDice()
    {
        diceAmount = ParseInput(diceInputField.text);
        modifierValue = ParseInput(modifierInputField.text);

        if (diceAmount <= 0)
        {
            resultText.text = "WprowadŸ poprawn¹ liczbê koœci.";
            return;
        }

        int totalRoll;

        switch (currentMode)
        {
            case DamageRollMode.Normal:
                totalRoll = RollNormal(diceAmount);
                break;
            case DamageRollMode.Advantage:
                totalRoll = RollAdvantage(diceAmount);
                break;
            case DamageRollMode.Disadvantage:
                totalRoll = RollDisadvantage(diceAmount);
                break;
            default:
                resultText.text = "Nieznany tryb rzutu.";
                return;
        }
        totalRoll = totalRoll + modifierValue;
        resultText.text = $"Wynik ({currentMode}): {totalRoll}";
    }


    protected virtual int RollNormal(int diceCount)
    {
        int total = 0;
        for (int i = 0; i < diceCount; i++)
        {
            total += Random.Range(1, diceRange);
        }
        return total;
    }

    protected virtual int RollAdvantage(int diceCount)
    {
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
        return total;
    }

    protected virtual int RollDisadvantage(int diceCount)
    {
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
        return total;
    }

    protected virtual (int total, string details) RollNormalDetails(int diceCount)
    {
        int total = 0;
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount; i++)
        {
            int roll = Random.Range(1, diceRange);
            rolls.Add(roll);
            total += roll;
        }

        string details = string.Join(", ", rolls);
        return (total, details);
    }
    protected virtual (int total, string details) RollAdvantageDetails(int diceCount)
    {
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount * 2; i++)
        {
            rolls.Add(Random.Range(1, diceRange));
        }
        rolls.Sort((a, b) => b.CompareTo(a)); 

        int total = 0;
        List<int> selectedRolls = rolls.GetRange(0, diceCount); 
        foreach (int roll in selectedRolls)
        {
            total += roll;
        }

        string details = $"{string.Join(", ", rolls)}";
        return (total, details);
    }

    protected virtual (int total, string details) RollDisadvantageDetails(int diceCount)
    {
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount * 2; i++)
        {
            rolls.Add(Random.Range(1, diceRange));
        }
        rolls.Sort(); 

        int total = 0;
        List<int> selectedRolls = rolls.GetRange(0, diceCount); 
        foreach (int roll in selectedRolls)
        {
            total += roll;
        }

        string details = $"{string.Join(", ", rolls)}";
        return (total, details);
    }

    protected int ParseInput(string input)
    {
        if (int.TryParse(input, out int result))
        {
            return result;
        }
        return 0;
    }

    public void OnModifierValueChanged(string text)
    {
        if (int.TryParse(text, out int value))
        {
            if (value < 0) value = 0;
            if (value > 99) value = 99;
            modifierInputField.text = value.ToString();
           
        }
        else
        {
            modifierInputField.text = "0";
        }
        modifierValue = ParseInput(modifierInputField.text);
    }
}
