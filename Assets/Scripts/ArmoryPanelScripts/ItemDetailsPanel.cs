using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ItemDetailsPanel : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI categoriesText;

    public ArmoryPanel armoryPanel;

    public void ShowDetails(ItemData itemData)
    {
        StartCoroutine(ShowDetailsCoroutine(itemData));
    }

    public IEnumerator ShowDetailsCoroutine(ItemData itemData)
    {
        iconImage.sprite = itemData.icon;
        nameText.text = itemData.itemName;
        descriptionText.text = itemData.description;

        string categories = string.Join(", ", itemData.categories).Replace('_', ' ');
        categoriesText.text = $"Kategorie : {categories}";

        yield return new WaitForEndOfFrame();

        armoryPanel.InitializePanelData();
    }
}
