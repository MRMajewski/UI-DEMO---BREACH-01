using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private static MultiTooltipManager instance;

    private bool isPosXFirstChecked = true;

    [SerializeField]
    private MultiTooltipTriggerInputAdder dataAdder;
    public MultiTooltipTriggerInputAdder DataAdder { get => dataAdder; }

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
        currentTooltip.ResizeTooltip();
    }

    internal void ShowTooltip()
    {
        throw new NotImplementedException();
    }

    internal void CloseTooltipUI()
    {
      //  Destroy(currentTooltip.gameObject);
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
      

        RectTransform rectTransform = currentTooltip.GetComponent<RectTransform>();
        rectTransform.position = GetCurrentTooltipNewPos();
        // currentTooltip.transform.localPosition = AdjustWontOverrlapTrigger(rectTransform);
        //   currentTooltip.transform.localPosition = AdjustWontLeaveScreen(rectTransform, tooltipsContainer, currentTooltip.transform.localPosition);
        rectTransform.position = AdjustWontLeaveScreen(rectTransform, tooltipsContainer, rectTransform.localPosition);
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

    public Vector3 AdjustWontOverrlapTrigger(RectTransform currentRect)
    {
     //   isPosXFirstChecked = GenerateRandomBool();

        if (isPosXFirstChecked)
        {
            currentTooltip.transform.localPosition = AdjustWontOverrlapTriggerPosX(currentRect);
            if (CheckIfTooltipOverrlapTrigger(currentTooltip.GetComponent<RectTransform>(), inspectedRectTransformDummy))
                currentTooltip.transform.localPosition = AdjustWontOverrlapTriggerPosY(currentTooltip.GetComponent<RectTransform>());
        }
        else
        {
            currentTooltip.transform.localPosition = AdjustWontOverrlapTriggerPosY(currentRect);
            if (CheckIfTooltipOverrlapTrigger(currentTooltip.GetComponent<RectTransform>(), inspectedRectTransformDummy))
                currentTooltip.transform.localPosition = AdjustWontOverrlapTriggerPosX(currentTooltip.GetComponent<RectTransform>());
        }
        return currentTooltip.transform.localPosition;
    }

    public bool CheckIfTooltipOverrlapTrigger(RectTransform tooltip, RectTransform trigger)
    {
        tooltip.transform.SetParent(overrlapCheckContainer);
        tooltip.transform.SetAsLastSibling();

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

        return isOverrlapping;
    }

    public Vector3 AdjustWontOverrlapTriggerPosX(RectTransform currentRect)
    {
        if (CheckIfTooltipOverrlapTrigger(currentRect, inspectedRectTransformDummy))
        {
            if (inspectedRectTransformDummy.transform.localPosition.x >= 0)
            {
                CurrentTooltipNewPosition.x = CurrentTooltipNewPosition.x - offsetTooltipX - currentRect.rect.width / 2;
            }
            else if (inspectedRectTransformDummy.transform.localPosition.x < 0)
            {
                CurrentTooltipNewPosition.x = CurrentTooltipNewPosition.x + offsetTooltipX + currentRect.rect.width / 2;
            }
        }
        return CurrentTooltipNewPosition;
    }

    public Vector3 AdjustWontOverrlapTriggerPosY(RectTransform currentRect)
    {
        if (CheckIfTooltipOverrlapTrigger(currentRect, inspectedRectTransformDummy))
        {
            if (inspectedRectTransformDummy.transform.localPosition.y >= 0)
            {
                CurrentTooltipNewPosition.y = CurrentTooltipNewPosition.y - offsetTooltipY - currentRect.rect.height / 2;
            }
            else if (inspectedRectTransformDummy.transform.localPosition.y < 0)
            {
                CurrentTooltipNewPosition.y = CurrentTooltipNewPosition.y + offsetTooltipY + currentRect.rect.height / 2;
            }
        }
        return CurrentTooltipNewPosition;
    }

    //private bool GenerateRandomBool()
    //{
        //int randomIndex = Random.Range(0, 100);

        //if (randomIndex > 49)
        //{
        //    return true;
        //}
        //else return false;
   // }

    public Vector3 AdjustWontLeaveScreen(RectTransform currentRect, RectTransform CanvasRect, Vector3 newPos)
    {
        float minX = (CanvasRect.rect.width - currentRect.sizeDelta.x) * -0.5f;
        float maxX = (CanvasRect.rect.width - currentRect.sizeDelta.x) * 0.5f;
        float minY = (CanvasRect.rect.height - currentRect.sizeDelta.y) * -0.5f;
        float maxY = (CanvasRect.rect.height - currentRect.sizeDelta.y) * 0.5f;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        return newPos;
    }
}

public enum MultiTooltipType
{
    Automatic,
    Timed,
    Disabled
}