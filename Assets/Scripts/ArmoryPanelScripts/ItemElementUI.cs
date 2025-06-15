using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class ItemElementUI : MonoBehaviour
{
    [SerializeField] 
    private Image icon;
    [SerializeField] 
    private TextMeshProUGUI nameText;
    [SerializeField] 
    private Button button;

    private ItemData itemData;

    public Button Button { get => button;}

    public virtual void SetUp(ItemData data, System.Action<ItemData> onClick)
    {
        itemData = data;
        icon.sprite = data.icon;
        nameText.text = data.itemName;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke(itemData));
    }
}
