using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClassData", menuName = "Classes/ClassData")]
public class ClassData : ScriptableObject
{
    public string className;
    [TextArea(1, 10)]
    public string classDescription;
    public Sprite classIcon;

    public string[] classSkills;
    public string[] subclasses;
}
