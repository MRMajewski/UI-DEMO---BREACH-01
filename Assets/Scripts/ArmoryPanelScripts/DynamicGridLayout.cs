using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayout : MonoBehaviour
{
    public float spacing = 10f; // Odst�p mi�dzy elementami
    public float rowHeight = 100f; // Wysoko�� wiersza (mo�e by� dynamiczna)
    public float padding = 10f; // Padding wok� uk�adu (opcjonalnie)
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

            // Pobieramy szeroko�� i wysoko�� dziecka
            float childWidth = childRect.sizeDelta.x;
            float childHeight = childRect.sizeDelta.y;

            // Sprawdzamy, czy element mie�ci si� w aktualnym wierszu
            if (currentX + childWidth + padding > parentWidth)
            {
                // Przesuni�cie do nowego wiersza
                currentX = padding;
                currentY -= maxRowHeight + spacing/2;
                maxRowHeight = 0f;
            }

            // Ustawiamy pozycj� elementu (z uwzgl�dnieniem �rodka)
            float childPositionX = currentX + childWidth / 2;
            float childPositionY = currentY - childHeight / 2;
            childRect.anchoredPosition = new Vector2(childPositionX, childPositionY);

            // Aktualizujemy pozycj� w wierszu
            currentX += childWidth + spacing;

            // Aktualizujemy maksymaln� wysoko�� wiersza
            maxRowHeight = Mathf.Max(maxRowHeight, childHeight);
        }

        // Opcjonalne: Mo�esz zaktualizowa� wysoko�� kontenera, je�li ma by� elastyczny
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

        // Tymczasowa lista element�w w bie��cym wierszu
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

            // Pobieramy szeroko�� i wysoko�� dziecka
            float childWidth = childRect.sizeDelta.x;
            float childHeight = childRect.sizeDelta.y;

            // Sprawdzamy, czy element zmie�ci si� w bie��cym wierszu
            if (currentX + childWidth + padding > parentWidth && currentRow.Count > 0)
            {
                // Justowanie bie��cego wiersza
                JustifyRow(currentRow, currentRowWidth, parentWidth, currentY);

                // Resetujemy parametry dla nowego wiersza
                currentX = padding;
                currentY -= maxRowHeight + spacing;
                maxRowHeight = 0f;
                currentRow.Clear();
                currentRowWidth = 0f;
            }

            // Dodajemy element do bie��cego wiersza
            currentRow.Add(childRect);
            currentRowWidth += childWidth;

            // Aktualizujemy pozycj� w wierszu
            currentX += childWidth + spacing;

            // Aktualizujemy maksymaln� wysoko�� wiersza
            maxRowHeight = Mathf.Max(maxRowHeight, childHeight);
        }

        // Justowanie ostatniego wiersza (je�li istnieje)
        if (currentRow.Count > 0)
        {
            JustifyRow(currentRow, currentRowWidth, parentWidth, currentY);
        }
    }

    private void JustifyRow(List<RectTransform> row, float rowWidth, float parentWidth, float currentY)
    {
        // Obliczamy odst�p mi�dzy elementami
        float totalSpacing = parentWidth - padding * 2 - rowWidth;
        float dynamicSpacing = row.Count > 1 ? totalSpacing / (row.Count - 1) : 0f;

        float currentX = padding;

        foreach (RectTransform childRect in row)
        {
            float childWidth = childRect.sizeDelta.x;
            float childHeight = childRect.sizeDelta.y;

            // Ustawiamy pozycj� elementu (z uwzgl�dnieniem �rodka)
            float childPositionX = currentX + childWidth / 2;
            float childPositionY = currentY - childHeight / 2;
            childRect.anchoredPosition = new Vector2(childPositionX, childPositionY);

            // Przesuwamy pozycj� w wierszu
            currentX += childWidth + dynamicSpacing;
        }
    }
}
