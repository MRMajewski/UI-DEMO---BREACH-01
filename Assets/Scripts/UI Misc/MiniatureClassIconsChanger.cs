using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureClassIconsChanger : MiniatureIconsChanger
{
    //[SerializeField]
    //private Button classIconExample;

    //[SerializeField]
    //private Transform iconsParent;

    public override void CreateIcons(IDataBase dataBase)
    {
        
    }
    public void CreateIcons(ClassesDataBase classData)
    {
        foreach (var item in iconList)
        {
            DestroyImmediate(item.gameObject);
        }

        iconList.Clear();
        iconList.TrimExcess();

        foreach (ISnapperPanelData data in classData.AllClasses)
        {
            iconExample.gameObject.SetActive(true);
            Button newIcon = Instantiate(iconExample, iconsParent).GetComponent<Button>();
            Image iconImage = newIcon.GetComponent<Image>();
            iconImage.sprite = data.GetMiniatureIcon();

            int index = iconList.Count;
            newIcon.name = "Icon " + index;
            iconList.Add(iconImage);
        }
        iconExample.gameObject.SetActive(false);
    }

}
