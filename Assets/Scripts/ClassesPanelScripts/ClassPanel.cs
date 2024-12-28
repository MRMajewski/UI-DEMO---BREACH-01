using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassPanel : SimpleUIPanelMobiles
{
    [SerializeField]
    private RectTransform container; // Kontener na elementy UI

    [SerializeField]
    private ClassDataElementUI classDataElementPrefab; // Prefab klasy UI

    [SerializeField]
    private List<ClassDataElementUI> classesDataElementsUIList; // Lista ScriptableObjectów

    [SerializeField]
    private ClassesDataBase ClassesDataBase;

    [SerializeField]
    private ClassesElementsSnapper snapper;

    [SerializeField]
    private MiniatureClassIconsChanger classIconChanger;

    /// <summary>
    /// Tworzy i inicjalizuje obiekty typu ClassDataElementUI na podstawie danych ClassesData.
    /// </summary>
    public void InitializeClasses()
    {
        // Upewnij siê, ¿e kontener jest pusty
        //foreach (Transform child in container)
        //{
        //    Destroy(child.gameObject);
        //}

        foreach (ClassDataElementUI classData in classesDataElementsUIList)
        {
            Destroy(classData.gameObject);
        }
        classesDataElementsUIList.Clear();
        classesDataElementsUIList.TrimExcess();


            // Iteracja przez listê ClassesData i tworzenie elementów UI
            foreach (var classData in ClassesDataBase.AllClasses)
        {
            // Utwórz nowy element na podstawie prefabrykatu
            ClassDataElementUI newElement = Instantiate(classDataElementPrefab, container);

            // Ustaw dane w nowym elemencie
            newElement.Initialize(classData);
            classesDataElementsUIList.Add(newElement);
        }

        classDataElementPrefab.gameObject.SetActive(false);

        snapper.InitPanels(classesDataElementsUIList);
        classIconChanger.CreateClassIcons(ClassesDataBase);

      //  snapper.OnPanelChanged += classIconChanger.SetAlphaForIndex;
    }


    public override void InitializePanel()
    {
        InitializeClasses();
        snapper.OnPanelChanged += classIconChanger.SetAlphaForIndex;
    }

    public override void DisablePanel()
    {
        this.gameObject.SetActive(false);
     //   snapper.OnPanelChanged -= classIconChanger.SetAlphaForIndex;
    }
    public void OnDestroy()
    {
        snapper.OnPanelChanged -= classIconChanger.SetAlphaForIndex;
    }

}
