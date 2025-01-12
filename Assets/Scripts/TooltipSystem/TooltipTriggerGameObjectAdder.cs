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

        // Zaktualizuj dane przed rozpoczêciem przetwarzania
        textMeshProText.ForceMeshUpdate();  // Upewnij siê, ¿e siatka jest zaktualizowana

        // Przechodzimy przez frazy w triggerach
        string updatedText = textMeshProText.text;

        foreach (var trigger in triggersList)
        {
            string triggerName = trigger.TriggerName;
            string tooltipText = trigger.TooltipText;

            // Wyszukaj wszystkie wyst¹pienia triggerName (frazy) w tekœcie
            var matches = FindPhraseMatches(triggerName);

            foreach (var match in matches)
            {
                // Otaguj znalezion¹ frazê <b></b> dla ka¿dego s³owa
                updatedText = AddBoldTagToText(updatedText, match.GetWord());
            }

            // Zaktualizuj tekst w TextMeshPro
            textMeshProText.text = updatedText;

            // Upewnij siê, ¿e tekst jest aktualny po modyfikacjach
            textMeshProText.ForceMeshUpdate();

            // Tworzymy Tooltipy dla ka¿dego s³owa w dopasowanej frazie
            foreach (var match in matches)
            {
                // Tworzymy nowy obiekt TooltipTrigger dla ka¿dego s³owa w frazie
                CreateTooltipTriggerObjectsForPhrase(new List<TMP_WordInfo> { match }, trigger);
            }
        }
    }

    private List<TMP_WordInfo> FindPhraseMatches(string triggerName)
    {
        List<TMP_WordInfo> matchingWords = new List<TMP_WordInfo>();

        // £¹czymy s³owa w frazê
        string[] triggerWords = triggerName.Split(' ');

        for (int i = 0; i < textMeshProText.textInfo.wordCount; i++)
        {
            var wordInfo = textMeshProText.textInfo.wordInfo[i];

            // Jeœli s³owo pasuje do jednego z wyrazów w frazie
            bool isMatch = true;
            for (int j = 0; j < triggerWords.Length; j++)
            {
                if (i + j >= textMeshProText.textInfo.wordCount || !textMeshProText.textInfo.wordInfo[i + j].GetWord().Equals(triggerWords[j], System.StringComparison.OrdinalIgnoreCase))
                {
                    isMatch = false;
                    break;
                }
            }

            // Jeœli ca³a fraza pasuje
            if (isMatch)
            {
                // Dodajemy wszystkie s³owa z frazy do listy
                for (int k = 0; k < triggerWords.Length; k++)
                {
                    matchingWords.Add(textMeshProText.textInfo.wordInfo[i + k]);
                }
            }
        }

        return matchingWords;
    }

    private string AddBoldTagToText(string text, string word)
    {
        // Sprawdzamy, czy s³owo nie jest ju¿ otagowane <b>...</b>
        string wordWithTag = "<b>" + word + "</b>";

        // Zastêpujemy tylko te s³owa, które nie zawieraj¹ tagu <b>
        if (!text.Contains(wordWithTag))
        {
            text = text.Replace(word, wordWithTag);
        }

        return text;
    }

    private void CreateTooltipTriggerObjectsForPhrase(List<TMP_WordInfo> wordInfos, TooltipTriggerTextInfo tooltipInfo)
    {
        foreach (var wordInfo in wordInfos)
        {
            GameObject tooltipObject = Instantiate(tooltipPrefab, textMeshProText.transform);
            tooltipObject.name = "TooltipTrigger_" + wordInfo.GetWord();

            TooltipTrigger tooltipTrigger = tooltipObject.GetComponent<TooltipTrigger>();
            tooltipTrigger.TooltipText = tooltipInfo.TooltipText;
            tooltipTrigger.Type = tooltipInfo.TooltipType;
            tooltipTrigger.ActionName = tooltipInfo.ActionName;

            //// Przypisz akcjê, jeœli tooltip jest typu WithButton
            //if (tooltipInfo.TooltipType == TooltipType.WithButton && tooltipInfo.ActionName != null)
            //{
            //}

            RepositionTooltipObject(wordInfo, tooltipObject);
            createdTooltipObjects.Add(tooltipObject);
        }
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

        // Debugowanie: sprawdzamy pozycjê
        Debug.Log($"Repositioning tooltip for word '{wordInfo.GetWord()}' at position {wordCenterX}, {wordBottomLine} with width {wordWidth}");

        // Ustawiamy rozmiar i pozycjê
        rectTransform.sizeDelta = new Vector2(wordWidth, wordInfo.textComponent.fontSize);
        Vector3 wordPosition = textMeshProText.transform.TransformPoint(new Vector3(wordCenterX, wordBottomLine, 0f));
        rectTransform.position = wordPosition;
    }
}
