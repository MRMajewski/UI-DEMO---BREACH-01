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
    [SerializeField] protected int diceAmount=1;
    [SerializeField] protected int modifierValue = 0;

    [SerializeField]
    protected int diceRange;

    public enum DamageRollMode
    {
        Normal,
        Advantage,
        Disadvantage
    }

    public enum DiceType
    {
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
     //   return diceRange;
    }

    protected void UpdateDiceRange()
    {

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

    // Funkcja wywo³uj¹ca rzut
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
     //   GetDiceRange();
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
      //  GetDiceRange();
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
     //   GetDiceRange();
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount * 2; i++)
        {
            rolls.Add(Random.Range(1, diceRange));
        }
        rolls.Sort((a, b) => b.CompareTo(a)); // Sortowanie malej¹ce

        int total = 0;
        List<int> selectedRolls = rolls.GetRange(0, diceCount); // Najwiêksze wartoœci
        foreach (int roll in selectedRolls)
        {
            total += roll;
        }

        //    string details = $"Wszystkie: [{string.Join(", ", rolls)}], Wybrane: [{string.Join(", ", selectedRolls)}]";
        string details = $"{string.Join(", ", rolls)}";
        return (total, details);
    }

    protected virtual (int total, string details) RollDisadvantageDetails(int diceCount)
    {
      //  GetDiceRange();
        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount * 2; i++)
        {
            rolls.Add(Random.Range(1, diceRange));
        }
        rolls.Sort(); // Sortowanie rosn¹ce

        int total = 0;
        List<int> selectedRolls = rolls.GetRange(0, diceCount); // Najmniejsze wartoœci
        foreach (int roll in selectedRolls)
        {
            total += roll;
        }

        //  string details = $"Wszystkie: [{string.Join(", ", rolls)}], Wybrane: [{string.Join(", ", selectedRolls)}]";
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
}
