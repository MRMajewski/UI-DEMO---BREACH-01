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

    [SerializeField]
    protected UIScrollViewFitter uIScrollViewFitter;

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


        CheckVisibilityDelayed();
    }

    public void CheckVisibilityDelayed()
    {
        if (this.gameObject.activeSelf)
            StartCoroutine(EnsureVisibleNextFrame(uIScrollViewFitter));
    }

    private IEnumerator EnsureVisibleNextFrame(UIScrollViewFitter uIScrollViewFitter)
    {
        yield return new WaitForEndOfFrame();

        uIScrollViewFitter?.EnsureVisibleSmooth(this.GetComponent<RectTransform>());
    }
}
