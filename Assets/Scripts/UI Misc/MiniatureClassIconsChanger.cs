using System.Collections;
using System.Collections.Generic;
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

    public void CreateClassIcons(ClassesDataBase classdata, UIElementsSnapper snapper)
    {
        //foreach (var item in classIconList)
        //{
        //    DestroyImmediate(item.gameObject);
        //}

        iconList.Clear();
        iconList.TrimExcess();

        foreach (ISnapperPanelElement data in classdata.AllClasses)
        {
            Button newIcon = Instantiate(classIconExample, iconsParent).GetComponent<Button>();
            Image iconImage = newIcon.GetComponent<Image>();
            iconImage.sprite = data.GetMiniatureIcon();

            int index = iconList.Count; // Zapisujemy bie¿¹cy indeks
            newIcon.onClick.AddListener(() => snapper.SnapToPanelFromButton(index));
            newIcon.onClick.AddListener(() => SetAlphaForIndex(index));

            iconList.Add(iconImage);
        }
        classIconExample.gameObject.SetActive(false);
    }
}
