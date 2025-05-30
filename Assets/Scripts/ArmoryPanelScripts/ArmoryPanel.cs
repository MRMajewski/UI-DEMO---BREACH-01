using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmoryPanel : SimpleUIPanelMobiles
{
    public Transform itemListContainer;
    public ItemElementUI itemUIPrefab;
    public ItemDatabase itemDatabase;

    private List<ItemCategory> selectedCategories = new List<ItemCategory>();
    private string currentSearchText = "";

    [SerializeField]
    private FilterUI filterUI;

    [SerializeField]
    private List<ItemElementUI> currentItems;

    [SerializeField]
    private RectTransform filterUIRectTransform;
    [SerializeField]
    private RectTransform detailsRectTransform;
    [SerializeField]
    private RectTransform armoryContentRectTransform;
    [SerializeField]
    private RectTransform armoryViewportRectTransform;

    [SerializeField]
    private TMP_InputField searchInputField;

    public override void InitializePanel()
    {
        filterUI.CreateCategoryButtons();

        RefreshUI(itemDatabase.AllItems);

        currentItems[0].button.onClick.Invoke();
    }
    public override void EnablePanel()
    {
        this.gameObject.SetActive(true);

        panelsCanvasGroup.DOFade(1, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine);

        StartCoroutine(UpdateFiltersLayoutCoroutine());

        ResetSearchInput();

        filterUI.ClearSelection();
    }

    public IEnumerator UpdateFiltersLayoutCoroutine()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
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
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
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

    public void OnCategorySelected(List<ItemCategory> newSelectedCategories)
    {
        selectedCategories = newSelectedCategories;
        FilterAndRefreshUI();
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
        itemUIPrefab.gameObject.SetActive(true);

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
        itemUIPrefab.gameObject.SetActive(false);


        if(currentItems.Count > 0)
            currentItems[0].button.onClick.Invoke();

        LayoutRebuilder.ForceRebuildLayoutImmediate(filterUIRectTransform.GetComponent<RectTransform>());

        AdjustScrollRect();
    }

    private void CreateItemUI(ItemData itemData)
    {
        ItemElementUI newItemUI = Instantiate(itemUIPrefab, itemListContainer);
        newItemUI.SetUp(itemData);
        currentItems.Add(newItemUI);
    }

    private void AdjustScrollRect()
    {
        float contentHeight = filterUIRectTransform.rect.height + detailsRectTransform.rect.height;
        float parentHeight = armoryContentRectTransform.rect.height;
        float test = parentHeight - contentHeight - 20f;
        float newViewportHeight = Mathf.Clamp(test, 0, parentHeight);

        Vector2 newSize = armoryViewportRectTransform.sizeDelta;
        newSize.y = newViewportHeight;
        armoryViewportRectTransform.sizeDelta = newSize;
    }
}
