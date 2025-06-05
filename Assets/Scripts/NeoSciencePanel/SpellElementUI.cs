using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpellUIElement : ItemElementUI
{
    private ItemData itemData;

    [SerializeField]
    private ItemDetailsPanel itemDetailsPanel;

    public override void SetUp(ItemData data)
    {
        itemData = data;
        icon.sprite = data.icon;
        nameText.text = data.itemName;
        button.onClick.AddListener(ShowItemElementInfo);
    }

    private void ShowItemElementInfo()
    {
        itemDetailsPanel.ShowDetails(itemData);
    }
}
