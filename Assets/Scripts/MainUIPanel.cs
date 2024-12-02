using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIPanel : SimpleUIPanelMobiles
{

    public override void DisablePanel()
    {
        this.gameObject.SetActive(false);
    }

    public override void EnablePanel()
    {
       this.gameObject.SetActive(true);
    }

}
