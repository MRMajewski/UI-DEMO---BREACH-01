using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClassData", menuName = "Classes/ClassData")]
public class ClassData : ScriptableObject
{
    public string itemName;
    public string description;
    public string subclassesDescription;
    public Sprite classIcon;

    [Tooltip("Categories this item belongs to")]
    public string[] skills;
}
