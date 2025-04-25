using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NeoScienceSubPanel : MonoBehaviour
{
    [SerializeField] private NeoScienceCategory category;
    [SerializeField] private Transform spellListParent;
    [SerializeField] private SpellNode spellPrefab;
    [SerializeField] private List<SpellNode> instantiatedSpells = new();

    [SerializeField] private UIScrollViewFitter uIScrollViewFitter;

    public void Setup<T>(List<T> spellList) where T : SpellData
    {
        foreach (var node in instantiatedSpells)
        {
            if (node != null)
                DestroyImmediate(node.gameObject);
        }

        instantiatedSpells.Clear();

        foreach (var spell in spellList)
        {
            SpellNode spellUI = Instantiate(spellPrefab, spellListParent);
            spellUI.Setup(spell);
            spellUI.UIScrollViewFitter = this.uIScrollViewFitter;
            instantiatedSpells.Add(spellUI);
        }

        CloseAllSpells();
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.spellListParent.GetComponent<RectTransform>());
    }

    public void SortByName()
    {
        var sorted = instantiatedSpells
            .OrderBy(n => n.NameText.text)
            .ToList();

        ReorderNodes(sorted);
    }

    public void GroupByType()
    {
        var sorted = instantiatedSpells
            .OrderBy(n => n.TypeText.text)
            .ThenBy(l =>
            {
                int.TryParse(l.LevelText.text, out int lvl);
                return lvl;
            })
            .ToList();

        ReorderNodes(sorted);
    }

    public void GroupByLevel()
    {
        var sorted = instantiatedSpells
            .OrderBy(n =>
            {
                int.TryParse(n.LevelText.text, out int lvl);
                return lvl;
            })
            .ThenBy(n => n.TypeText.text)
            .ToList();

        ReorderNodes(sorted);
    }

    private void ReorderNodes(List<SpellNode> sortedList)
    {
        for (int i = 0; i < sortedList.Count; i++)
        {
            sortedList[i].transform.SetSiblingIndex(i);
        }
    }

    public void CloseAllSpells()
    {
        foreach (var spellNode in instantiatedSpells)
        {
            if (spellNode.IsOpen)
                spellNode.SelectionClick();
        }
    }
}
