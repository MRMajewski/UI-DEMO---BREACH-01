using System.Collections.Generic;

public class MiniatureClassIconsChanger : MiniatureIconsCreatorChanger
{
    protected override IEnumerable<ISnapperPanelData> GetIconData(IDataBase dataBase)
    {
        if (dataBase is ClassesDataBase classData)
        {
            return classData.AllClasses;
        }
        return new List<ISnapperPanelData>();
    }
}
