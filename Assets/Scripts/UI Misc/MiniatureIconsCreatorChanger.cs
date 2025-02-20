using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MiniatureIconsCreatorChanger : MiniatureIconsChanger
{
    protected abstract IEnumerable<ISnapperPanelData> GetIconData(IDataBase dataBase);
    public void CreateIcons(IDataBase dataBase)
    {
        if (dataBase == null)
        {
            Debug.LogWarning("Brak bazy danych!");
            return;
        }
        ClearIcons();
        iconExample.gameObject.SetActive(true);

        GetIconData(dataBase);
        Debug.Log(GetIconData(dataBase));

        foreach (ISnapperPanelData data in GetIconData(dataBase))
        {
            Button newIcon = Instantiate(iconExample, iconsParent);
            Image iconImage = newIcon.GetComponent<Image>();
            iconImage.sprite = data.GetMiniatureIcon();

            newIcon.name = "Icon " + iconList.Count;
            iconList.Add(iconImage);
        }
        iconExample.gameObject.SetActive(false);
        AddOnClickMethodsToIcons();

        void ClearIcons()
        {
            foreach (var item in iconList)
            {
                DestroyImmediate(item.gameObject);
            }
            iconList.Clear();
            iconList.TrimExcess();
        }
    }

}
