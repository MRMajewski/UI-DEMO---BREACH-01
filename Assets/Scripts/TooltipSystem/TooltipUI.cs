using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tooltipText;
    [SerializeField]
    private Button actionButton;

    public void SetTooltip(string text, System.Action buttonAction=null)
    {
        tooltipText.text = text;

        if (buttonAction != null)
        {
            actionButton.onClick.AddListener(() => buttonAction.Invoke());
            actionButton.gameObject.SetActive(true);
        }
        else
        {
            actionButton.gameObject.SetActive(false);
        }
    }
}
