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

    //[SerializeField]
    //private List<Image> classIconList;

    [SerializeField]
    private UIElementsSnapper snapper;

    public void CreateClassIcons(ClassesDataBase classdata, UIElementsSnapper snapper)
    {
      //  this.snapper = snapper;
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
          
            int index = iconList.Count; // Zapisujemy bie��cy indeks

            newIcon.name = newIcon.name + " " + index;

            //newIcon.onClick.AddListener(() => snapper.SnapToPanelFromButton(index));
            //newIcon.onClick.AddListener(() => SetAlphaForIndex(index));

            //Debug.Log($"Dodaj� onClick dla {newIcon.name} z indexem {index}");
            //newIcon.onClick.RemoveAllListeners();

            //newIcon.onClick.AddListener(() =>
            //{
            //    Debug.Log($"Klikni�to ikon�: {index}");
            ////    snapper.SnapToPanelFromButton(index);
            //});

            //newIcon.onClick.AddListener(() =>
            //{
            //    Debug.Log($"Ustawiono alpha dla: {index}");
            //  //  SetAlphaForIndex(index);
            //});
            //Debug.Log($"Dodano ikon�: {newIcon.name} do listy");
            iconList.Add(iconImage);

        }
        Debug.Log("Koniec CreateClassIcons");
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
    }

    public void MethodsOnClick(int index)
    {
        snapper.SnapToPanelFromButton(index);
        SetAlphaForIndex(index);
    }


    private void Start()
    { 
        
        Debug.Log($"Odpala si� mianowicie START w ICON CHANGERZE");
       AddOnClickMethodsToIcons();
    }
}
