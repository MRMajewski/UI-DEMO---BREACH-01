using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Linq;

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
 
    private System.Action onDetailsShown;

    public void ShowDetails(ItemData itemData, System.Action onShown = null)
    {
        onDetailsShown = onShown;
        StartCoroutine(ShowDetailsCoroutine(itemData));
    }

    public IEnumerator ShowDetailsCoroutine(ItemData itemData)
    {
        TooltipManager.Instance.HideTooltip();

        iconImage.sprite = itemData.icon;
        nameText.text = itemData.itemName;
        descriptionText.text = itemData.description;

        string categories = string.Join(", ", itemData.categories.Select(c => EnumManager.GetName(c)));
        categoriesText.text = $"Kategorie: {categories}";

        yield return new WaitForEndOfFrame();

        onDetailsShown?.Invoke();
        onDetailsShown = null;
    }
}
