using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailsPanel : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI categoriesText;

    public void ShowDetails(ItemData itemData)
    {
        iconImage.sprite = itemData.icon;
        nameText.text = itemData.itemName;
        descriptionText.text = itemData.description;

        // Wyœwietlanie kategorii
        string categories = string.Join(", ", itemData.categories);
        categoriesText.text = $"Categories: {categories}";
    }
}
