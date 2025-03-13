using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    #region Variables

    public static TooltipManager Instance;

    [SerializeField]
    private TooltipUI tooltipPrefab;

    private TooltipUI currentTooltip;
    public TooltipUI CurrentTooltip => currentTooltip;

    [SerializeField]
    private List<TooltipAction> tooltipActions;

    [SerializeField]
    protected RectTransform tooltipsContainer;
    public RectTransform TooltipsContainer { get => tooltipsContainer; set => tooltipsContainer = value; }

    private RectTransform inspectedRectTransform;
    public RectTransform InspectedRectTransform { get => inspectedRectTransform; set => inspectedRectTransform = value; }

    [SerializeField]
    private RectTransform inspectedRectTransformDummy;

    [Space]
    [Header("Tween related refs")]

    [SerializeField]
    private float tweenSpeed;
    [SerializeField]
    private float tweenDelay;

    [Space]

    [SerializeField]
    private GameObject tooltipPrefabBase;

    [SerializeField]
    private List<TooltipUI> tooltipsQueque;

    private Sequence showSequence;
    private Sequence hideSequence;
    private Vector3 CurrentTooltipNewPosition;

    #endregion

    #region Init Methods
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

    internal void CreateCurrentTooltip()
    {
        if (tooltipPrefab == null)
        {
            Debug.LogError("Tooltip prefab is not assigned!");
            return;
        }
        TooltipUI tooltipInstance = Instantiate(tooltipPrefab, tooltipsContainer);

        currentTooltip = tooltipInstance;

        if (currentTooltip == null)
        {
            Debug.LogError("Tooltip prefab does not contain a TooltipUI component!");
            Destroy(tooltipInstance.gameObject);
            return;
        }

        RectTransform tooltipRect = tooltipInstance.GetComponent<RectTransform>();

        tooltipInstance.gameObject.SetActive(true);
        tooltipsQueque.Add(currentTooltip);
    }
    internal void ExecuteAction(string actionName)
    {
        var action = tooltipActions.Find(a => a.actionName == actionName);
        if (action.actionEvent != null)
        {
            action.Invoke();
        }
    }
    #endregion

    #region Show Tooltips Methods
    internal void ShowTooltip()
    {
        RepositionToolTip();
        OpenTooltipUI();
    }

    private void OpenTooltipUI()
    {
        if (hideSequence != null) hideSequence.Kill();

        currentTooltip.CanvasGroup.alpha = 0;

        showSequence = DOTween.Sequence();
        showSequence
        .Append(currentTooltip.CanvasGroup.DOFade(1f, tweenSpeed))
        .SetUpdate(true);
    }

    public void RepositionToolTip()
    {
        currentTooltip.transform.SetParent(tooltipsContainer);
        currentTooltip.transform.SetAsLastSibling();
        currentTooltip.transform.localScale = new Vector3(1, 1, 1);
        currentTooltip.transform.localRotation = Quaternion.identity;

        RectTransform rectTransform = currentTooltip.GetComponent<RectTransform>();

        rectTransform.position = GetCurrentTooltipNewPos();

        AdjustTooltipPosition(inspectedRectTransform, rectTransform, tooltipsContainer);

        rectTransform.anchoredPosition = AdjustWontLeaveScreen(rectTransform, tooltipsContainer, rectTransform.anchoredPosition);

        Vector3 GetCurrentTooltipNewPos()
        {
            inspectedRectTransformDummy.transform.SetParent(inspectedRectTransform.transform.parent);
            inspectedRectTransformDummy.transform.SetAsLastSibling();

            inspectedRectTransformDummy.sizeDelta = inspectedRectTransform.sizeDelta;
            inspectedRectTransformDummy.pivot = inspectedRectTransform.pivot;
            inspectedRectTransformDummy.anchoredPosition = inspectedRectTransform.anchoredPosition;
            inspectedRectTransformDummy.transform.localPosition = inspectedRectTransform.transform.localPosition;

            inspectedRectTransformDummy.transform.SetParent(null);
            Debug.Log(inspectedRectTransformDummy.transform.parent);
            CurrentTooltipNewPosition = inspectedRectTransformDummy.localPosition;
            return CurrentTooltipNewPosition;
        }


        void AdjustTooltipPosition(RectTransform triggerRect, RectTransform tooltipRect, RectTransform canvasRect)
        {

            Vector2 triggerScreenPosition = RectTransformUtility.WorldToScreenPoint(null, triggerRect.position);
            Vector2 tooltipScreenPosition = RectTransformUtility.WorldToScreenPoint(null, tooltipRect.position);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, triggerScreenPosition, null, out Vector2 triggerLocalPosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, tooltipScreenPosition, null, out Vector2 tooltipLocalPosition);

            Vector2 triggerSize = triggerRect.rect.size;
            Vector2 tooltipSize = tooltipRect.rect.size;

            bool isOverlappingX = Mathf.Abs(triggerLocalPosition.x - tooltipLocalPosition.x) < (triggerSize.x + tooltipSize.x) / 2;
            bool isOverlappingY = Mathf.Abs(triggerLocalPosition.y - tooltipLocalPosition.y) < (triggerSize.y + tooltipSize.y) / 2;

            Vector2 adjustedPosition = tooltipLocalPosition;

            if (isOverlappingX)
            {
                if (triggerLocalPosition.x < 0)
                    adjustedPosition.x = triggerLocalPosition.x + triggerSize.x / 2 + tooltipSize.x / 2;
                else
                    adjustedPosition.x = triggerLocalPosition.x - triggerSize.x / 2 - tooltipSize.x / 2;
            }

            if (isOverlappingY)
            {
                if (triggerLocalPosition.y < 0)
                    adjustedPosition.y = triggerLocalPosition.y + triggerSize.y / 2 + tooltipSize.y / 2;
                else
                    adjustedPosition.y = triggerLocalPosition.y - triggerSize.y / 2 - tooltipSize.y / 2;
            }

            tooltipRect.anchoredPosition = adjustedPosition;
        }

        Vector2 AdjustWontLeaveScreen(RectTransform currentRect, RectTransform CanvasRect, Vector3 newPos)
        {
            float minX = (CanvasRect.rect.width * -0.5f) + (currentRect.rect.width * 0.5f);
            float maxX = (CanvasRect.rect.width * 0.5f) - (currentRect.rect.width * 0.5f);
            float minY = (CanvasRect.rect.height * -0.5f) + (currentRect.rect.height * 0.5f);
            float maxY = (CanvasRect.rect.height * 0.5f) - (currentRect.rect.height * 0.5f);

            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

            return newPos;
        }
    }

    #endregion

    #region Hide Tooltips Methods
    internal void HideTooltip(TooltipUI tooltip)
    {
        tooltipsQueque.Remove(tooltip);
        CloseTooltipUI(tooltip);
    }

    internal void HideAllTooltips()
    {
        if (tooltipsQueque.Count == 0)
            return;

        foreach (TooltipUI tooltip in tooltipsQueque)
        {
            CloseTooltipUI(tooltip);
        }
        tooltipsQueque.Clear();
        tooltipsQueque.TrimExcess();
    }

    private void CloseTooltipUI(TooltipUI tooltip)
    {
        if (showSequence != null) showSequence.Kill();

        hideSequence = DOTween.Sequence();
        hideSequence
            .Append(tooltip.CanvasGroup.DOFade(0f, tweenSpeed))
            .AppendInterval(tweenSpeed)
            .OnComplete(() => Destroy(tooltip.gameObject))
            .SetUpdate(true);
    }
    #endregion
}

