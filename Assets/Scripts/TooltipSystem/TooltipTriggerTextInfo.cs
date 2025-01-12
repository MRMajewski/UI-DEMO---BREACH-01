using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[Serializable]
public struct TooltipAction
{
    public string actionName; // Nazwa metody
    public UnityEvent actionEvent; // Event, który mo¿na przypisaæ w edytorze

    public void Invoke()
    {
        actionEvent?.Invoke();
    }
}


[Serializable]
[CreateAssetMenu(fileName = "TooltipInfo", menuName = "TooltipSystem/Create TooltipInfo", order = 1)]
public class TooltipTriggerTextInfo : ScriptableObject
{
    [SerializeField]
    private string triggerName;
    public string TriggerName => triggerName;

    [SerializeField]
    private string tooltipText;
    public string TooltipText => tooltipText;

    [SerializeField]
    private TooltipType tooltipType = TooltipType.Simple;
    public TooltipType TooltipType => tooltipType;

    [SerializeField]
    private string actionName; // Nowa struktura akcji
    public string ActionName => actionName;
}

public enum TooltipType
{
    Simple,
    WithButton
}
