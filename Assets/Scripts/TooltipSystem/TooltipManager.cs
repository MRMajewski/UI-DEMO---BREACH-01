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
        throw new NotImplementedException();
    }

}

public enum MultiTooltipType
{
    Automatic,
    Timed,
    Disabled
}