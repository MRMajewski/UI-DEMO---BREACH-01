using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    [SerializeField]
    private GameObject tooltipPrefab;

    private TooltipUI currentTooltip;

    [SerializeField]
    public TooltipUI CurrentTooltip
    {
        get=>currentTooltip; set=>currentTooltip=value;
    }
    [SerializeField]
    private List<TooltipAction> tooltipActions;

    public List<TooltipAction> TooltipActions => tooltipActions;


    #region Variables

    [SerializeField]
    protected RectTransform tooltipsContainer;
    public RectTransform TooltipsContainer { get => tooltipsContainer; set => tooltipsContainer = value; }

    private RectTransform inspectedRectTransform;
    public RectTransform InspectedRectTransform { get => inspectedRectTransform; set => inspectedRectTransform = value; }
    [SerializeField]
    private RectTransform inspectedRectTransformDummy;

    [Space]
    [Header("Tween related refs")]
    private Sequence showSequence;
    private Sequence hideSequence;

    [SerializeField]
    private float tweenSpeed;
    [SerializeField]
    private float tweenDelay;

    [SerializeField]
    private float timerDelay;
    public float TimerDelay { get => timerDelay; }

    [Space]
    [Header("Tooltip related refs")]
    [SerializeField]
    private MultiTooltipType multiTooltipType = MultiTooltipType.Automatic;
    public MultiTooltipType MultiTooltipType { get => multiTooltipType; set => multiTooltipType = value; }
    [SerializeField]
    private int multiTooltipTypeIndex = 0;
    [SerializeField]
    private int previousMultiTooltipTypeIndex;


    [SerializeField]
    private GameObject tooltipPrefabBase;

    [SerializeField]
    private TooltipInteractionHandler tooltipHandler;

    [SerializeField]
    private int hideFirstTooltipsBorderIndex = 3;

    [SerializeField]
    private List<TooltipUI> tooltipsQueque;
    public List<TooltipUI> TooltipsQueque { get => tooltipsQueque; set => tooltipsQueque = value; }

    [SerializeField]
    private List<TooltipUI> hiddenTooltipsQueque;

    [Space]
    [Header("Privates")]
    private float offsetTooltipX;
    private float offsetTooltipY;


    private bool isPosXFirstChecked = true;



    [SerializeField]
    protected RectTransform overrlapCheckContainer;
    public RectTransform OverrlapCheckContainer { get; set; }

    [SerializeField]
    private Vector3 CurrentTooltipNewPosition;

    [SerializeField]
    private TextMeshProUGUI testInfoText;

    public object MultitooltipsQueque { get; internal set; }
    #endregion

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
    public void Test1()
    {
        Debug.Log("TEST 1");
    }
    public void Test1(int index)
    {
        Debug.Log("TEST WITH INT :" + index);
    }
    public void ExecuteAction(string actionName)
    {
        var action = tooltipActions.Find(a => a.actionName == actionName);
        if (action.actionEvent != null)
        {
            action.Invoke();
        }
        else
        {
            Debug.LogWarning($"Akcja '{actionName}' nie zosta³a znaleziona!");
        }
    }

    internal void CreateCurrentTooltip()
    {
        // Upewnij siê, ¿e prefab tooltipu zosta³ ustawiony
        if (tooltipPrefab == null)
        {
            Debug.LogError("Tooltip prefab is not assigned!");
            return;
        }

        // Utwórz instancjê tooltipu
        GameObject tooltipInstance = Instantiate(tooltipPrefab, tooltipsContainer);

        // Przypisz instancjê do `currentTooltip`
        currentTooltip = tooltipInstance.GetComponent<TooltipUI>();

        // SprawdŸ, czy komponent TooltipUI istnieje
        if (currentTooltip == null)
        {
            Debug.LogError("Tooltip prefab does not contain a TooltipUI component!");
            Destroy(tooltipInstance);
            return;
        }

        RectTransform tooltipRect = tooltipInstance.GetComponent<RectTransform>();
    
        // Ustaw tooltip jako aktywny
        tooltipInstance.SetActive(true);


        // W razie potrzeby dostosuj rozmiar tooltipu
     //   currentTooltip.ResizeTooltip();
    }

    internal void ShowTooltip()
    {
        RepositionToolTip();
        OpenTooltipUI();

    }

    public void OpenTooltipUI()
    {
        if (hideSequence != null) hideSequence.Kill();

        currentTooltip.CanvasGroup.alpha = 0;

        showSequence = DOTween.Sequence();
        showSequence
        .Append(currentTooltip.CanvasGroup.DOFade(1f, tweenSpeed))
        .SetUpdate(true);
    }

    internal void HideTooltip()
    {
        CloseTooltipUI();
        Destroy(currentTooltip.gameObject);
    }


    public void CloseTooltipUI()
    {
        if (showSequence != null) showSequence.Kill();

        hideSequence = DOTween.Sequence();
        hideSequence
        .Append(currentTooltip.CanvasGroup.DOFade(0f, tweenSpeed))
        .AppendInterval(tweenSpeed).SetUpdate(true);
    }
    public void RepositionToolTip()
    {
        currentTooltip.transform.SetParent(tooltipsContainer);
        currentTooltip.transform.SetAsLastSibling();
        currentTooltip.transform.localScale = new Vector3(1, 1, 1);
        currentTooltip.transform.localRotation = Quaternion.identity;
        offsetTooltipX = inspectedRectTransform.rect.width / 2;
        offsetTooltipY = inspectedRectTransform.rect.height / 2;

       // currentTooltip.transform.localPosition = GetCurrentTooltipNewPos();
      
        //TO DZIA£A
        RectTransform rectTransform = currentTooltip.GetComponent<RectTransform>();
        rectTransform.position = GetCurrentTooltipNewPos();


        /// TU TESTY!
        //   rectTransform.anchoredPosition = AdjustWontOverrlapTrigger(rectTransform);
        AdjustTooltipPosition(inspectedRectTransform, rectTransform, tooltipsContainer);


       
        /// 
        //TO TE¯ DZIA£¥!
        rectTransform.anchoredPosition = AdjustWontLeaveScreenTest(rectTransform, tooltipsContainer, rectTransform.anchoredPosition);


    }

    public Vector3 GetCurrentTooltipNewPos()
    {
        inspectedRectTransformDummy.transform.SetParent(inspectedRectTransform.transform.parent);
        inspectedRectTransformDummy.transform.SetAsLastSibling();

        inspectedRectTransformDummy.sizeDelta = inspectedRectTransform.sizeDelta;
        inspectedRectTransformDummy.pivot = inspectedRectTransform.pivot;
        inspectedRectTransformDummy.anchoredPosition = inspectedRectTransform.anchoredPosition;
        inspectedRectTransformDummy.transform.localPosition = inspectedRectTransform.transform.localPosition;

        inspectedRectTransformDummy.transform.SetParent(overrlapCheckContainer);
        CurrentTooltipNewPosition = inspectedRectTransformDummy.localPosition;
        return CurrentTooltipNewPosition;
    }

    #region tests
    public static void AdjustTooltipPosition(RectTransform triggerRect, RectTransform tooltipRect, RectTransform canvasRect)
    {
        // 1. Pobierz pozycje w przestrzeni ekranowej
        Vector2 triggerScreenPosition = RectTransformUtility.WorldToScreenPoint(null, triggerRect.position);
        Vector2 tooltipScreenPosition = RectTransformUtility.WorldToScreenPoint(null, tooltipRect.position);

        // 2. Zamieñ pozycje ekranowe na lokalne w przestrzeni canvasu
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, triggerScreenPosition, null, out Vector2 triggerLocalPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, tooltipScreenPosition, null, out Vector2 tooltipLocalPosition);

        // 3. Pobierz rozmiary triggera i tooltipa
        Vector2 triggerSize = triggerRect.rect.size;
        Vector2 tooltipSize = tooltipRect.rect.size;

        // 4. SprawdŸ kolizje na osiach X i Y
        bool isOverlappingX = Mathf.Abs(triggerLocalPosition.x - tooltipLocalPosition.x) < (triggerSize.x + tooltipSize.x) / 2;
        bool isOverlappingY = Mathf.Abs(triggerLocalPosition.y - tooltipLocalPosition.y) < (triggerSize.y + tooltipSize.y) / 2;

        // 5. Przesuwaj tooltip
        Vector2 adjustedPosition = tooltipLocalPosition;

        if (isOverlappingX)
        {
            // Przesuñ w osi X tak, aby tooltip styka³ siê z triggerem
            if (triggerLocalPosition.x < 0)
                adjustedPosition.x = triggerLocalPosition.x + triggerSize.x / 2 + tooltipSize.x / 2;
            else
                adjustedPosition.x = triggerLocalPosition.x - triggerSize.x / 2 - tooltipSize.x / 2;
        }

        if (isOverlappingY)
        {
            // Przesuñ w osi Y tak, aby tooltip styka³ siê z triggerem
            if (triggerLocalPosition.y < 0)
                adjustedPosition.y = triggerLocalPosition.y + triggerSize.y / 2 + tooltipSize.y / 2;
            else
                adjustedPosition.y = triggerLocalPosition.y - triggerSize.y / 2 - tooltipSize.y / 2;
        }

        // 6. Na³ó¿ pozycjê
        tooltipRect.anchoredPosition = adjustedPosition;
    }
    private static Rect GetScreenRect(RectTransform rectTransform, RectTransform canvasRect)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        // Konwersja na wspó³rzêdne przestrzeni ekranu (dla Screen Space - Overlay)
        Vector2 size = new Vector2(corners[2].x - corners[0].x, corners[2].y - corners[0].y);
        Vector2 position = new Vector2(corners[0].x, corners[0].y);

        return new Rect(position, size);
    }

    private static bool IsOverlappingInX(Rect rectA, Rect rectB)
    {
        return rectA.xMin < rectB.xMax && rectA.xMax > rectB.xMin;
    }

    private static bool IsOverlappingInY(Rect rectA, Rect rectB)
    {
        return rectA.yMin < rectB.yMax && rectA.yMax > rectB.yMin;
    }

    private static void AdjustInX(Rect triggerRect, RectTransform tooltipRect, RectTransform canvasRect)
    {
        Vector3 tooltipPosition = tooltipRect.anchoredPosition;

        float canvasWidth = canvasRect.rect.width;

        if (triggerRect.center.x < canvasWidth / 2)
        {
            // Przesuñ tooltip w prawo
            tooltipPosition.x = triggerRect.xMax + (tooltipRect.rect.width / 2);
        }
        else
        {
            // Przesuñ tooltip w lewo
            tooltipPosition.x = triggerRect.xMin - (tooltipRect.rect.width / 2);
        }

        // Upewnij siê, ¿e tooltip nie wychodzi poza ekran
        tooltipPosition.x = Mathf.Clamp(tooltipPosition.x, 0, canvasWidth - tooltipRect.rect.width);

        tooltipRect.anchoredPosition = tooltipPosition;
    }

    private static void AdjustInY(Rect triggerRect, RectTransform tooltipRect, RectTransform canvasRect)
    {
        Vector3 tooltipPosition = tooltipRect.anchoredPosition;

        float canvasHeight = canvasRect.rect.height;

        if (triggerRect.center.y < canvasHeight / 2)
        {
            // Przesuñ tooltip w górê
            tooltipPosition.y = triggerRect.yMax + (tooltipRect.rect.height / 2);
        }
        else
        {
            // Przesuñ tooltip w dó³
            tooltipPosition.y = triggerRect.yMin - (tooltipRect.rect.height / 2);
        }

        // Upewnij siê, ¿e tooltip nie wychodzi poza ekran
        tooltipPosition.y = Mathf.Clamp(tooltipPosition.y, 0, canvasHeight - tooltipRect.rect.height);

        tooltipRect.anchoredPosition = tooltipPosition;
    }
    #endregion

    public Vector2 AdjustWontOverrlapTrigger(RectTransform currentRect)
    {
       isPosXFirstChecked = GenerateRandomBool();

        if (isPosXFirstChecked)
        {
            currentRect.anchoredPosition = AdjustWontOverrlapTriggerPosX(currentRect);
            if (CheckIfTooltipOverrlapTrigger(currentTooltip.GetComponent<RectTransform>(), inspectedRectTransformDummy))
                currentRect.anchoredPosition = AdjustWontOverrlapTriggerPosY(currentTooltip.GetComponent<RectTransform>());
        }
        else
        {
            currentRect.anchoredPosition = AdjustWontOverrlapTriggerPosY(currentRect);
            if (CheckIfTooltipOverrlapTrigger(currentTooltip.GetComponent<RectTransform>(), inspectedRectTransformDummy))
                currentRect.anchoredPosition = AdjustWontOverrlapTriggerPosX(currentTooltip.GetComponent<RectTransform>());
        }
        return currentRect.anchoredPosition;
    }

    public bool CheckIfTooltipOverrlapTrigger(RectTransform tooltip, RectTransform trigger)
    {
        Transform triggerPreviousParent = trigger.parent;

        tooltip.transform.SetParent(overrlapCheckContainer);
        tooltip.transform.SetAsLastSibling();

        trigger.transform.SetParent(overrlapCheckContainer);
        trigger.transform.SetAsLastSibling();

        Vector2 minMaxValuesX;
        Vector2 minMaxValuesY;

        minMaxValuesX.x = trigger.localPosition.x - (trigger.rect.width / 2);
        minMaxValuesX.y = trigger.localPosition.x + (trigger.rect.width / 2);
        minMaxValuesY.x = trigger.localPosition.y - (trigger.rect.height / 2);
        minMaxValuesY.y = trigger.localPosition.y + (trigger.rect.height / 2);

        bool isOverrlapping;
        if (tooltip.localPosition.x >= minMaxValuesX.x & tooltip.localPosition.x <= minMaxValuesX.y)
        {
            if (tooltip.localPosition.y >= minMaxValuesY.x & tooltip.localPosition.y <= minMaxValuesY.y)
            {
                isOverrlapping = true;
            }
            else
            {
                isOverrlapping = false;
            }
        }
        else
        {
            isOverrlapping = false;
        }
        tooltip.transform.SetParent(tooltipsContainer);
        tooltip.transform.SetAsLastSibling();

        trigger.transform.SetParent(triggerPreviousParent);
        trigger.transform.SetAsLastSibling();

        return isOverrlapping;
    }

    public Vector3 AdjustWontOverrlapTriggerPosX(RectTransform currentRect)
    {
        if (CheckIfTooltipOverrlapTrigger(currentRect, inspectedRectTransformDummy))
        {
            if (inspectedRectTransformDummy.anchoredPosition.x >= 0)
            {
                CurrentTooltipNewPosition.x = CurrentTooltipNewPosition.x - offsetTooltipX - currentRect.rect.width / 2;
            }
            else if (inspectedRectTransformDummy.anchoredPosition.x < 0)
            {
                CurrentTooltipNewPosition.x = CurrentTooltipNewPosition.x + offsetTooltipX + currentRect.rect.width / 2;
            }
        }
        return CurrentTooltipNewPosition;
    }

    public Vector2 AdjustWontOverrlapTriggerPosY(RectTransform currentRect)
    {
        if (CheckIfTooltipOverrlapTrigger(currentRect, inspectedRectTransformDummy))
        {
            if (inspectedRectTransformDummy.anchoredPosition.y >= 0)
            {
                CurrentTooltipNewPosition.y = CurrentTooltipNewPosition.y - offsetTooltipY - currentRect.rect.height / 2;
            }
            else if (inspectedRectTransformDummy.anchoredPosition.y < 0)
            {
                CurrentTooltipNewPosition.y = CurrentTooltipNewPosition.y + offsetTooltipY + currentRect.rect.height / 2;
            }
        }
        return CurrentTooltipNewPosition;
    }

    private bool GenerateRandomBool()
    {
        int randomIndex = UnityEngine.Random.Range(0, 100);

        if (randomIndex > 49)
        {
            return true;
        }
        else return false;
    }

    public Vector3 AdjustWontLeaveScreen(RectTransform currentRect, RectTransform CanvasRect, Vector2 newPos)
    {
        float minX = (CanvasRect.rect.width - currentRect.sizeDelta.x) * -0.5f;
        float maxX = (CanvasRect.rect.width - currentRect.sizeDelta.x) * 0.5f;
        float minY = (CanvasRect.rect.height - currentRect.sizeDelta.y) * -0.5f;
        float maxY = (CanvasRect.rect.height - currentRect.sizeDelta.y) * 0.5f;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        return newPos;
    }

    public Vector2 AdjustWontLeaveScreenTest(RectTransform currentRect, RectTransform CanvasRect, Vector3 newPos)
    {
       // Vector3 newPos = new Vector3();
        float minX = (CanvasRect.rect.width * -0.5f) + (currentRect.rect.width * 0.5f);
        float maxX = (CanvasRect.rect.width * 0.5f) - (currentRect.rect.width * 0.5f);
        float minY = (CanvasRect.rect.height * -0.5f) + (currentRect.rect.height * 0.5f);
        float maxY = (CanvasRect.rect.height * 0.5f) - (currentRect.rect.height * 0.5f);

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        return newPos;
    }

    //public void AdjustToScreen()
    //{

    //    // Przeliczenie pozycji do lokalnych wspó³rzêdnych canvasu
    //    Vector3 localPosition = targetRect.localPosition;

    //    // Wyliczenie krawêdzi elementu wzglêdem centrum canvasu
    //    float minX = (canvasRect.rect.width * -0.5f) + (targetRect.rect.width * 0.5f);
    //    float maxX = (canvasRect.rect.width * 0.5f) - (targetRect.rect.width * 0.5f);
    //    float minY = (canvasRect.rect.height * -0.5f) + (targetRect.rect.height * 0.5f);
    //    float maxY = (canvasRect.rect.height * 0.5f) - (targetRect.rect.height * 0.5f);

    //    // Korekta pozycji w osi X i Y
    //    localPosition.x = Mathf.Clamp(localPosition.x, minX, maxX);
    //    localPosition.y = Mathf.Clamp(localPosition.y, minY, maxY);

    //    // Zastosowanie nowej pozycji
    //    targetRect.localPosition = localPosition;
    //}
}

public enum MultiTooltipType
{
    Automatic,
    Timed,
    Disabled
}