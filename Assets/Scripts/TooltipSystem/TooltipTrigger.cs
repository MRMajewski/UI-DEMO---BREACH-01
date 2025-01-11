using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    [SerializeField]
    private string tooltipText;
    public string TooltipText { get => tooltipText; set => tooltipText = value; }
}
