using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayout : MonoBehaviour
{
    public float spacing = 10f; 
    public float rowHeight = 50f; 
    public float padding = 10f;
    [SerializeField]
    private RectTransform parentRect;

    [SerializeField]
    private bool isJustified = false;

    [ContextMenu("Update Layout")]

    public void UpdateLayout()
    {
        if (isJustified)
        {
            UpdateJustifiedLayout();
        }
        else
        {
            UpdateNotJustifiedLayout();
        }
    }

    public void UpdateNotJustifiedLayout()
    {

        float currentX = padding;
        float currentY = -padding / 2;
        float maxRowHeight = 0f;

        Debug.Log(parentRect.sizeDelta.x);
        float parentWidth = parentRect.rect.width;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (!child.gameObject.activeSelf)
                continue;

            if (!child.TryGetComponent<RectTransform>(out var childRect)) 
                continue;

            float childWidth = childRect.sizeDelta.x;
            float childHeight = childRect.sizeDelta.y;

            if (currentX + childWidth + padding > parentWidth)
            {
                currentX = padding;
                currentY -= maxRowHeight + spacing/2;
                maxRowHeight = 0f;
            }

            float childPositionX = currentX + childWidth / 2;
            float childPositionY = currentY - childHeight / 2;
            childRect.anchoredPosition = new Vector2(childPositionX, childPositionY);

            currentX += childWidth + spacing;

            maxRowHeight = Mathf.Max(maxRowHeight, childHeight);
        }

        float totalHeight = Mathf.Abs(currentY) + maxRowHeight + padding/2;
        parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, totalHeight);
    }


    [ContextMenu("Update Justified Layout")]
    public void UpdateJustifiedLayout()
    {
        float currentX = padding;
        float currentY = -padding / 2;
        float maxRowHeight = 0f;

        float parentWidth = parentRect.rect.width;

        List<RectTransform> currentRow = new List<RectTransform>();
        float currentRowWidth = 0f;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (!child.gameObject.activeSelf)
                continue;

            if (!child.TryGetComponent<RectTransform>(out var childRect)) 
                continue;

            float childWidth = childRect.sizeDelta.x;
            float childHeight = childRect.sizeDelta.y;

            if (currentX + childWidth + padding > parentWidth && currentRow.Count > 0)
            {
                JustifyRow(currentRow, currentRowWidth, parentWidth, currentY, false);

                currentX = padding;
                currentY -= maxRowHeight + spacing;
                maxRowHeight = 0f;
                currentRow.Clear();
                currentRowWidth = 0f;
            }

            currentRow.Add(childRect);
            currentRowWidth += childWidth;

            currentX += childWidth + spacing;

            maxRowHeight = Mathf.Max(maxRowHeight, childHeight);
        }

        if (currentRow.Count > 0)
        {
            JustifyRow(currentRow, currentRowWidth, parentWidth, currentY, true);
        }

        float totalHeight = Mathf.Abs(currentY) + maxRowHeight + padding / 2;
        parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, totalHeight);
    }

    private void JustifyRow(List<RectTransform> row, float rowWidth, float parentWidth, float currentY, bool isLastRow)
    {
        float totalSpacing = parentWidth - padding * 2 - rowWidth;
        float dynamicSpacing = row.Count > 1 ? totalSpacing / (row.Count - 1) : 0f;

        if (isLastRow && dynamicSpacing > parentWidth / 4f)
        {
            dynamicSpacing = spacing; 
        }

        float currentX = padding;

        foreach (RectTransform childRect in row)
        {
            float childWidth = childRect.sizeDelta.x;
            float childHeight = childRect.sizeDelta.y;

            float childPositionX = currentX + childWidth / 2;
            float childPositionY = currentY - childHeight / 2;
            childRect.anchoredPosition = new Vector2(childPositionX, childPositionY);

            currentX += childWidth + dynamicSpacing;
        }
    }
}
