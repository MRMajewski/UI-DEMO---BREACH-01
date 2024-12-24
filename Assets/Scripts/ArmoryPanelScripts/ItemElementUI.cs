using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum ItemCategory
{
    Weapon,
    Armor,
    Utility,
    WeaponRanged,
    WeaponMelee,
    Civilian,
    Military,
    Experimental,
    Pistol,
    SMG,
    Rifle,
    RifleAssault,
    Heavy,
    Shotgun,
    Tool,
    Miscellaneous
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
