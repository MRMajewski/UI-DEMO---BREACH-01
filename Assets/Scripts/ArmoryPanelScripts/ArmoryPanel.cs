using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmoryPanel : SimpleUIPanelMobiles, IFilterablePanel<ItemCategory>
{
    public Transform itemListContainer;
    public ItemElementUI itemUIPrefab;
    public ItemDatabase itemDatabase;

    private HashSet<ItemCategory> selectedCategories = new HashSet<ItemCategory>();
    private string currentSearchText = "";

    [SerializeField]
    private FilterUIItems filterUI;
    [SerializeField]
    private List<ItemElementUI> currentItems;
    [SerializeField]
    private RectTransform armoryRectTransform;
    [SerializeField]
    private RectTransform filterUIRectTransform;
    [SerializeField]
    private RectTransform detailsRectTransform;
    [SerializeField]
    private RectTransform  searchRectTransform;
    [SerializeField]
    private RectTransform armoryContentRectTransform;
    [SerializeField]
    private RectTransform armoryViewportRectTransform;

    [SerializeField]
    private ItemDetailsPanel itemDetailsPanel;
    [SerializeField]
    private TMP_InputField searchInputField;

    [SerializeField] 
    private float scrollPadding = 20f;

    public override void InitializePanel()
    {
        filterUI.CreateCategoryButtons();

        RefreshUI(itemDatabase.AllItems);

        AutoSelectFirstItem();
    }
    private void AutoSelectFirstItem()
    {
        currentItems.FirstOrDefault()?.Button.onClick.Invoke();
    }
    public override void DisablePanel()
    {
        base.DisablePanel();
        filterUI.OnFilterChanged -= FilterUpdated;
    }
    public override void EnablePanel()
    {
        filterUI.OnFilterChanged += FilterUpdated;
        this.gameObject.SetActive(true);

        panelsCanvasGroup.DOFade(1, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine);

        StartCoroutine(UpdateFiltersLayoutCoroutine());

        ResetSearchInput();

        filterUI.ClearSelection();
    }

    public IEnumerator UpdateFiltersLayoutCoroutine()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(armoryRectTransform);
        filterUI.UpdateFiltersLayout();
        AdjustScrollRect();
    }

    public void UpdateContentLayout()
    {
        StartCoroutine(UpdateContentLayoutCoroutine());
    }
    public IEnumerator UpdateContentLayoutCoroutine()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(armoryRectTransform);
        AdjustScrollRect();
    }
    public void OnSearchInputChanged(string searchText)
    {
        currentSearchText = searchText;
        FilterAndRefreshUI();
    }

    public void ResetSearchInput()
    {
        currentSearchText =string.Empty;
        searchInputField.text = currentSearchText;
        OnSearchInputChanged(currentSearchText);
    }

    private void FilterAndRefreshUI()
    {
        var filteredItems = itemDatabase.AllItems.Where(item =>
            (string.IsNullOrEmpty(currentSearchText) ||
             item.itemName.ToLower().Contains(currentSearchText.ToLower())) &&
            (selectedCategories.Count == 0 ||
             item.categories.Any(category => selectedCategories.Contains(category)))
        ).ToList();

        RefreshUI(filteredItems);
    }

    private void RefreshUI(List<ItemData> items)
    {
        foreach (ItemElementUI itemElement in currentItems)
        {
            Destroy(itemElement.gameObject);
        }
        currentItems.Clear();
        currentItems.TrimExcess();

        foreach (var item in items)
        {
            CreateItemUI(item);
        }
        AutoSelectFirstItem();
        LayoutRebuilder.ForceRebuildLayoutImmediate(filterUIRectTransform);
        AdjustScrollRect();
    }

    private void CreateItemUI(ItemData itemData)
    {    
        ItemElementUI newItemUI = Instantiate(itemUIPrefab, itemListContainer);
        newItemUI.gameObject.SetActive(true);
        newItemUI.SetUp(itemData, OnItemClicked);
        currentItems.Add(newItemUI);

        void OnItemClicked(ItemData data)
        {
            itemDetailsPanel.ShowDetails(data, InitializePanelData);
        }
    }
    private void AdjustScrollRect()
    {
        float contentHeight = filterUIRectTransform.rect.height + detailsRectTransform.rect.height+searchRectTransform.rect.height;
        float parentHeight = armoryContentRectTransform.rect.height;
        float test = parentHeight - contentHeight - scrollPadding;
        float newViewportHeight = Mathf.Clamp(test, 0, parentHeight);

        Vector2 newSize = armoryViewportRectTransform.sizeDelta;
        newSize.y = newViewportHeight;
        armoryViewportRectTransform.sizeDelta = newSize;
    }

    public void FilterUpdated(HashSet<ItemCategory> selectedFilters)
    {
        selectedCategories = selectedFilters;
        FilterAndRefreshUI();
    }
}

