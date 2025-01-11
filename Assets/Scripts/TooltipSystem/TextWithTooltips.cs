using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TextWithTooltips : MonoBehaviour
{
    [SerializeField]
    private GameObject tooltipPrefab; // Prefab dla obiektu reprezentuj¹cego tooltip.

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
        // Pobierz tekst Ÿród³owy z TextMeshPro
        string sourceText = textMeshPro.text;

        // ZnajdŸ wszystkie s³owa pomiêdzy tagami <tooltip> i </tooltip>
        MatchCollection matches = tooltipRegex.Matches(sourceText);

        if (matches.Count == 0)
        {
            Debug.LogWarning("No tooltip tags found in the text.");
            return;
        }

        // Pobierz informacje o tekœcie
        textMeshPro.ForceMeshUpdate(); // Wymuœ aktualizacjê danych tekstowych
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        // Lista indeksów do usuniêcia tagów
        List<(int startIndex, int length)> tagRanges = new List<(int, int)>();

        foreach (Match match in matches)
        {
            string tooltipWord = match.Groups[1].Value; // Tekst pomiêdzy tagami <tooltip></tooltip>
            int startIndex = match.Index; // Pocz¹tkowy indeks pasuj¹cego tekstu w sourceText
            int length = match.Length; // D³ugoœæ pasuj¹cego tekstu

            tagRanges.Add((startIndex, length));

            // ZnajdŸ pozycjê s³owa w TextMeshPro
            TMP_WordInfo? wordInfo = FindWordInfo(textInfo, tooltipWord);

            if (!wordInfo.HasValue)
            {
                Debug.LogWarning($"Tooltip word '{tooltipWord}' not found in TextMeshPro data.");
                continue;
            }

            // Stwórz obiekt GameObject dla tooltipa
            GameObject tooltipObject = Instantiate(tooltipPrefab, textMeshPro.transform);
            RepositionTooltip(wordInfo.Value, tooltipObject);
        }

        // Usuñ tagi z tekstu
        string cleanedText = RemoveTags(sourceText, tagRanges);
        textMeshPro.text = cleanedText;
    }

    private TMP_WordInfo? FindWordInfo(TMP_TextInfo textInfo, string word)
    {
        foreach (TMP_WordInfo wordInfo in textInfo.wordInfo)
        {
            if (wordInfo.characterCount > 0) // SprawdŸ, czy struktura zawiera dane
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
        tagRanges.Sort((a, b) => b.startIndex.CompareTo(a.startIndex)); // Sortuj w odwrotnej kolejnoœci
        foreach (var range in tagRanges)
        {
            text = text.Remove(range.startIndex, range.length);
        }
        return text;
    }
}
