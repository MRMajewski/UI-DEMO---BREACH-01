
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureIconsChanger : MonoBehaviour
{
    [SerializeField]
    protected List<Image> iconList;

    public List<Image> IconList { get => iconList; }

    [SerializeField]
    protected UIElementsSnapper snapper;

    [SerializeField]
    protected Button iconExample;

    [SerializeField]
    protected Transform iconsParent;

    public virtual void CreateIcons(IDataBase dataBase)
    {
    }

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

