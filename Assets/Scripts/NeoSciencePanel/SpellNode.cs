using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public void Setup(SpellData spellData)
    {
       // nameText.text = spellData.spellName.Replace("_", " ");
        nameText.text = spellData.spellName;
        levelText.text = spellData is NeophysicsSpellData phys ? phys.spellLevel.ToString() : "";
        ContentText.text = "Opis: " + spellData.spellDescription;

        if (spellData is NeophysicsSpellData n)
        {
            levelText.text = n.spellLevel.ToString();
            typeText.text= n.spellType.ToString();
            ContentText.text += "\nZasiêg: " + n.spellRange;
            ContentText.text += "\nCzas rzucania: " + n.spellCastingTime;
        }
        if (spellData is NeoLinguisticsSpellData l)
        {
            levelText.text = l.spellLevel.ToString();
            typeText.text = "";
            ContentText.text +=  l.spellRange;
            ContentText.text += l.spellCastingTime;
        }

        if (spellData is PsionicsSpellData p)
        {
            typeText.text = p.spellType.ToString();
            levelText.text += p.spellCost.ToString();
        }

        if (spellData is NeoSensoricsSpellData s)
        {
            typeText.text = s.spellType.ToString();
            levelText.text = s.spellCost.ToString();
        }

        //  gameObject.name = spellData.spellName.Replace("_", " ");
        gameObject.name = spellData.spellName;
    }
}
