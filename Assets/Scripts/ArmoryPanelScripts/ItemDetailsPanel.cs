using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ItemDetailsPanel : MonoBehaviour
{
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private TextMeshProUGUI categoriesText;

    [SerializeField]
    private ArmoryPanel armoryPanel;

    public void ShowDetails(ItemData itemData)
    {
        StartCoroutine(ShowDetailsCoroutine(itemData));
    }

    public IEnumerator ShowDetailsCoroutine(ItemData itemData)
    {
        TooltipManager.Instance.HideTooltip();

        iconImage.sprite = itemData.icon;
        nameText.text = itemData.itemName;
        descriptionText.text = itemData.description;

        string categories = string.Join(", ", itemData.categories).Replace('_', ' ');
        categoriesText.text = $"Kategorie : {categories}";

        yield return new WaitForEndOfFrame();

        armoryPanel.InitializePanelData();
    }
}
