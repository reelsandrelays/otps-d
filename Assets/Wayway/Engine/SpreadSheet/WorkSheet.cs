using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wayway.Engine
{
    public class WorkSheet : ScriptableObject
    {
        public SpreadSheet SpreadSheet;
        public string Title;
        public int HeadLine;
        public int RowMax;
        public int ColumnMax;
        public List<RowGroup> Cells;

        public List<string> Header => Cells[HeadLine - 1].Cell;

        /// <summary>
        /// GetCellValue by String Type
        /// </summary>
        /// <param name="row">ZeroBase Ordering ex.SpreadSheet 1 row equals 0</param>
        /// <param name="col">ZeroBase Ordering ex.SpreadSheet A column equals 0</param>
        /// <returns></returns>
        public string GetValue(int row, int col)
        {
            return Cells[row].Cell[col];
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
                if (rowGroup.Cell.Exists(x => x == value))
                {
                    return rowGroup.Index;
                }
            }
            Debug.LogError($"Can't Find {value} in sheetData");
            return 0;
        }

        /// <summary>
        /// Searching value in specific column, return Row Index
        /// </summary>
        /// <param name="column">searching Target column index</param>
        /// <param name="value">searching Value in rows at specific column</param>
        /// <returns>Row Index</returns>
        public int GetRowInColumn(int column, string value)
        {
            for (var i = 0; i < RowMax; ++i)
            {
                if (GetValue(i, column) == value) return i;
            }
            Debug.LogError($"Can't Find {value} in column Numer {column}");
            return -1;
        }

        /// <summary>
        /// Get Column number by specific string (Header first searching)
        /// </summary>
        /// <param name="value">key</param>
        /// <returns>ZeroBase number</returns>
        public int GetColumn(string value)
        {
            // Searching Header at First 
            if (Header.Exists(x => x.Equals(value)))
            {
                return Header.FindIndex(x => x.Equals(value));
            }

            foreach (var rowGroup in Cells)
            {
                if (rowGroup.Index == HeadLine)
                    continue;

                return rowGroup.Cell.FindIndex(x => x.Equals(value));
            }

            Debug.LogError("Can't Find " + value + " in sheetData");
            return 0;
        }

        [Serializable]
        public class RowGroup
        {
            public RowGroup() { Cell = new List<string>(); }

            public int Index;
            public List<string> Cell;
        }

#if UNITY_EDITOR        
        public void ShowInWeb() => SpreadSheet.ShowInWeb();
        public void Update() => SpreadSheet.Update();
#endif
    }
}


