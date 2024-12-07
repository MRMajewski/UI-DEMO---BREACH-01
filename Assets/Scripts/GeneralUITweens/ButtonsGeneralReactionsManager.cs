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
        Color
    }
    [SerializeField]
    private float bigSize = 1.2f;
    [SerializeField]
    private float normalSize = 1f;

    [SerializeField]
    private Color normalColor = Color.white;
    [SerializeField]
    private Color highlightColor = Color.yellow;

    [SerializeField]
    private float tweenTime = 0.2f;

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
    }
    public void ScaleUp(Transform buttonTransform)
    {
        buttonTransform.DOScale(bigSize, tweenTime).SetEase(Ease.OutBounce);
    }
    public void HighlightColor(Transform buttonTransform)
    {
      //  var renderer = buttonTransform.GetComponent<SpriteRenderer>();
     //   renderer.DOColor(highlightColor, tweenTime);
    }


    #endregion

    #region Deselect Methods
    public void OnButtonDeselected(ButtonAnimationType animType, Transform buttonTransform)
    {
        if (animType == ButtonAnimationType.Scale)
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
        buttonTransform.DOScale(normalSize, tweenTime).SetEase(Ease.InBounce);
    }

    public void NormalColor(Transform buttonTransform)
    {
       
      //  buttonTransform.GetComponent<Button>().DOColor(normalColor, tweenTime);
    }
    #endregion
}
