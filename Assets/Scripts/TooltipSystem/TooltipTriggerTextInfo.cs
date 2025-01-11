using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "TooltipInfo", menuName = "TooltipSystem/Create TooltipInfo", order = 1)]
public class TooltipTriggerTextInfo : ScriptableObject
{
    [SerializeField]
    private string triggerName;
    public string TriggerName { get => triggerName; }

    [SerializeField]
    private string tooltipText;
    public string TooltipText { get => tooltipText; }
}
