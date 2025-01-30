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

    private const float swipeThreshold = 50f;

    public event Action<int> OnPanelChanged; 
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            ProcessTouch(touch.phase, touch.position);
        }
        else if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
                StartTouch(Input.mousePosition);
            else
                MoveTouch(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndTouch(Input.mousePosition);
        }
    }

    private void StartTouch(Vector2 position)
    {
        touchStartPosition = position;
        isHorizontalSwipe = false;
    }

    private void MoveTouch(Vector2 position)
    {
        Vector2 delta = position - touchStartPosition;

        if (!isHorizontalSwipe && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            isHorizontalSwipe = true;
        }
    }

    private void EndTouch(Vector2 position)
    {
        if (isHorizontalSwipe)
        {
            HandleHorizontalSwipe(position);
        }
    }

    private void ProcessTouch(TouchPhase phase, Vector2 position)
    {
        switch (phase)
        {
            case TouchPhase.Began:
                StartTouch(position);
                break;
            case TouchPhase.Moved:
                MoveTouch(position);
                break;
            case TouchPhase.Ended:
                EndTouch(position);
                break;
        }
    }
    private void HandleHorizontalSwipe(Vector2 endPosition)
    {
        float dragDistance = endPosition.x - touchStartPosition.x;

        if (Mathf.Abs(dragDistance) > swipeThreshold)
        {
            if (dragDistance < 0 && currentPanelIndex < panels.Count - 1)
            {
                currentPanelIndex++;
            }
            else if (dragDistance > 0 && currentPanelIndex > 0)
            {
                currentPanelIndex--;
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
       // classesDataElementsUIList.Clear();
       // classesDataElementsUIList.TrimExcess();

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
