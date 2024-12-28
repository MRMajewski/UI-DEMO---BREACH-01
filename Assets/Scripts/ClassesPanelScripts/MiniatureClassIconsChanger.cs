using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureClassIconsChanger : MonoBehaviour
{
    [SerializeField]
    private Image classIconExample;

    [SerializeField]
    private Transform iconsParent;

    [SerializeField]
    private List<Image> classIconList;

    public void CreateClassIcons(ClassesDataBase classdata)
    {
        classIconList.Clear();
        classIconList.TrimExcess();

        foreach (ClassData data in classdata.AllClasses)
        {
            Image newIcon = Instantiate(classIconExample, iconsParent);
            newIcon.sprite = data.classIcon;
            classIconList.Add(newIcon);
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
