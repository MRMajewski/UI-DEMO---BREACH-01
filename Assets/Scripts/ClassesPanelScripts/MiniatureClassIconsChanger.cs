using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureClassIconsChanger : MonoBehaviour
{
    [SerializeField]
    private Button classIconExample;

    [SerializeField]
    private Transform iconsParent;

    [SerializeField]
    private List<Image> classIconList;

    //public void CreateClassIcons(ClassesDataBase classdata)
    //{
    //    classIconList.Clear();
    //    classIconList.TrimExcess();

    //    foreach (ClassData data in classdata.AllClasses)
    //    {
    //        Button newIcon = Instantiate(classIconExample, iconsParent);
    //        newIcon.GetComponent<Image>().sprite = data.classIcon;
    //        classIconList.Add(newIcon.GetComponent<Image>());
    //    }
    //    classIconExample.gameObject.SetActive(false);
    //}

    public void CreateClassIcons(ClassesDataBase classdata, ClassesElementsSnapper snapper)
    {
        classIconList.Clear();
        classIconList.TrimExcess();

        foreach (ClassData data in classdata.AllClasses)
        {
            Button newIcon = Instantiate(classIconExample, iconsParent).GetComponent<Button>();
            Image iconImage = newIcon.GetComponent<Image>();
            iconImage.sprite = data.classIcon;

            int index = classIconList.Count; // Zapisujemy bie¿¹cy indeks
            newIcon.onClick.AddListener(() => snapper.SnapToPanelFromButton(index));
          //  newIcon.onClick.AddListener(() => SetAlphaForIndex(index));

            classIconList.Add(iconImage);
        }
        classIconExample.gameObject.SetActive(false);
    }

    public void SetAlphaForIndex(int index)
    {
        if (index < 0 || index >= classIconList.Count)
        {
            Debug.LogWarning("Index out of bounds");
            return;
        }

        for (int i = 0; i < classIconList.Count; i++)
        {
            Color color = classIconList[i].color;
            color.a = (i == index) ? 1f : 0.5f;
            classIconList[i].color = color;
        }
    }
}
