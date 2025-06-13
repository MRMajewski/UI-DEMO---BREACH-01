using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum ItemCategory
{
    Bro�,
    Pancerz,
    Akcesorium,
    Bro�_Palna,
    Bro�_Bia�a,
    Cywilny,
    Militarny,
    Eksperymentalny,
    Pistolet,
    SMG,
    Karabin,
    Karabin_Szturmowy,
    Bro�_Ci�ka,
    Strzelba,
    Narz�dzie,
    Inne
}

public class ItemElementUI : MonoBehaviour
{
    [SerializeField] 
    private Image icon;
    [SerializeField] 
    private TextMeshProUGUI nameText;
    [SerializeField] 
    private Button button;
    [SerializeField]
    private ItemDetailsPanel itemDetailsPanel;

    private ItemData itemData;

    public Button Button { get => button;}

    public virtual void SetUp(ItemData data)
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
