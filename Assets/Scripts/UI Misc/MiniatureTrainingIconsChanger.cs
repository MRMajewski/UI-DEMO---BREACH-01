using System.Collections.Generic;

public class MiniatureTrainingIconsChanger : MiniatureIconsCreatorChanger
{
    protected override IEnumerable<ISnapperPanelData> GetIconData(IDataBase dataBase)
    {
        if (dataBase is TrainingBaseData trainingBaseData)
        {
            return trainingBaseData.TrainingDataSectionList;
        }
        return new List<ISnapperPanelData>();
    }
}
