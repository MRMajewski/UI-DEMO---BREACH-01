using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilterUI<T> : MonoBehaviour where T : Enum
{
    [Header("Filter Buttons")]
    [SerializeField]
    private Transform filterButtonContainer;

    [SerializeField]
    private GameObject filterButtonPrefab;

    private HashSet<T> selectedCategories = new HashSet<T>();
    [SerializeField]
    private List<T> allCategories = new List<T>();

    [SerializeField]
    private List<Button> allCategoriesButtons = new List<Button>();

    [SerializeField]
    private Color selectedCategoryColor;

    [SerializeField]
    private Color defaultCategoryColor;

    [SerializeField]
    private DynamicGridLayout dynamicGridLayout;

    [SerializeField]
    private RectTransform filterContainerRectTransform;

    public event Action<HashSet<T>> OnFilterChanged;

    public void CreateCategoryButtons()
    {
        allCategories.Clear();
        allCategories.TrimExcess();

        filterButtonPrefab.SetActive(true);
        allCategories.AddRange((T[])Enum.GetValues(typeof(T)));

        foreach (Button child in allCategoriesButtons)
        {
            DestroyImmediate(child.gameObject);
        }
        allCategoriesButtons.Clear();
        allCategoriesButtons.TrimExcess();

        foreach (var category in allCategories)
        {
            GameObject newButton = Instantiate(filterButtonPrefab, filterButtonContainer);

            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            if (!newButton.TryGetComponent<Button>(out Button button) || buttonText == null)
            {
                Debug.LogError("FilterButtonPrefab must have Button and Text components.");
                continue;
            }

            buttonText.text = EnumManager.GetName(category);
            newButton.name = newButton.name + " " + buttonText.text;
            allCategoriesButtons.Add(button);

            T capturedCategory = category;
            button.onClick.AddListener(() => ToggleCategory(capturedCategory, button));
        }
        filterButtonPrefab.SetActive(false);
    }

    public void UpdateFiltersLayout()
    {
        UpdateFiltersContentElements();

        dynamicGridLayout.UpdateLayout();

        void UpdateFiltersContentElements()
        {
            foreach (Transform child in filterButtonContainer)
            {
                RectTransform childRect = child as RectTransform;
                if (childRect != null)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(childRect);
                }
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(filterContainerRectTransform);
        }
    }

    private void ToggleCategory(T category, Button button)
    {
        if (selectedCategories.Contains(category))
        {
            selectedCategories.Remove(category);
            SetButtonState(button, false);
        }
        else
        {
            selectedCategories.Add(category);
            SetButtonState(button, true);
        }
        OnFilterChanged?.Invoke(new HashSet<T>(selectedCategories));
    }

    private void SetButtonState(Button button, bool isSelected)
    {
        button.targetGraphic.color = isSelected ? selectedCategoryColor : defaultCategoryColor;
    }

    public void ClearSelection()
    {
        selectedCategories.Clear();

        foreach (Transform child in filterButtonContainer)
        {
            if (child.TryGetComponent<Button>(out Button button))
                SetButtonState(button, false);
        }
        OnFilterChanged?.Invoke(new HashSet<T>(selectedCategories));
    }
}
