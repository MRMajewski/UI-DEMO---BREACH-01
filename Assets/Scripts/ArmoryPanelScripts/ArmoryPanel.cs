using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArmoryPanel : SimpleUIPanelMobiles
{
    public Transform itemListContainer; // Kontener dla elementów UI
 //   public GameObject itemUIPrefab;     // Prefab dla
    public ItemElementUI itemUIPrefab;     // Prefab dla przedmiotów
    public ItemDatabase itemDatabase;  // Referencja do bazy danych przedmiotów

    private List<ItemCategory> selectedCategories = new List<ItemCategory>(); // Wybrane kategorie
    private string currentSearchText = ""; // Tekst wyszukiwania

    [SerializeField]
    private FilterUI filterUI;

    [SerializeField]
    private List<ItemElementUI> currentItems;


    [SerializeField]
    private RectTransform filterUIRectTransform;
    [SerializeField]
    private RectTransform armoryContentRectTransform;
    [SerializeField]
    private RectTransform armoryViewportRectTransform;
  
    public override void InitializePanel()
    {
        if (itemDatabase == null)
        {
            Debug.LogError("ItemDatabase is not assigned!");
            return;
        }

        // Wczytanie przedmiotów z bazy danych
        RefreshUI(itemDatabase.AllItems);

        filterUI.CreateCategoryButtons();

        FilterAndRefreshUI();

        currentItems[0].button.onClick.Invoke();
    }
    public override void DisablePanel()
    {
        this.gameObject.SetActive(false);
    }

    public override void EnablePanel()
    {
        this.gameObject.SetActive(true);

        //filterUI.CreateCategoryButtons();
        //InitArmoryDatabase();
        panelsCanvasGroup.DOFade(1, SimpleUIPanelMobilesManager.Instance.TransitionTime).SetEase(Ease.InOutSine);

     //   filterUI.UpdateFiltersLayout();
        StartCoroutine(UpdateFiltersLayoutCoroutine());
    }

    public IEnumerator UpdateFiltersLayoutCoroutine()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
        filterUI.UpdateFiltersLayout();
    }


    // Wywo³ywane, gdy zmienia siê pole wyszukiwania
    public void OnSearchInputChanged(string searchText)
    {
        currentSearchText = searchText;
        FilterAndRefreshUI();
    }

    // Wywo³ywane, gdy zmieniaj¹ siê wybrane kategorie
    public void OnCategorySelected(List<ItemCategory> newSelectedCategories)
    {
        selectedCategories = newSelectedCategories;
        FilterAndRefreshUI();
    }

    // Filtruje i odœwie¿a UI
    private void FilterAndRefreshUI()
    {
      //  selectedCategories.Clear();
      //  selectedCategories.TrimExcess();

        var filteredItems = itemDatabase.AllItems.Where(item =>
            // Filtruj na podstawie wyszukiwania
            (string.IsNullOrEmpty(currentSearchText) ||
             item.itemName.ToLower().Contains(currentSearchText.ToLower())) &&
            // Filtruj na podstawie kategorii
            (selectedCategories.Count == 0 ||
             item.categories.Any(category => selectedCategories.Contains(category)))
        ).ToList();

        RefreshUI(filteredItems);
    }

    // Aktualizuje UI na podstawie listy przedmiotów
    private void RefreshUI(List<ItemData> items)
    {
        itemUIPrefab.gameObject.SetActive(true);
        // Czyœci obecne elementy UI
        foreach (ItemElementUI itemElement in currentItems)
        {
            Destroy(itemElement.gameObject);
        }
        currentItems.Clear();
        currentItems.TrimExcess();

        // Tworzy nowe elementy UI
        foreach (var item in items)
        {
            CreateItemUI(item);
        }
        itemUIPrefab.gameObject.SetActive(false);


        LayoutRebuilder.ForceRebuildLayoutImmediate(filterUIRectTransform.GetComponent<RectTransform>());

  
        AdjustScrollRect();
    }

    // Tworzy UI dla pojedynczego przedmiotu
    private void CreateItemUI(ItemData itemData)
    {
        ItemElementUI newItemUI = Instantiate(itemUIPrefab, itemListContainer);
        newItemUI.SetUp(itemData);
        currentItems.Add(newItemUI);
    }

    private void AdjustScrollRect()
    {

        float contentHeight = filterUIRectTransform.rect.height;
        float parentHeight = armoryContentRectTransform.rect.height;
        float newViewportHeight = Mathf.Clamp(parentHeight - contentHeight - 10f, 0, parentHeight);

        Vector2 newSize = armoryViewportRectTransform.sizeDelta;
        newSize.y = newViewportHeight;
        armoryViewportRectTransform.sizeDelta = newSize;
    }
}
