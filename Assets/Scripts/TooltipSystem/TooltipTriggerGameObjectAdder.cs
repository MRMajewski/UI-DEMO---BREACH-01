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

        // Zaktualizuj dane przed rozpocz�ciem przetwarzania
        textMeshProText.ForceMeshUpdate();  // Upewnij si�, �e siatka jest zaktualizowana

        // Przechodzimy przez frazy w triggerach
        string updatedText = textMeshProText.text;

        foreach (var trigger in triggersList)
        {
            string triggerName = trigger.TriggerName;
            string tooltipText = trigger.TooltipText;

            // Wyszukaj wszystkie wyst�pienia triggerName (frazy) w tek�cie
            var matches = FindPhraseMatches(triggerName);

            foreach (var match in matches)
            {
                // Otaguj znalezion� fraz� <b></b> dla ka�dego s�owa
                updatedText = AddBoldTagToText(updatedText, match.GetWord());
            }

            // Zaktualizuj tekst w TextMeshPro
            textMeshProText.text = updatedText;

            // Upewnij si�, �e tekst jest aktualny po modyfikacjach
            textMeshProText.ForceMeshUpdate();

            // Tworzymy Tooltipy dla ka�dego s�owa w dopasowanej frazie
            foreach (var match in matches)
            {
                // Tworzymy nowy obiekt TooltipTrigger dla ka�dego s�owa w frazie
                CreateTooltipTriggerObjectsForPhrase(new List<TMP_WordInfo> { match }, trigger);
            }
        }
    }

    private List<TMP_WordInfo> FindPhraseMatches(string triggerName)
    {
        List<TMP_WordInfo> matchingWords = new List<TMP_WordInfo>();

        // ��czymy s�owa w fraz�
        string[] triggerWords = triggerName.Split(' ');

        for (int i = 0; i < textMeshProText.textInfo.wordCount; i++)
        {
            var wordInfo = textMeshProText.textInfo.wordInfo[i];

            // Je�li s�owo pasuje do jednego z wyraz�w w frazie
            bool isMatch = true;
            for (int j = 0; j < triggerWords.Length; j++)
            {
                if (i + j >= textMeshProText.textInfo.wordCount || !textMeshProText.textInfo.wordInfo[i + j].GetWord().Equals(triggerWords[j], System.StringComparison.OrdinalIgnoreCase))
                {
                    isMatch = false;
                    break;
                }
            }

            // Je�li ca�a fraza pasuje
            if (isMatch)
            {
                // Dodajemy wszystkie s�owa z frazy do listy
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
        // Sprawdzamy, czy s�owo nie jest ju� otagowane <b>...</b>
        string wordWithTag = "<b>" + word + "</b>";

        // Zast�pujemy tylko te s�owa, kt�re nie zawieraj� tagu <b>
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

            //// Przypisz akcj�, je�li tooltip jest typu WithButton
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

        // Obliczamy pozycj� s�owa
        float wordCenterX = (textInfo.characterInfo[startIndex].bottomLeft.x + textInfo.characterInfo[endIndex].topRight.x) / 2f;
        float wordBottomLine = textInfo.characterInfo[startIndex].bottomLeft.y + wordInfo.textComponent.fontSize / 2;

        float wordWidth = Mathf.Abs(textInfo.characterInfo[startIndex].bottomLeft.x - textInfo.characterInfo[endIndex].topRight.x);

        // Debugowanie: sprawdzamy pozycj�
        Debug.Log($"Repositioning tooltip for word '{wordInfo.GetWord()}' at position {wordCenterX}, {wordBottomLine} with width {wordWidth}");

        // Ustawiamy rozmiar i pozycj�
        rectTransform.sizeDelta = new Vector2(wordWidth, wordInfo.textComponent.fontSize);
        Vector3 wordPosition = textMeshProText.transform.TransformPoint(new Vector3(wordCenterX, wordBottomLine, 0f));
        rectTransform.position = wordPosition;
    }
}
