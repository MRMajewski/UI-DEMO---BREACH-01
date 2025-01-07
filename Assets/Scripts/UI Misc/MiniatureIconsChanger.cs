using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureIconsChanger : MonoBehaviour
{


    //[SerializeField]
    //protected Transform iconsParent;

    [SerializeField]
    protected List<Image> classIconList;
 

    public virtual void SetAlphaForIndex(int index)
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
