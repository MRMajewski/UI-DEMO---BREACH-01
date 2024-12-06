using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceRollerDamagePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField diceInputField; 
    [SerializeField] private TMP_InputField modifierInputField; 
    [SerializeField] private TMP_Dropdown modeDropdown; 
    [SerializeField] private TextMeshProUGUI resultText; 

    public enum DamageRollMode
    {
        Normal,
        Advantage,
        Disadvantage
    }

    [SerializeField]
    private DamageRollMode currentMode;

    private void Start()
    {
        modeDropdown.onValueChanged.AddListener(OnModeChanged);
        OnModeChanged(modeDropdown.value);
    }

    private void OnModeChanged(int index)
    {
        currentMode = (DamageRollMode)index;
    }

    // Funkcja wywo³uj¹ca rzut
    public void RollDice()
    {
        int diceCount = ParseInput(diceInputField.text);
        int modifier = ParseInput(modifierInputField.text);

        if (diceCount <= 0)
        {
            resultText.text = "WprowadŸ poprawn¹ liczbê koœci.";
            return;
        }

        int totalRoll;

        switch (currentMode)
        {
            case DamageRollMode.Normal:
                totalRoll = RollNormal(diceCount);
                break;
            case DamageRollMode.Advantage:
                totalRoll = RollAdvantage(diceCount);
                break;
            case DamageRollMode.Disadvantage:
                totalRoll = RollDisadvantage(diceCount);
                break;
            default:
                resultText.text = "Nieznany tryb rzutu.";
                return;
        }
        totalRoll = totalRoll + modifier;
        resultText.text = $"Wynik ({currentMode}): {totalRoll}";
    }


    private int RollNormal(int diceCount)
    {
        int total = 0;
        for (int i = 0; i < diceCount; i++)
        {
            total += Random.Range(1, 7); 
        }
        return total;
    }

    private int RollAdvantage(int diceCount)
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

    private int RollDisadvantage(int diceCount)
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

    private int ParseInput(string input)
    {
        if (int.TryParse(input, out int result))
        {
            return result;
        }
        return 0;
    }
}
