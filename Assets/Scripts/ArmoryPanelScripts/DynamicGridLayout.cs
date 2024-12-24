using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayout : MonoBehaviour
{
    public float spacing = 10f; // Odstêp miêdzy elementami
    public float rowHeight = 100f; // Wysokoœæ wiersza (mo¿e byæ dynamiczna)
    public float padding = 10f; // Padding wokó³ uk³adu (opcjonalnie)
    [SerializeField]
    private RectTransform parentRect;
    public float rowMaxWIdth = 900f;

    [ContextMenu("Update Layout")]
    public void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);


        float currentX = padding;
        float currentY = -padding / 2;
        float maxRowHeight = 0f;

        //RectTransform parentRect = GetComponent<RectTransform>();
        Debug.Log(parentRect.sizeDelta.x);
        float parentWidth = parentRect.rect.width;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // Pomijamy nieaktywne elementy
            if (!child.gameObject.activeSelf)
                continue;

            // Pobieramy RectTransform dziecka
            RectTransform childRect = child.GetComponent<RectTransform>();
            if (childRect == null) continue;

            // Pobieramy szerokoœæ i wysokoœæ dziecka
            float childWidth = childRect.sizeDelta.x;
            float childHeight = childRect.sizeDelta.y;

            // Sprawdzamy, czy element mieœci siê w aktualnym wierszu
            if (currentX + childWidth + padding > parentWidth)
            {
                // Przesuniêcie do nowego wiersza
                currentX = padding;
                currentY -= maxRowHeight + spacing/2;
                maxRowHeight = 0f;
            }

            // Ustawiamy pozycjê elementu (z uwzglêdnieniem œrodka)
            float childPositionX = currentX + childWidth / 2;
            float childPositionY = currentY - childHeight / 2;
            childRect.anchoredPosition = new Vector2(childPositionX, childPositionY);

            // Aktualizujemy pozycjê w wierszu
            currentX += childWidth + spacing;

            // Aktualizujemy maksymaln¹ wysokoœæ wiersza
            maxRowHeight = Mathf.Max(maxRowHeight, childHeight);
        }

        // Opcjonalne: Mo¿esz zaktualizowaæ wysokoœæ kontenera, jeœli ma byæ elastyczny
        float totalHeight = Mathf.Abs(currentY) + maxRowHeight + padding/2;
        parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, totalHeight);

    }
    [ContextMenu("Update Justiced Layout")]
    public void UpdateJustifiedLayout()
    {
        float currentX = padding;
        float currentY = -padding / 2;
        float maxRowHeight = 0f;

        RectTransform parentRect = GetComponent<RectTransform>();
        float parentWidth = parentRect.rect.width;

        // Tymczasowa lista elementów w bie¿¹cym wierszu
        List<RectTransform> currentRow = new List<RectTransform>();
        float currentRowWidth = 0f;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // Pomijamy nieaktywne elementy
            if (!child.gameObject.activeSelf)
                continue;

            // Pobieramy RectTransform dziecka
            RectTransform childRect = child.GetComponent<RectTransform>();
            if (childRect == null) continue;

            // Pobieramy szerokoœæ i wysokoœæ dziecka
            float childWidth = childRect.sizeDelta.x;
            float childHeight = childRect.sizeDelta.y;

            // Sprawdzamy, czy element zmieœci siê w bie¿¹cym wierszu
            if (currentX + childWidth + padding > parentWidth && currentRow.Count > 0)
            {
                // Justowanie bie¿¹cego wiersza
                JustifyRow(currentRow, currentRowWidth, parentWidth, currentY);

                // Resetujemy parametry dla nowego wiersza
                currentX = padding;
                currentY -= maxRowHeight + spacing;
                maxRowHeight = 0f;
                currentRow.Clear();
                currentRowWidth = 0f;
            }

            // Dodajemy element do bie¿¹cego wiersza
            currentRow.Add(childRect);
            currentRowWidth += childWidth;

            // Aktualizujemy pozycjê w wierszu
            currentX += childWidth + spacing;

            // Aktualizujemy maksymaln¹ wysokoœæ wiersza
            maxRowHeight = Mathf.Max(maxRowHeight, childHeight);
        }

        // Justowanie ostatniego wiersza (jeœli istnieje)
        if (currentRow.Count > 0)
        {
            JustifyRow(currentRow, currentRowWidth, parentWidth, currentY);
        }
    }

    private void JustifyRow(List<RectTransform> row, float rowWidth, float parentWidth, float currentY)
    {
        // Obliczamy odstêp miêdzy elementami
        float totalSpacing = parentWidth - padding * 2 - rowWidth;
        float dynamicSpacing = row.Count > 1 ? totalSpacing / (row.Count - 1) : 0f;

        float currentX = padding;

        foreach (RectTransform childRect in row)
        {
            float childWidth = childRect.sizeDelta.x;
            float childHeight = childRect.sizeDelta.y;

            // Ustawiamy pozycjê elementu (z uwzglêdnieniem œrodka)
            float childPositionX = currentX + childWidth / 2;
            float childPositionY = currentY - childHeight / 2;
            childRect.anchoredPosition = new Vector2(childPositionX, childPositionY);

            // Przesuwamy pozycjê w wierszu
            currentX += childWidth + dynamicSpacing;
        }
    }
}
