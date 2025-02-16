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
        //foreach (ClassDataElementUI classData in classesDataElementsUIList)
        //{
        //    DestroyImmediate(classData.gameObject);
        //}
       classesDataElementsUIList.Clear();
       classesDataElementsUIList.TrimExcess();
        tooltipTriggerAdders.Clear();
        tooltipTriggerAdders.TrimExcess();

        foreach (var classData in ClassesDataBase.AllClasses)
        {
            ClassDataElementUI newElement = Instantiate(classDataElementPrefab, container);

            newElement.Initialize(classData);
            classesDataElementsUIList.Add(newElement);

            tooltipTriggerAdders.AddRange(newElement.TriggerGameObjectAdder);

        }
        classDataElementPrefab.gameObject.SetActive(false);

        snapper.InitPanels(classesDataElementsUIList);
        classIconChanger.CreateClassIcons(ClassesDataBase, snapper);
    }

    [ContextMenu("Init")]
    public override void InitializePanelData()
    {
       // InitializePanel();
        base.InitializePanelData();
    }


    public override void InitializePanel()
    {
        
    }

    public void InitPanel()
    {
       // ResizeClassDataElement();
        InitializeClasses();
        snapper.OnPanelChanged += classIconChanger.SetAlphaForIndex;
        classIconChanger.SetAlphaForIndex(0);
    }

    //public void ResizeClassDataElement()
    //{
    //    Debug.Log(viewportRect.rect.width);
    //    classDataElementPrefab.GetComponent<RectTransform>().sizeDelta =
    //        new Vector2(viewportRect.rect.width, classDataElementPrefab.GetComponent<RectTransform>().sizeDelta.y);


    //}

    //public override void DisablePanel()
    //{
    //    this.gameObject.SetActive(false);
    //}
    public void OnDestroy()
    {
        snapper.OnPanelChanged -= classIconChanger.SetAlphaForIndex;
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
        Debug.Log(" ResizeSnappedPanel()");
        ResizeSnappedPanel();
    }
}
