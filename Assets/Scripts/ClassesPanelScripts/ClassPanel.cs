using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassPanel : SimpleUIPanelMobiles, ISnapperPanel
{
    [SerializeField]
    private RectTransform container;
    [SerializeField]
    private RectTransform viewportRect;

    [SerializeField]
    private ClassDataElementUI classDataElementPrefab;

    [SerializeField]
    private List<ClassDataElementUI> classesDataElementsUIList;

    [SerializeField]
    private ClassesDataBase ClassesDataBase;

    [SerializeField]
    private UIElementsSnapper snapper;

    [SerializeField]
    private MiniatureClassIconsChanger classIconChanger;

    public void InitializeClasses()
    {
        foreach (ClassDataElementUI classData in classesDataElementsUIList)
        {
            if(classData!=null)
            DestroyImmediate(classData.gameObject);
        }
        classesDataElementsUIList.Clear();
        classesDataElementsUIList.TrimExcess();
        tooltipTriggerAdders.Clear();
        tooltipTriggerAdders.TrimExcess();

        classDataElementPrefab.gameObject.SetActive(true);

        foreach (var classData in ClassesDataBase.AllClasses)
        {
            ClassDataElementUI newElement = Instantiate(classDataElementPrefab, container);

            newElement.Initialize(classData);
            classesDataElementsUIList.Add(newElement);

            tooltipTriggerAdders.AddRange(newElement.TriggerGameObjectAdder);
        }
        classDataElementPrefab.gameObject.SetActive(false);

        snapper.InitPanels(classesDataElementsUIList);
        classIconChanger.CreateIcons(ClassesDataBase);

        snapper.OnPanelChanged += classIconChanger.SetAlphaForIndex;
    }

    public void ResizeSnappedPanel()
    {
        Debug.Log(viewportRect.rect.width);

        foreach (ClassDataElementUI classData in classesDataElementsUIList)
        {
            classData.GetComponent<RectTransform>().sizeDelta =
                new Vector2(viewportRect.rect.width, classDataElementPrefab.GetComponent<RectTransform>().sizeDelta.y);
        }
    }
    //private void OnEnable()
    //{
    //    ResizeSnappedPanel();

    //}

    private void Start()
    {   
        classIconChanger.SetAlphaForIndex(0);
    }

    public override void EnablePanel()
    {
        base.EnablePanel();
        snapper.OnPanelChanged += classIconChanger.SetAlphaForIndex;
        ResizeSnappedPanel();
    }
    public override void DisablePanel()
    {
        base.DisablePanel();
        snapper.OnPanelChanged -= classIconChanger.SetAlphaForIndex;
    }
}
