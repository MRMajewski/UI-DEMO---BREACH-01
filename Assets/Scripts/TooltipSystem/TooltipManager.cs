using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    #region Variables

    public static TooltipManager Instance;

    // [SerializeField]
    // private TooltipUI tooltipPrefab;
    [SerializeField]
    private TooltipUI currentTooltip;
    public TooltipUI CurrentTooltip => currentTooltip;

    [SerializeField]
    private List<TooltipAction> tooltipActions;

    [SerializeField]
    protected RectTransform tooltipsContainer;
    public RectTransform TooltipsContainer { get => tooltipsContainer; set => tooltipsContainer = value; }

    private RectTransform inspectedRectTransform;
    public RectTransform InspectedRectTransform { get => inspectedRectTransform; set => inspectedRectTransform = value; }

    //[SerializeField]
    //private TooltipTrigger currentlyInspectedTrigger;
    //public TooltipTrigger CurrentlyInspectedTrigger { get => currentlyInspectedTrigger; set => currentlyInspectedTrigger = value; }

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

    public bool IsDuringAnimation { get; private set; } = false;

    [SerializeField]
    private Vector2 offsideFromScreenBorder;

    [SerializeField] private CanvasGroup backgroundOverlay;

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

    //internal void CreateCurrentTooltip(TooltipTrigger trigger)
    //{
    //    if (IsDuringAnimation) return;

    //    if (tooltipPrefab == null)
    //    {
    //        Debug.LogError("Tooltip prefab is not assigned!");
    //        return;
    //    }
    //    TooltipUI tooltipInstance = Instantiate(tooltipPrefab, tooltipsContainer);

    //    tooltipInstance.Trigger = trigger;

    //    currentTooltip = tooltipInstance;

    //    if (currentTooltip == null)
    //    {
    //        Debug.LogError("Tooltip prefab does not contain a TooltipUI component!");
    //        Destroy(tooltipInstance.gameObject);
    //        return;
    //    }

    //    RectTransform tooltipRect = tooltipInstance.GetComponent<RectTransform>();

    //    tooltipInstance.gameObject.SetActive(true);
    //    tooltipsQueque.Add(currentTooltip);
    //}
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
        if (IsDuringAnimation) return;

        IsDuringAnimation = true;
        RepositionToolTip();
        OpenTooltipUI();
    }

    //internal void ShowTooltip()
    //{
    //    if (IsDuringAnimation || currentTooltip == null) return;

    //    IsDuringAnimation = true;
    //    RepositionToolTip();

    //    if (hideSequence != null) hideSequence.Kill();
    //    if (showSequence != null) showSequence.Kill();

    //    currentTooltip.CanvasGroup.alpha = 0;

    //    showSequence = DOTween.Sequence();

    //    if (tooltipBackground != null)
    //    {
    //        showSequence.Append(tooltipBackground.DOFade(1f, tweenSpeed).OnUpdate(() =>
    //        {
    //            tooltipBackground.interactable = tooltipBackground.alpha > 0.9f;
    //            tooltipBackground.blocksRaycasts = tooltipBackground.alpha > 0.9f;
    //        }));
    //    }

    //    showSequence.Append(currentTooltip.CanvasGroup.DOFade(1f, tweenSpeed))
    //        .OnComplete(() => IsDuringAnimation = false)
    //        .SetUpdate(true);
    //}

    private void OpenTooltipUI()
    {
        if (hideSequence != null) hideSequence.Kill();
        currentTooltip.gameObject.SetActive(true);
        currentTooltip.CanvasGroup.alpha = 0;

        backgroundOverlay.gameObject.SetActive(true);
        backgroundOverlay.alpha = 0;
        backgroundOverlay.blocksRaycasts = true;
        backgroundOverlay.interactable = true;

        showSequence = DOTween.Sequence();
        showSequence
        .Append(currentTooltip.CanvasGroup.DOFade(1f, tweenSpeed))
        .Join(backgroundOverlay.DOFade(1f, tweenSpeed))
        .OnComplete(() => IsDuringAnimation = false)
        .SetUpdate(true);
    }

    public void RepositionToolTip()
    {
        currentTooltip.transform.SetParent(tooltipsContainer);
        currentTooltip.transform.SetAsLastSibling();
        currentTooltip.transform.localScale = new Vector3(1, 1, 1);
        currentTooltip.transform.localRotation = Quaternion.identity;

        RectTransform rectTransform = currentTooltip.GetComponent<RectTransform>();

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, inspectedRectTransform.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(tooltipsContainer,screenPoint,null,out Vector2 localPoint);

        rectTransform.anchoredPosition = localPoint;

        AdjustTooltipPosition(inspectedRectTransform, rectTransform, tooltipsContainer);

        rectTransform.anchoredPosition = AdjustWontLeaveScreen(rectTransform, tooltipsContainer, rectTransform.anchoredPosition);

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
            float minX = (CanvasRect.rect.width * -0.5f) + (currentRect.rect.width * 0.5f)+offsideFromScreenBorder.x;
            float maxX = (CanvasRect.rect.width * 0.5f) - (currentRect.rect.width * 0.5f) - offsideFromScreenBorder.x;
            float minY = (CanvasRect.rect.height * -0.5f) + (currentRect.rect.height * 0.5f) - offsideFromScreenBorder.y;
            float maxY = (CanvasRect.rect.height * 0.5f) - (currentRect.rect.height * 0.5f) + offsideFromScreenBorder.y;

            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

            return newPos;
        }
    }

    #endregion

    #region Hide Tooltips Methods
    internal void HideTooltip()
    {
        if (IsDuringAnimation) return;

        IsDuringAnimation = true;
      //  tooltipsQueque.Remove(tooltip);
        CloseTooltipUI();
    }

    //internal void HideAllTooltips()
    //{
    //    if (IsDuringAnimation || tooltipsQueque.Count == 0)
    //        return;

    //    IsDuringAnimation = true;
    //    foreach (TooltipUI tooltip in tooltipsQueque)
    //    {
    //        CloseTooltipUI(tooltip);
    //    }
    //    tooltipsQueque.Clear();
    //    tooltipsQueque.TrimExcess();

    //    currentTooltip.gameObject.SetActive(true);
    //}

    private void CloseTooltipUI()
    {
        if (showSequence != null) showSequence.Kill();

        //  if(currentTooltip.Trigger != null)
        //  currentTooltip.Trigger.IsTriggered = false;

        //backgroundOverlay.gameObject.SetActive(true);
        //backgroundOverlay.alpha = 0;
        //backgroundOverlay.blocksRaycasts = true;
        //backgroundOverlay.interactable = true;


        hideSequence = DOTween.Sequence();
        hideSequence
            .Append(currentTooltip.CanvasGroup.DOFade(0f, tweenSpeed))
              .Join(backgroundOverlay.DOFade(0f, tweenSpeed))
            .OnComplete(() =>
            {
                IsDuringAnimation = false;

                currentTooltip.gameObject.SetActive(false);
                backgroundOverlay.gameObject.SetActive(false);
                backgroundOverlay.alpha = 0;
                backgroundOverlay.blocksRaycasts = true;
                backgroundOverlay.interactable = true;
            })
            .SetUpdate(true);
    }
    #endregion
}

