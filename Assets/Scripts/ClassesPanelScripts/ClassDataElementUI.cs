using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClassDataElementUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI classNameText;
    [SerializeField]
    private TextMeshProUGUI classDescriptionText;
    [SerializeField]
    private TextMeshProUGUI classProficienciesDescriptionText;
    [SerializeField]
    private TextMeshProUGUI subclassesDescriptionText;
    [SerializeField]
    private TextMeshProUGUI skillsDescriptionText;

    [SerializeField]
    private Transform levelUpDetailsParent;

    [SerializeField]
    private ClassLevelUpDetailsElementUI levelUpDetailsPrefab;

    [SerializeField]
    private ClassesDataBase classesDatabase;

    public void Initialize(ClassData data)
    {
        classNameText.text = data.className;

        classDescriptionText.text = "<size=150%>Opis:</size><br>" + data.classDescription;

        subclassesDescriptionText.text = "<size=150%>Subklasy:</size><br>" + string.Join("<br>", data.subclasses);

        string reductionDiceDescription = classesDatabase.GetReductionDiceDescription(data.reductionDiceInfos);
        string savingThrowDescription = classesDatabase.GetSavingThrowDescription(data.savingThrowInfos);

        List<string> abilityDescriptions = new List<string>();
        foreach (var ability in data.abilityInfos)
        {
            abilityDescriptions.Add(classesDatabase.GetAbilityDescription(ability));
        }

        classProficienciesDescriptionText.text = "<size=150%>Wyszkolenia:</size><br>" +
            $"<b>Kostka redukcji:</b> {reductionDiceDescription}<br>" +
            $"<b>Rzuty obronne:</b> {savingThrowDescription}<br>" +
            $"<b>Zdolnoœci (dwie do wyboru): </b>{string.Join(" ", abilityDescriptions)}";


        GenerateLevelUpDetails(data.levelUpTable);
    }

    private void GenerateLevelUpDetails(List<LevelUpTable> levelUpTable)
    {
        foreach (var levelData in levelUpTable)
        {
            ClassLevelUpDetailsElementUI newLevelUpDetail = Instantiate(levelUpDetailsPrefab, levelUpDetailsParent);

            if (newLevelUpDetail != null)
            {
                newLevelUpDetail.Initialize(levelData);
            }
        }

        levelUpDetailsPrefab.gameObject.SetActive(false);
    }
}
