using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureClassIconsChanger : MiniatureIconsChanger
{
    [SerializeField]
    private Button classIconExample;

    [SerializeField]
    private Transform iconsParent;

    [SerializeField]
    private UIElementsSnapper snapper;

    public void CreateClassIcons(ClassesDataBase classdata, UIElementsSnapper snapper)
    {
        foreach (var item in iconList)
        {
            DestroyImmediate(item.gameObject);
        }

        iconList.Clear();
        iconList.TrimExcess();

        foreach (ISnapperPanelElement data in classdata.AllClasses)
        {
            classIconExample.gameObject.SetActive(true);
            Button newIcon = Instantiate(classIconExample, iconsParent).GetComponent<Button>();
            Image iconImage = newIcon.GetComponent<Image>();
            iconImage.sprite = data.GetMiniatureIcon();
          
            int index = iconList.Count;

            newIcon.name = newIcon.name + " " + index;
            iconList.Add(iconImage);
        }
        classIconExample.gameObject.SetActive(false);
    }

    public void AddOnClickMethodsToIcons()
    {
        for (int i = 0; i < iconList.Count; i++)
        {
            int index = i;
            Button button = iconList[i].GetComponent<Button>();

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                MethodsOnClick(index);

            });
        }

        void MethodsOnClick(int index)
        {
            snapper.SnapToPanelFromButton(index);
            SetAlphaForIndex(index);
        }
    }


    private void Start()
    { 
       AddOnClickMethodsToIcons();
    }
}
