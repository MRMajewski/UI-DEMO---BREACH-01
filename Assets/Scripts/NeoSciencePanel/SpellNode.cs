using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellNode : TrainingNode
{
    [Header("Spell references")]
    public GameObject contentGameObject;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI typeText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    public override void SelectionClick()
    {
        base.SelectionClick();
        contentGameObject.gameObject.SetActive(isOpen);

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.transform.parent.GetComponent<RectTransform>());
    }

    public void Setup(NeophysicsSpellData spellData)
    {
        string cleanName = spellData.spellName.Replace("_", " ");

        nameText.text = cleanName;
        gameObject.name = cleanName +" - Spell Node";

        typeText.text = spellData.spellType.ToString();
        levelText.text = spellData.spellLevel.ToString();

        ContentText.text = $"Opis: {spellData.description}\n";
        ContentText.text += $"Zasiêg: {spellData.spellRange}\n";
        ContentText.text += $"Czas rzucania: {spellData.spellCastingTime}";

        contentGameObject.SetActive(isOpen) ;
    }
}
