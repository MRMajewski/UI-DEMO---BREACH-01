using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIElementsSnapper : MonoBehaviour
{

    public RectTransform content;

    private int currentPanelIndex = 0;
    private bool isHorizontalSwipe = false;
    private Vector2 touchStartPosition;
    private bool hasSwiped = false;

    private float swipeThreshold;

    public event Action<int> OnPanelChanged;

    private List<ISnappedElement> snappedElements = new();

    private void Start()
    {
        swipeThreshold = Screen.width * 0.15f;
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.isEditor)
        {
            HandleMouseInput(); 
        }
        else
        {
            HandleTouchInput();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartTouch(touch.position);
                    break;
                case TouchPhase.Moved:
                    MoveTouch(touch.position);
                    break;
                case TouchPhase.Ended:
                    EndTouch(touch.position);
                    break;
            }
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
            StartTouch(Input.mousePosition);
        else if (Input.GetMouseButton(0))
            MoveTouch(Input.mousePosition);
        else if (Input.GetMouseButtonUp(0))
            EndTouch(Input.mousePosition);
    }

    private void StartTouch(Vector2 position)
    {
        touchStartPosition = position;
        isHorizontalSwipe = false;
        hasSwiped = false;     
    }

    private void MoveTouch(Vector2 position)
    {
        Vector2 delta = position - touchStartPosition;

        if (!isHorizontalSwipe && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            isHorizontalSwipe = true;
        }

        if (isHorizontalSwipe && Mathf.Abs(delta.x) > swipeThreshold)
        {
            EndTouch(position);
        }
    }

    private void EndTouch(Vector2 position)
    {
        if (!isHorizontalSwipe || hasSwiped) return;

        hasSwiped = true;
        HandleHorizontalSwipe(position);
    }

    private void HandleHorizontalSwipe(Vector2 endPosition)
    {
        float dragDistance = endPosition.x - touchStartPosition.x;

        snappedElements[currentPanelIndex].ResetRectScroll();

        if (Mathf.Abs(dragDistance) > swipeThreshold)
        {
            if (dragDistance < 0)
            {
                currentPanelIndex = (currentPanelIndex + 1) % snappedElements.Count; 
            }
            else if (dragDistance > 0)
            {
                currentPanelIndex = (currentPanelIndex - 1 + snappedElements.Count) % snappedElements.Count; 
            }
            SnapToPanel(currentPanelIndex);
            OnPanelChanged?.Invoke(currentPanelIndex);

            TooltipManager.Instance.HideTooltip();
        }
    }

    private void SnapToPanel(int panelIndex)
    {
        Vector2 targetPosition = new Vector2(-snappedElements[panelIndex].GetRectTransform().anchoredPosition.x, content.anchoredPosition.y);
        content.DOAnchorPos(targetPosition, 0.3f).SetEase(Ease.InOutSine).SetUpdate(true);
    }

    public void InitPanels(List<ISnappedElement> classesDataElementsUIList)
    {
        snappedElements.Clear();
        snappedElements.TrimExcess();

        foreach (ISnappedElement item in classesDataElementsUIList)
        {
            snappedElements.Add(item);
        }
    }

    public void ResetAllScrolls()
    {
        if (snappedElements == null)
            return;
        foreach (var element in snappedElements)
        {
            element.ResetRectScroll();
        }
    }

    public void SnapToPanelFromButton(int panelIndex)
    {
        snappedElements[currentPanelIndex].ResetRectScroll();
        SnapToPanel(panelIndex);
        currentPanelIndex = panelIndex;
        OnPanelChanged?.Invoke(currentPanelIndex);
    }
}
