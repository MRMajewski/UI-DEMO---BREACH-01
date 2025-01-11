using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    [SerializeField]
    private GameObject tooltipPrefab;

    private TooltipUI currentTooltip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowTooltip(string tooltipText, Vector3 position, System.Action buttonAction = null)
    {
        if (currentTooltip != null)
        {
            Destroy(currentTooltip.gameObject);
        }

        GameObject tooltipObject = Instantiate(tooltipPrefab, transform);
        currentTooltip = tooltipObject.GetComponent<TooltipUI>();

        currentTooltip.SetTooltip(tooltipText, buttonAction);
        currentTooltip.transform.position = position;
    }

    public void HideTooltip()
    {
        if (currentTooltip != null)
        {
            Destroy(currentTooltip.gameObject);
            currentTooltip = null;
        }
    }
}
