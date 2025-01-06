using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class ClassLevelUpDetailsElementUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelIndexText;
    [SerializeField]
    private TextMeshProUGUI levelSimpleDescription;
    [SerializeField]
    private TextMeshProUGUI levelProficiencyBonusText;
    [SerializeField]
    private TextMeshProUGUI levelSkillsDetailedText;

    [SerializeField]
    private GameObject levelSkillsDetailsGameObject;

    [SerializeField]
    private bool isOpen = false;

    public void Initialize(LevelUpTable data)
    {
        levelIndexText.text = data.level.ToString();

        levelSimpleDescription.text = data.skills;

        levelProficiencyBonusText.text = "+ " + data.proficiencyBonus;

        levelSkillsDetailedText.text = data.skillsDescription;

        levelSkillsDetailsGameObject.SetActive(isOpen);
    }

    public  void SelectionClick()
    {
        isOpen = !isOpen;

        levelSkillsDetailsGameObject.gameObject.SetActive(isOpen);

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.transform.parent.GetComponent<RectTransform>());
    }
}
