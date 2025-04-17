using System.Collections.Generic;
using UnityEngine;

public class NeoScienceSubPanel : MonoBehaviour
{
    [SerializeField] private NeoScienceCategory category;
    [SerializeField] private Transform spellListParent;
    [SerializeField] private SpellNode spellPrefab;
    [SerializeField]
    private List<SpellNode> instantiatedSpells = new List<SpellNode>();

    public void Setup(List<NeophysicsSpellData> neophysicsSpellsList)
    {
        // Usuñ stare obiekty UI
        foreach (var node in instantiatedSpells)
        {
            if (node != null)
                DestroyImmediate(node.gameObject);
        }

        instantiatedSpells.Clear();

        // Twórz nowe UI
        foreach (var spell in neophysicsSpellsList)
        {
            var spellUI = Instantiate(spellPrefab, spellListParent);
            spellUI.Setup(spell);
            instantiatedSpells.Add(spellUI);
        }
    }
}
