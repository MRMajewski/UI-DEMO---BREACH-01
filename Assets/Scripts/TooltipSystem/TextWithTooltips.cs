using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TextWithTooltips : MonoBehaviour
{
    [SerializeField]
    private GameObject tooltipPrefab; // Prefab dla obiektu reprezentuj�cego tooltip.

    private Regex tooltipRegex = new Regex("<tooltip>(.*?)</tooltip>");


    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;

    [ContextMenu("TEST")]
    public void Test()
    {
        textMeshProUGUI.ForceMeshUpdate();

        ProcessText(textMeshProUGUI);
    }


    public void ProcessText(TMP_Text textMeshPro)
    {
        // Pobierz tekst �r�d�owy z TextMeshPro
        string sourceText = textMeshPro.text;

        // Znajd� wszystkie s�owa pomi�dzy tagami <tooltip> i </tooltip>
        MatchCollection matches = tooltipRegex.Matches(sourceText);

        if (matches.Count == 0)
        {
            Debug.LogWarning("No tooltip tags found in the text.");
            return;
        }

        // Pobierz informacje o tek�cie
        textMeshPro.ForceMeshUpdate(); // Wymu� aktualizacj� danych tekstowych
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        // Lista indeks�w do usuni�cia tag�w
        List<(int startIndex, int length)> tagRanges = new List<(int, int)>();

        foreach (Match match in matches)
        {
            string tooltipWord = match.Groups[1].Value; // Tekst pomi�dzy tagami <tooltip></tooltip>
            int startIndex = match.Index; // Pocz�tkowy indeks pasuj�cego tekstu w sourceText
            int length = match.Length; // D�ugo�� pasuj�cego tekstu

            tagRanges.Add((startIndex, length));

            // Znajd� pozycj� s�owa w TextMeshPro
            TMP_WordInfo? wordInfo = FindWordInfo(textInfo, tooltipWord);

            if (!wordInfo.HasValue)
            {
                Debug.LogWarning($"Tooltip word '{tooltipWord}' not found in TextMeshPro data.");
                continue;
            }

            // Stw�rz obiekt GameObject dla tooltipa
            GameObject tooltipObject = Instantiate(tooltipPrefab, textMeshPro.transform);
            RepositionTooltip(wordInfo.Value, tooltipObject);
        }

        // Usu� tagi z tekstu
        string cleanedText = RemoveTags(sourceText, tagRanges);
        textMeshPro.text = cleanedText;
    }

    private TMP_WordInfo? FindWordInfo(TMP_TextInfo textInfo, string word)
    {
        foreach (TMP_WordInfo wordInfo in textInfo.wordInfo)
        {
            if (wordInfo.characterCount > 0) // Sprawd�, czy struktura zawiera dane
            {
                string currentWord = wordInfo.GetWord();
                if (!string.IsNullOrEmpty(currentWord) && currentWord == word)
                {
                    return wordInfo;
                }
            }
        }
        return null;
    }


    private void RepositionTooltip(TMP_WordInfo wordInfo, GameObject tooltipObject)
    {
        RectTransform rectTransform = tooltipObject.GetComponent<RectTransform>();
        TMP_Text textComponent = wordInfo.textComponent;

        TMP_CharacterInfo startChar = textComponent.textInfo.characterInfo[wordInfo.firstCharacterIndex];
        TMP_CharacterInfo endChar = textComponent.textInfo.characterInfo[wordInfo.lastCharacterIndex];

        Vector3 bottomLeft = startChar.bottomLeft;
        Vector3 topRight = endChar.topRight;

        Vector3 centerPosition = (bottomLeft + topRight) / 2;
        Vector2 size = new Vector2(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);

        rectTransform.sizeDelta = size;
        rectTransform.position = textComponent.transform.TransformPoint(centerPosition);
    }

    private string RemoveTags(string text, List<(int startIndex, int length)> tagRanges)
    {
        tagRanges.Sort((a, b) => b.startIndex.CompareTo(a.startIndex)); // Sortuj w odwrotnej kolejno�ci
        foreach (var range in tagRanges)
        {
            text = text.Remove(range.startIndex, range.length);
        }
        return text;
    }
}
