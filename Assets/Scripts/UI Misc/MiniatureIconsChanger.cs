using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureIconsChanger : MonoBehaviour
{
    [SerializeField]
    protected List<Image> iconList;
    public List<Image> IconList { get => iconList; }

    [SerializeField]
    protected List<Color> iconColorsList;
    public List<Color> IconColorsList { get => iconColorsList; }

    [SerializeField]
    protected Color defaultColor;
    [SerializeField]
    protected UIElementsSnapper snapper;

    [SerializeField]
    protected Button iconExample;

    [SerializeField]
    protected Transform iconsParent;

    public void SetAlphaForIndex(int index)
    {
        if (index < 0 || index >= iconList.Count)
        {
            Debug.LogWarning("Index out of bounds");
            return;
        }

        for (int i = 0; i < iconList.Count; i++)
        {
            if (i == index)
            {
                Color color = iconColorsList[i];
                color.a = 1f;
                iconList[i].color = color;
            }
            else
            {
                Color color = defaultColor;
                color.a = 0.5f;
                iconList[i].color = color;
            }
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
    protected void Start()
    {
        AddOnClickMethodsToIcons();
    }
}

