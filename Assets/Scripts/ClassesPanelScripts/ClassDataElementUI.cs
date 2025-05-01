using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassDataElementUI : MonoBehaviour, ISnapperPanel
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
    [Space]
    [SerializeField]
    private Image backgroundImage;
    [Space]
    [SerializeField]
    private ClassLevelUpDetailsElementUI levelUpDetailsPrefab;
    [SerializeField]
    private Transform levelUpDetailsParent;

    [SerializeField]
    private ClassDetailsElementUI subClassDetailsPrefab;
    [SerializeField]
    private Transform subClassDetailsParent;

    [SerializeField]
    private ClassesDataBase classesDatabase;

    [SerializeField]
    private List<TooltipTriggerGameObjectAdder> triggerAdders;

    public List<TooltipTriggerGameObjectAdder> TriggerGameObjectAdder { get { return triggerAdders; } }

    public void Initialize(ClassData data)
    {
        classNameText.text = data.className;
        classDescriptionText.text = data.classDescription;

        string reductionDiceDescription = classesDatabase.GetReductionDiceDescription(data.reductionDiceInfos);
        string savingThrowDescription = classesDatabase.GetSavingThrowDescription(data.savingThrowInfos);

        backgroundImage.color = new Color(data.classColor.r, data.classColor.g, data.classColor.b, 100f / 255f);

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
        GenerateSubClassesDetails(data.subClassesTable);
    }

    private void GenerateLevelUpDetails(List<LevelUpTable> levelUpTable)
    {
        foreach (var levelData in levelUpTable)
        {
            ClassLevelUpDetailsElementUI newLevelUpDetail = Instantiate(levelUpDetailsPrefab, levelUpDetailsParent);

            if (newLevelUpDetail != null)
            {
                newLevelUpDetail.Initialize(levelData);

                triggerAdders.AddRange(newLevelUpDetail.TriggerGameObjectAdder);
            }      
        }
        levelUpDetailsPrefab.gameObject.SetActive(false);
    }

    private void GenerateSubClassesDetails(List<SubClassInfo> subClassInfo)
    {
        foreach (SubClassInfo subclass in subClassInfo)
        {
            ClassDetailsElementUI newSubclassDetails = Instantiate(subClassDetailsPrefab, subClassDetailsParent);

            if (newSubclassDetails != null)
            {
                newSubclassDetails.Initialize(subclass);
            }
        }
        subClassDetailsPrefab.gameObject.SetActive(false);
    }

    public void ResizeSnappedPanel()
    {
        throw new System.NotImplementedException();
    }
}
