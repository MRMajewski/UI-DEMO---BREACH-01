using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
public class ClassDetailsElementUI : MonoBehaviour
{

    [SerializeField]
    protected TextMeshProUGUI SimpleDescription;
    [SerializeField]
    protected TextMeshProUGUI DetailedDescriptionText;

    [SerializeField]
    protected GameObject DetailedDescriptionGameObject;

    [SerializeField]
    protected bool isOpen = false;

    public virtual void Initialize(SubClassInfo data)
    { 
        SimpleDescription.text = data.subClassName;

        DetailedDescriptionText.text = data.subClassDescription;

        DetailedDescriptionGameObject.SetActive(isOpen);
    }

    public virtual void SelectionClick()
    {
        isOpen = !isOpen;

        DetailedDescriptionGameObject.gameObject.SetActive(isOpen);

        LayoutRebuilder.ForceRebuildLayoutImmediate(DetailedDescriptionText.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.transform.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.transform.parent.GetComponent<RectTransform>());
    }
}
