using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class ButtonsGeneralReactionsManager :  MonoBehaviour
{
    public static ButtonsGeneralReactionsManager Instance { get; private set; }

    public enum ButtonAnimationType
    {
        Scale,
        Color,
        ScaleSubtle
    }
    [SerializeField]
    private float bigSize = 1.2f;

    [SerializeField]
    private float bigSizeSubtle = 1.1f;

    [SerializeField]
    private float normalSize = 1f;

    [SerializeField]
    private Color normalColor = Color.white;
    [SerializeField]
    private Color highlightColor = Color.yellow;

    [SerializeField]
    private float tweenTime = 0.2f;

    [SerializeField]
    private Ease easeTypeScale;

    [SerializeField]
    private Ease easeTypeColor;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    #region Select Methods
    public void OnButtonSelected(ButtonAnimationType animType, Transform buttonTransform)
    {
        if (animType == ButtonAnimationType.Scale)
        {
            ScaleUp(buttonTransform);

        }
        else if (animType == ButtonAnimationType.Color)
        {
            HighlightColor(buttonTransform);
        }
        else if (animType == ButtonAnimationType.ScaleSubtle)
        {
            ScaleUpSubtle(buttonTransform);
        }
    }
    public void ScaleUp(Transform buttonTransform)
    {
        buttonTransform.DOScale(bigSize, tweenTime).SetEase(easeTypeScale);
    }
    public void ScaleUpSubtle(Transform buttonTransform)
    {
        buttonTransform.DOScale(bigSizeSubtle, tweenTime).SetEase(easeTypeScale);
    }
    public void HighlightColor(Transform buttonTransform)
    {
    }

    #endregion

    #region Deselect Methods
    public void OnButtonDeselected(ButtonAnimationType animType, Transform buttonTransform)
    {
        if (animType == ButtonAnimationType.Scale || animType==ButtonAnimationType.ScaleSubtle)
        {
            ScaleDown(buttonTransform);

        }
        else if (animType == ButtonAnimationType.Color)
        {
            NormalColor(buttonTransform);
        }
    }


    public void ScaleDown(Transform buttonTransform)
    {
        buttonTransform.DOScale(normalSize, tweenTime).SetEase(easeTypeScale);
    }

    public void NormalColor(Transform buttonTransform)
    {
    }
    #endregion
}
