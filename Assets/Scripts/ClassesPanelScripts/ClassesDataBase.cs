using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IDataBase
{
}
public class ClassesDataBase : MonoBehaviour, IDataBase
{ 
    public List<ClassData> AllClasses = new List<ClassData>();

}
