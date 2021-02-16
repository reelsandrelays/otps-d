using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[HideMonoScript]
public class WorkSheet : SerializedScriptableObject
{
    #region FIELD    
    [ReadOnly] public SpreadSheet SpreadSheet;
    [DisplayAsString] public string Title;
    [DisplayAsString] public int HeadLine;    
    [DisplayAsString] public int RowMax;
    [DisplayAsString] public int ColumnMax;    

    [Serializable]
    public class RowGroup
    {
        public RowGroup() { cell = new List<string>(); }

        [HorizontalGroup(0.1f)]
        [DisplayAsString, HideLabel]
        [SerializeField]
        public int index;
        [HorizontalGroup]
        [ListDrawerSettings(HideRemoveButton = true, HideAddButton = true, IsReadOnly = true)]
        [SerializeField]
        public List<string> cell;
    }
    
    [ListDrawerSettings(HideRemoveButton = true, HideAddButton = true, IsReadOnly = true, Expanded = true), PropertySpace(SpaceBefore = 10f)]
    public List<RowGroup> Cells;

    #endregion 
    public List<string> Header { get => Cells[HeadLine - 1].cell; }


    /// <summary>
    /// GetCellValue by String Type
    /// </summary>
    /// <param name="row">ZeroBase Ordering ex.SpreadSheet 1 row equals 0</param>
    /// <param name="col">ZeroBase Ordering ex.SpreadSheet A column equals 0</param>
    /// <returns></returns>
    public string GetValue(int row, int col)
    {
        return Cells[row].cell[col];
    }    
    /// <summary>
    /// Get Row number by specific string
    /// </summary>
    /// <param name="value">key</param>
    /// <returns>ZeroBase Number</returns>
    public int GetRow(string value)
    {
        foreach (var rowGroup in Cells)
        {
            if (rowGroup.cell.Exists(x => x == value))
            {
                return rowGroup.index;
            }
        }

        Debug.LogError("Can't Find " + value + " in sheetData");
        return 0;
    }    
    /// <summary>
    /// Get Column number by specific string (Header first searching)
    /// </summary>
    /// <param name="value">key</param>
    /// <returns>ZeroBase number</returns>
    public int GetColumn(string value)
    {
        // Header 먼저 검색
        if (Header.Exists(x => x.Equals(value)))
        {
            return Header.FindIndex(x => x.Equals(value));
        }
        else
        {
            foreach (var rowGroup in Cells)
            {
                if (rowGroup.index == HeadLine)
                    continue;

                return rowGroup.cell.FindIndex(x => x == value);
            }
        }
        Debug.LogError("Can't Find " + value + " in sheetData");
        return 0;
    }
}
