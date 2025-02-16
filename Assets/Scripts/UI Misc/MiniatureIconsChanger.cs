using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureIconsChanger : MonoBehaviour
{

    [SerializeField]
    protected List<Image> iconList;

    public virtual void SetAlphaForIndex(int index)
    {
        if (index < 0 || index >= iconList.Count)
        {
            Debug.LogWarning("Index out of bounds");
            return;
        }

        for (int i = 0; i < iconList.Count; i++)
        {
            Color color = iconList[i].color;
            color.a = (i == index) ? 1f : 0.5f;
            iconList[i].color = color;
        }
    }
}
