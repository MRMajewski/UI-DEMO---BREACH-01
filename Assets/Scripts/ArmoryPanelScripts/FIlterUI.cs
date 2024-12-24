using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilterUI : MonoBehaviour
{
    [Header("Filter Buttons")]
    public Transform filterButtonContainer; // Kontener na przyciski kategorii
    public GameObject filterButtonPrefab;  // Prefab przycisku kategorii

    private HashSet<ItemCategory> selectedCategories = new HashSet<ItemCategory>();
    [SerializeField]
    private List<ItemCategory> allCategories = new List<ItemCategory>();

    [SerializeField]
    private List<Button> allCategoriesButtons = new List<Button>();

    [SerializeField]
    private ArmoryPanel itemPanel;

    [SerializeField]
    private Color selectedCategoryColor;

    [SerializeField]
    private Color defaultCategoryColor;

    [SerializeField]
    private DynamicGridLayout dynamicGridLayout;


    public void CreateCategoryButtons()
    {
        allCategories.Clear();
        allCategories.TrimExcess();

        filterButtonPrefab.SetActive(true);
        allCategories.AddRange((ItemCategory[])System.Enum.GetValues(typeof(ItemCategory)));

        foreach (Button child in allCategoriesButtons)
        {
            Destroy(child.gameObject);
        }
        allCategoriesButtons.Clear();
        allCategoriesButtons.TrimExcess();


        foreach (var category in allCategories)
        {
            GameObject newButton = Instantiate(filterButtonPrefab, filterButtonContainer);
            Button button = newButton.GetComponent<Button>();
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

            if (button == null || buttonText == null)
            {
                Debug.LogError("FilterButtonPrefab must have Button and Text components.");
                continue;
            }
            buttonText.text = category.ToString();
            newButton.name = newButton.name + " " + buttonText.text;
            allCategoriesButtons.Add(button);
            button.onClick.AddListener(() => ToggleCategory(category, button));
        }
        filterButtonPrefab.SetActive(false);


      //  LayoutRebuilder.ForceRebuildLayoutImmediate(filterButtonContainer.GetComponent<RectTransform>());
      //  dynamicGridLayout.UpdateLayout();
    }

    public void UpdateFiltersLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(filterButtonContainer.GetComponent<RectTransform>());
        dynamicGridLayout.UpdateLayout();
        LayoutRebuilder.ForceRebuildLayoutImmediate(filterButtonContainer.GetComponent<RectTransform>());
    }

    private void ToggleCategory(ItemCategory category, Button button)
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

        itemPanel.OnCategorySelected(new List<ItemCategory>(selectedCategories));
    }

    private void SetButtonState(Button button, bool isSelected)
    {
        if (isSelected)
        {
            button.targetGraphic.color = selectedCategoryColor; 
        }
        else
        {
            button.targetGraphic.color = defaultCategoryColor; 
        }
    }

    public void ClearSelection()
    {
        selectedCategories.Clear();

        foreach (Transform child in filterButtonContainer)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                SetButtonState(button, false);
            }
        }
        itemPanel.OnCategorySelected(new List<ItemCategory>());
    }
}
