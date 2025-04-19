using System.Collections.Generic;
using UnityEngine;

public class NeoScienceSubPanel : MonoBehaviour
{
    [SerializeField] private NeoScienceCategory category;
    [SerializeField] private Transform spellListParent;
    [SerializeField] private SpellNode spellPrefab;
    [SerializeField] private List<SpellNode> instantiatedSpells = new();

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
            var spellUI = Instantiate(spellPrefab, spellListParent);

            spellUI.Setup(spell);
            instantiatedSpells.Add(spellUI);
        }
    }
}
