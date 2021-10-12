/* SpreadSheetImporter.cs
 * Wayway Studio
 * Spreadsheet Importer
 * Editor Class
 * ScriptableObjectCreator, Odin Inspector, ClientInformation Require
 * 2021.09.13 */

using System.Collections.Generic;
using UnityEngine;

namespace Wayway.Engine.SpreadSheets
{
    public class SpreadSheetImporter : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private ClientInformation clientInformation;
        [SerializeField] private string savePath;
        [SerializeField] private string sheetID;
        [SerializeField] private string[] ignorePrefix = new string[] { "_" };

        public void Generate() => GenerateSpreadSheet(sheetID);

        public void GenerateSpreadSheet(string sheetID)
        {
            var apiData = clientInformation.GetSpreadSheetData(sheetID);
            var spreadSheetName = $"{apiData.Properties.Title}";
            var spreadSheet = SpreadSheetEditorUtility.TryGetSpreadSheet(savePath, spreadSheetName);            

            spreadSheet.ID = sheetID;
            spreadSheet.Url = apiData.SpreadsheetUrl;
            spreadSheet.Title = apiData.Properties.Title;

            apiData.Sheets.ForEach(x => GenerateWorkSheet(spreadSheet, x.Properties.Title));
            spreadSheet.WorkSheetList.RemoveNull();

            ObjectUtility.Save(spreadSheet);
        }

        public void GenerateWorkSheet(SpreadSheet spreadSheet, string workSheetName)
        {
            if (!IsValidWorksheet(workSheetName)) return;
            
            var workSheetSavePath = $"{savePath}/{spreadSheet.Title}";
            var workSheetFullName = $"{spreadSheet.Title}.{workSheetName}";
            var workSheet = SpreadSheetEditorUtility.TryGetWorkSheet(workSheetSavePath, workSheetFullName);

            workSheet.SpreadSheet = spreadSheet;
            workSheet.Title = workSheetName;
            workSheet.HeadLine = spreadSheet.Headline;

            var workSheetApiData = clientInformation.GetWorkSheetAPIData(spreadSheet.ID, workSheet.Title);

            var dataByRow = workSheetApiData.Execute().Values;
            workSheetApiData.MajorDimension =
                (Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum)2;
            var dataByCol = workSheetApiData.Execute().Values;

            workSheet.RowMax = dataByRow.Count;
            workSheet.ColumnMax = dataByCol.Count;
            workSheet.Cells = new List<WorkSheet.RowGroup>();            

            for (int row = 0; row < workSheet.RowMax; ++row)
            {
                workSheet.Cells.Add(new WorkSheet.RowGroup());
                workSheet.Cells[row].Index = row;

                for (int col = 0; col < workSheet.ColumnMax; ++col)
                {
                    var value = col >= dataByRow[row].Count ? string.Empty
                                                            : dataByRow[row][col].ToString();

                    workSheet.Cells[row].Cell.Add(value);
                }
            }

            AddWorkSheetToSpreadSheet(workSheet);

            ObjectUtility.Save(workSheet);
        }


        private void AddWorkSheetToSpreadSheet(WorkSheet workSheet)
        {
            var workSheetList = workSheet.SpreadSheet.WorkSheetList;
            
            if (!workSheetList.Contains(workSheet))
            {
                workSheetList.Add(workSheet);
                Debug.Log($"Add : <b><color=green>{workSheet.Title} </color></b> to <b>{workSheet.SpreadSheet.Title}</b>");
            }
            else
            {
                Debug.Log($"Reload : <b><color=green>{workSheet.Title}</color></b>");
            }
        }

        private bool IsValidWorksheet(string worksSheetTitle)
        {
            foreach (var prefix in ignorePrefix)
            {
                if (worksSheetTitle.Trim().StartsWith(prefix))
                {
                    Debug.Log($"Sheet prefix ignorance Activated prefix : <color=yellow>{prefix}</color>");
                    return false;
                }
            }

            return true;
        }
#endif
    }
}