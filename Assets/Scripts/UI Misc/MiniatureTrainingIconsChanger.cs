using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureTrainingIconsChanger : MiniatureIconsChanger
{
    public override void CreateIcons(IDataBase dataBase)
    {
    }
    public void CreateIcons(TrainingBaseData trainingBaseData)
    {
        foreach (var item in iconList)
        {
            DestroyImmediate(item.gameObject);
        }

        iconList.Clear();
        iconList.TrimExcess();

        foreach (ISnapperPanelData data in trainingBaseData.TrainingDataSectionList)
        {
            iconExample.gameObject.SetActive(true);
            Button newIcon = Instantiate(iconExample, iconsParent);
            Image iconImage = newIcon.GetComponent<Image>();
            iconImage.sprite = data.GetMiniatureIcon();

            int index = iconList.Count;
            newIcon.name = "Icon " + index;
            iconList.Add(iconImage);
        }
        iconExample.gameObject.SetActive(false);
    }
}
