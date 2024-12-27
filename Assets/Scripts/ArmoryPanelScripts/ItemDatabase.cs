using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [Tooltip("List of all items available in the game.")]
    public List<ItemData> AllItems = new List<ItemData>();

    private void Start()
    {
        if(AllItems==null)
        {
            AllItems = Resources.LoadAll<ItemData>("ArmoryScriptables").ToList();
        }
    }
}
