using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIElementsSnapper : MonoBehaviour
{
    public List<RectTransform> panels;
    public RectTransform content;

    private int currentPanelIndex = 0;
    private bool isHorizontalSwipe = false;
    private Vector2 touchStartPosition;
    private bool hasSwiped = false;

    private float swipeThreshold;

    public event Action<int> OnPanelChanged;

    private void Start()
    {
        swipeThreshold = Screen.width * 0.15f; // Dynamiczne dopasowanie
        Debug.LogError("Swipe threshold set to: " + swipeThreshold);
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.isEditor)
        {
            HandleMouseInput(); // Obs³uga dotyku przez mysz w WebGL
        }
        else
        {
            HandleTouchInput(); // Standardowa obs³uga dotyku w aplikacjach mobilnych
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

        if (Mathf.Abs(dragDistance) > swipeThreshold)
        {
            if (dragDistance < 0)
            {
                currentPanelIndex = (currentPanelIndex + 1) % panels.Count; // Zapêtlenie w prawo
            }
            else if (dragDistance > 0)
            {
                currentPanelIndex = (currentPanelIndex - 1 + panels.Count) % panels.Count; // Zapêtlenie w lewo
            }

            SnapToPanel(currentPanelIndex);
            OnPanelChanged?.Invoke(currentPanelIndex);
        }
    }

    private void SnapToPanel(int panelIndex)
    {
        Vector2 targetPosition = new Vector2(-panels[panelIndex].anchoredPosition.x, content.anchoredPosition.y);
        content.DOAnchorPos(targetPosition, 0.3f).SetEase(Ease.InOutSine);
    }

    public void InitPanels(List<ClassDataElementUI> classesDataElementsUIList)
    {
        panels.Clear();
        panels.TrimExcess();

        foreach (ClassDataElementUI item in classesDataElementsUIList)
        {
            RectTransform newPanel = item.GetComponent<RectTransform>();
            panels.Add(newPanel);
        }
    }

    public void SnapToPanelFromButton(int panelIndex)
    {
        SnapToPanel(panelIndex);
        currentPanelIndex = panelIndex;
        OnPanelChanged?.Invoke(currentPanelIndex);
    }
}
