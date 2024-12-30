using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClassesDataBase : MonoBehaviour
{
    public List<ClassData> AllClasses = new List<ClassData>();

    private void Start()
    {
        if (AllClasses == null)
        {
            AllClasses = Resources.LoadAll<ClassData>("ArmoryScriptables").ToList();
        }
    }
}
