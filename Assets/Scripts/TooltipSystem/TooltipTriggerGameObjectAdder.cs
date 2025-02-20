using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class TooltipTriggerGameObjectAdder : MonoBehaviour
{
    [SerializeField]
    private List<TooltipTriggerTextInfo> triggersList;

    [SerializeField]
    private GameObject triggerPrefab;

    [SerializeField]
    private TextMeshProUGUI textMeshProText;

    [SerializeField]
    private bool searchIndividualPhrases = false; 

    [SerializeField]
    private List<GameObject> createdTriggersObjects = new List<GameObject>();

    [ContextMenu("TEST")]
    public void Test()
    {
        ProcessText(textMeshProText);
    }

    public void AddTriggersToText()
    {
        foreach (GameObject trigger in createdTriggersObjects)
        {
            if(trigger!=null)
            DestroyImmediate(trigger);
        }
        createdTriggersObjects.Clear();
        createdTriggersObjects.TrimExcess();

        bool wasActive = textMeshProText.gameObject.activeSelf;
        if (!wasActive)
        textMeshProText.gameObject.SetActive(true);
        ProcessText(textMeshProText); 
        textMeshProText.gameObject.SetActive(wasActive);
    }

    public void ProcessText(TextMeshProUGUI newTextMeshPro)
    {
        textMeshProText = newTextMeshPro;
        textMeshProText.ForceMeshUpdate();

        string updatedText = textMeshProText.text;

        foreach (var trigger in triggersList)
        {
            string triggerName = trigger.TriggerName;

            // Wyszukaj wszystkie pasuj¹ce frazy lub pojedyncze s³owa w zale¿noœci od wartoœci boola
            var matches = searchIndividualPhrases
                ? FindWordMatches(triggerName)
                : FindPhraseMatches(triggerName);

            foreach (var match in matches)
            {
                updatedText = AddCursiveTagToText(updatedText, match.GetWord());
            }

            textMeshProText.text = updatedText;
            textMeshProText.ForceMeshUpdate();

            foreach (var match in matches)
            {
                CreateTooltipTriggerObjectsForPhrase(new List<TMP_WordInfo> { match }, trigger);
            }
        }
    }

    private List<TMP_WordInfo> FindPhraseMatches(string triggerName)
    {
        textMeshProText.ForceMeshUpdate();
        List<TMP_WordInfo> matchingWords = new List<TMP_WordInfo>();

        string[] triggerWords = triggerName.Split(' ');

        for (int i = 0; i < textMeshProText.textInfo.wordCount; i++)
        {
            bool isMatch = true;

            for (int j = 0; j < triggerWords.Length; j++)
            {
                if (i + j >= textMeshProText.textInfo.wordCount ||
                    !textMeshProText.textInfo.wordInfo[i + j].GetWord().Equals(triggerWords[j], System.StringComparison.OrdinalIgnoreCase))
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                for (int k = 0; k < triggerWords.Length; k++)
                {
                    matchingWords.Add(textMeshProText.textInfo.wordInfo[i + k]);
                }
            }
        }

        return matchingWords;
    }

    private List<TMP_WordInfo> FindWordMatches(string triggerName)
    {
        textMeshProText.ForceMeshUpdate();
        List<TMP_WordInfo> matchingWords = new List<TMP_WordInfo>();

        string[] triggerWords = triggerName.Split(' ');


        Debug.Log("trigger Words" + triggerWords);
        textMeshProText.ForceMeshUpdate();
        for (int i = 0; i < textMeshProText.textInfo.wordCount; i++)
        {
            var wordInfo = textMeshProText.textInfo.wordInfo[i];

            foreach (string triggerWord in triggerWords)
            {
                if (wordInfo.GetWord().Equals(triggerWord, System.StringComparison.OrdinalIgnoreCase))
                {
                    matchingWords.Add(wordInfo);
                }
            }
        }

        return matchingWords;
    }

    private string AddCursiveTagToText(string text, string word)
    {
        string wordWithTag = "<i>" + word + "</i>";

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
            GameObject tooltipObject = Instantiate(triggerPrefab, textMeshProText.transform);
            tooltipObject.name = "TooltipTrigger_" + wordInfo.GetWord();

            TooltipTrigger tooltipTrigger = tooltipObject.GetComponent<TooltipTrigger>();
            tooltipTrigger.TooltipText = tooltipInfo.TooltipText;
            tooltipTrigger.Type = tooltipInfo.TooltipType;
            tooltipTrigger.ActionName = tooltipInfo.ActionName;

            RepositionTooltipTriggerObject(wordInfo, tooltipObject);
            createdTriggersObjects.Add(tooltipObject);
        }
    }

    private void RepositionTooltipTriggerObject(TMP_WordInfo wordInfo, GameObject tooltipObject)
    {
        RectTransform rectTransform = tooltipObject.GetComponent<RectTransform>();

        TMP_TextInfo textInfo = textMeshProText.textInfo;

        int startIndex = wordInfo.firstCharacterIndex;
        int endIndex = wordInfo.lastCharacterIndex;

        float wordCenterX = (textInfo.characterInfo[startIndex].bottomLeft.x + textInfo.characterInfo[endIndex].topRight.x) / 2f;
        float wordBottomLine = textInfo.characterInfo[startIndex].bottomLeft.y + wordInfo.textComponent.fontSize / 2;

        float wordWidth = Mathf.Abs(textInfo.characterInfo[startIndex].bottomLeft.x - textInfo.characterInfo[endIndex].topRight.x);

        rectTransform.sizeDelta = new Vector2(wordWidth * 1.15f, wordInfo.textComponent.fontSize * 1.5f);
        Vector3 wordPosition = textMeshProText.transform.TransformPoint(new Vector3(wordCenterX, wordBottomLine - wordInfo.textComponent.fontSize / 5f, 0f));
        rectTransform.position = wordPosition;
    }
}
