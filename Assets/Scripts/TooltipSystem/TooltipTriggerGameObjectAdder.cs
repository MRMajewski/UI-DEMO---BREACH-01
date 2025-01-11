using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TooltipTriggerGameObjectAdder : MonoBehaviour
{
    [SerializeField]
    private List<TooltipTriggerTextInfo> triggersList;

    [SerializeField]
    private GameObject tooltipPrefab;

    [SerializeField]
    private TextMeshProUGUI textMeshProText;

    private List<GameObject> createdTooltipObjects = new List<GameObject>();

    [ContextMenu("TEST")]
    public void Test()
    {
        ProcessText(textMeshProText);
    }

    private void Start()
    {
        ProcessText(textMeshProText);
    }

    public void ProcessText(TextMeshProUGUI newTextMeshPro)
    {
        // Zapisz nowy tekst w TextMeshProUGUI
        textMeshProText = newTextMeshPro;

        // Zaktualizuj dane
        textMeshProText.ForceMeshUpdate();

        // Przechodzimy przez s³owa w tekœcie
        string updatedText = textMeshProText.text;

        foreach (var trigger in triggersList)
        {
            string triggerName = trigger.TriggerName;
            string tooltipText = trigger.TooltipText;

            // Wyszukaj wszystkie wyst¹pienia triggerName w tekœcie
            var matches = FindWordMatches(triggerName);

            foreach (var match in matches)
            {
                // Otaguj znalezione s³owo <b></b>
                updatedText = AddBoldTagToText(updatedText, match);

                // Tworzymy nowy obiekt TooltipTrigger
                CreateTooltipTriggerObject(match, tooltipText);
            }
        }

        // Zaktualizuj tekst w TextMeshPro
        textMeshProText.text = updatedText;
    }

    private List<TMP_WordInfo> FindWordMatches(string triggerName)
    {
        List<TMP_WordInfo> matchingWords = new List<TMP_WordInfo>();

        for (int i = 0; i < textMeshProText.textInfo.wordCount; i++)
        {
            var wordInfo = textMeshProText.textInfo.wordInfo[i];
            if (wordInfo.GetWord().Equals(triggerName, System.StringComparison.OrdinalIgnoreCase))
            {
                matchingWords.Add(wordInfo);
            }
        }

        return matchingWords;
    }

    private string AddBoldTagToText(string text, TMP_WordInfo wordInfo)
    {
        string word = wordInfo.GetWord();

        // Sprawdzamy, czy s³owo nie jest ju¿ otagowane <b>...</b>
        string wordWithTag = "<b>" + word + "</b>";

        // Je¿eli ju¿ jest otagowane, zwróæ tekst bez zmian
        if (text.Contains(wordWithTag))
        {
            return text;
        }

        // Zastêpujemy tylko te s³owa, które nie zawieraj¹ tagu <b>
        return text.Replace(word, wordWithTag);
    }



    private void CreateTooltipTriggerObject(TMP_WordInfo wordInfo, string tooltipText)
    {
        // Tworzymy obiekt
        GameObject tooltipObject = Instantiate(tooltipPrefab, textMeshProText.transform);
        tooltipObject.name = "TooltipTrigger_" + wordInfo.GetWord();

        // Przypisujemy tekst tooltipa do komponentu TooltipTrigger
        TooltipTrigger tooltipTrigger = tooltipObject.GetComponent<TooltipTrigger>();
        tooltipTrigger.TooltipText = tooltipText;

        // Repositionujemy obiekt w miejscu s³owa
        RepositionTooltipObject(wordInfo, tooltipObject);

        createdTooltipObjects.Add(tooltipObject);
    }

    private void RepositionTooltipObject(TMP_WordInfo wordInfo, GameObject tooltipObject)
    {
        RectTransform rectTransform = tooltipObject.GetComponent<RectTransform>();

        TMP_TextInfo textInfo = textMeshProText.textInfo;

        int startIndex = wordInfo.firstCharacterIndex;
        int endIndex = wordInfo.lastCharacterIndex;

        // Obliczamy pozycjê s³owa
        float wordCenterX = (textInfo.characterInfo[startIndex].bottomLeft.x + textInfo.characterInfo[endIndex].topRight.x) / 2f;
        float wordBottomLine = textInfo.characterInfo[startIndex].bottomLeft.y + wordInfo.textComponent.fontSize / 2;

        float wordWidth = Mathf.Abs(textInfo.characterInfo[startIndex].bottomLeft.x - textInfo.characterInfo[endIndex].topRight.x);

        // Ustawiamy rozmiar i pozycjê
        rectTransform.sizeDelta = new Vector2(wordWidth, wordInfo.textComponent.fontSize);
        Vector3 wordPosition = textMeshProText.transform.TransformPoint(new Vector3(wordCenterX, wordBottomLine, 0f));
        rectTransform.position = wordPosition;
    }
}
