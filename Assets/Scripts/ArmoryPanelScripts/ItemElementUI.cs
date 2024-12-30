using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum ItemCategory
{
    Broñ,
    Pancerz,
    Akcesorium,
    Broñ_Palna,
    Broñ_Bia³a,
    Cywilny,
    Militarny,
    Eksperymentalny,
    Pistolet,
    SMG,
    Karabin,
    Karabin_Szturmowy,
    Broñ_Ciê¿ka,
    Strzelba,
    Narzêdze,
    Inne
}

public class ItemElementUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameText;
    public Button button;

    private ItemData itemData;

    [SerializeField]
    private ItemDetailsPanel itemDetailsPanel;

    public void SetUp(ItemData data)
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
