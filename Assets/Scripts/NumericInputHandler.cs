using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class NumericInputHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        if (inputField == null)
        {
            inputField = GetComponent<TMP_InputField>();
        }
        inputField.onSelect.AddListener(OpenNumericKeyboard);
    }

    private void OpenNumericKeyboard(string text)
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad);
    }
}
