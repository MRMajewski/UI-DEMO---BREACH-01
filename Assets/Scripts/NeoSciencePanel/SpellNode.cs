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
    [SerializeField]
    private TextMeshProUGUI categoryText;

    public TextMeshProUGUI NameText { get { return nameText; } }
    public TextMeshProUGUI TypeText { get { return typeText; } }
    public TextMeshProUGUI LevelText { get { return levelText; } }
    public TextMeshProUGUI CategoryText { get { return categoryText; } }

    public override void SelectionClick()
    {
        base.SelectionClick();
        contentGameObject.gameObject.SetActive(isOpen);

        if (isOpen)
            CheckVisibilityDelayed();

#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif
        StartCoroutine(UIRebuildCoroutine());
    }


    IEnumerator UIRebuildCoroutine()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.transform.parent.GetComponent<RectTransform>());
    }
    public void Setup(SpellData spellData)
    {
        nameText.text = spellData.spellName;
        levelText.text = spellData is NeophysicsSpellData phys ? phys.spellLevel.ToString() : "";
        ContentText.text = "Opis: " + spellData.spellDescription;

        if (spellData is NeophysicsSpellData n)
        {
            levelText.text = n.spellLevel.ToString();
            typeText.text = EnumTranslator.GetName(n.spellType);
            ContentText.text += "\nZasi�g: " + n.spellRange;
            ContentText.text += "\nCzas rzucania: " + n.spellCastingTime;
        }
        if (spellData is NeoLinguisticsSpellData l)
        {
            levelText.text = l.spellLevel.ToString();
            typeText.text = "";
            ContentText.text += "\nZasi�g: " + l.spellRange;
            ContentText.text += "\nCzas rzucania: " + l.spellCastingTime;
        }

        if (spellData is PsionicsSpellData p)
        {
            categoryText.text = EnumTranslator.GetName(p.Spellcategory);
            typeText.text = EnumTranslator.GetName(p.spellType);
            levelText.text = p.spellCost.ToString();
        }

        if (spellData is NeoSensoricsSpellData s)
        {
            categoryText.text = EnumTranslator.GetName(s.Spellcategory);
            typeText.text = EnumTranslator.GetName(s.spellType);
            levelText.text = s.spellCost.ToString();
        }
        gameObject.name = spellData.spellName;
    }
}
