using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[HideMonoScript]
public class SpreadSheet : ScriptableObject
{
    #region FIELD
    [DisplayAsString] public string Title;
    [DisplayAsString] public string ID;
    [DisplayAsString] public string Url;
    [DisplayAsString] public int Headline = 2;

    [ListDrawerSettings(HideRemoveButton = true, HideAddButton = true, IsReadOnly = true, Expanded = true), PropertySpace(SpaceBefore = 10f)]
    public List<WorkSheet> WorkSheetList;
    #endregion    

    public WorkSheet GetSheet(string worksheetName)
    {
        foreach(var workSheet in WorkSheetList)
        {
            if (workSheet.Title.Equals(worksheetName))
                return workSheet;
        }

        Debug.LogError("Can't Find <color=red>" + worksheetName + " </color>Sheet");
        return null;
    }
}
