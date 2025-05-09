using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClassPanel : SimpleUIPanelMobiles
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


    public override void InitializePanel()
    {
        CastSnappedElements();
        base.InitializePanel();

        void CastSnappedElements()
        {
            List<ISnappedElement> snappedElements = classesDataElementsUIList.Cast<ISnappedElement>().ToList();

            snapper.InitPanels(snappedElements);
        }
    }
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

        classIconChanger.CreateIcons(ClassesDataBase);
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
    private void OnEnable()
    {
        ResizeSnappedPanel();
    }

    private void Start()
    {   
        classIconChanger.SetAlphaForIndex(0);
    }

    public override void EnablePanel()
    {
        base.EnablePanel();
        snapper.OnPanelChanged += classIconChanger.SetAlphaForIndex;
    }
    public override void DisablePanel()
    {
        base.DisablePanel(); 
        snapper.ResetAllScrolls();
        snapper.OnPanelChanged -= classIconChanger.SetAlphaForIndex;
    }
}
