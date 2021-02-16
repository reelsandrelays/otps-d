using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Importer", menuName = "SpreadSheet/Importer")]
public class SpreadSheetImporter : ScriptableSingleton<SpreadSheetImporter>
{
    private const string SUFFIX         = "SpreadSheet";
    private const string SHEETSUFFIX    = "Sheet";
    private Google.Apis.Sheets.v4.Data.Spreadsheet apiData;

    [FolderPath]
    public string SavePath;
    public string SheetID;

    /// <summary>
    /// SpreadSheet Import Main Function
    /// </summary>
    [Button(ButtonSizes.Large), PropertySpace(SpaceAfter = 0, SpaceBefore = 5f)]
    public void GenerateSpreadSheet() => GenerateSpreadSheet(SheetID);
    public void GenerateSpreadSheet(string sheetID)
    {
        apiData = ClientInformation.instance.GetSpreadSheetData(sheetID);
        var spreadSheet = TryGetSpreadSheetData(apiData.Properties.Title);

        spreadSheet.ID = sheetID;
        spreadSheet.Url = apiData.SpreadsheetUrl;
        spreadSheet.Title = apiData.Properties.Title;

        GenerateWorkSheets(spreadSheet);

        EditorUtility.SetDirty(spreadSheet);
    }

    public void GenerateWorkSheets(SpreadSheet spreadSheet)
    {
        for (var i = 0; i < apiData.Sheets.Count; ++i)
        {
            string workSheetTitle = apiData.Sheets[i].Properties.Title;

            GenerateWorkSheet(spreadSheet, workSheetTitle);
        }
    }


    private SpreadSheet TryGetSpreadSheetData(string title)
    {
        string fileName = $"{title}{SUFFIX}";

        if (!FindSpreadSheet(fileName))
        {
            CreateSpreadSheet(fileName);
        }

        return GetSpreadSheet(fileName);
    }

    private WorkSheet TryGetWorkSheetData(SpreadSheet spreadsheet, string title)
    {
        string fileName = $"{title}{SHEETSUFFIX}";

        if (!FindWorkSheet(spreadsheet, fileName))
        {
            CreateWorkSheet(spreadsheet, fileName);
        }

        return GetWorkSheet(spreadsheet, fileName);
    }

    private void GenerateWorkSheet(SpreadSheet spreadSheet, string workSheetTitle)
    {
        WorkSheet workSheet = TryGetWorkSheetData(spreadSheet, workSheetTitle);
        workSheet.SpreadSheet = spreadSheet;
        workSheet.Title = workSheetTitle;
        workSheet.HeadLine = spreadSheet.Headline;

        var workSheetApiData = ClientInformation.instance.GetWorkSheetAPIData(spreadSheet.ID, workSheet.Title);

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
            workSheet.Cells[row].index = row;

            for (int col = 0; col < workSheet.ColumnMax; ++col)
            {
                if (col >= dataByRow[row].Count)
                {
                    workSheet.Cells[row].cell.Add("");
                }
                else
                    workSheet.Cells[row].cell.Add(dataByRow[row][col].ToString());
            }
        }

        AddWorkSheetToSpreadSheet(workSheet);

        EditorUtility.SetDirty(workSheet);
    }

    private void AddWorkSheetToSpreadSheet(WorkSheet workSheet)
    {
        if (workSheet.SpreadSheet.WorkSheetList == null)
        {
            workSheet.SpreadSheet.WorkSheetList = new List<WorkSheet>();
        }

        if (!workSheet.SpreadSheet.WorkSheetList.Contains(workSheet))
        {
            workSheet.SpreadSheet.WorkSheetList.Add(workSheet);
            Debug.Log("Add : <b><color=green>" + workSheet.Title + "</color></b> to <b>" + workSheet.SpreadSheet.Title + "</b>");
        }
        else
        {
            Debug.Log("Reload : <b><color=green>" + workSheet.Title + "</color></b>");
        }
    }

    #region Create, Find and Get SpreadSheet
    private void CreateSpreadSheet(string fileName)
    {
        SpreadSheet item = CreateInstance<SpreadSheet>();
        AssetDatabase.CreateAsset
            (item, $"{SavePath}/{fileName}.asset");

        Debug.Log("Create <b><color=green> " + fileName + " </color></b> Data File.");
    }

    private bool FindSpreadSheet(string fileName)
    {
        string[] assetDataGuid = AssetDatabase.FindAssets
            ($"t:{typeof(SpreadSheet).FullName} {fileName}", new[] { SavePath });

        // Can't Find
        if (assetDataGuid.Length < 1)
        {
            return false;
        }
        // Sucessfully Found
        else if (assetDataGuid.Length.Equals(1))
        {
            return true;
        }
        // Multiple Found
        else
        {
            Debug.LogWarning("Found Mutiple Name of : <b><color=yellow>" + fileName + "</color></b>");
            return true;
        }
    }

    private SpreadSheet GetSpreadSheet(string fileName)
    {
        SpreadSheet item = null;

        string assetPath;
        string[] assetDataGuid = AssetDatabase.FindAssets
            ($"t:{typeof(SpreadSheet).FullName} {fileName}", new[] { SavePath });

        // Can't Find
        if (assetDataGuid.Length < 1)
        {
            Debug.LogError("Can't Find '<b><color=red>" + fileName + "</color></b>' Asset Data");
        }

        // Sucessfully Found
        else if (assetDataGuid.Length.Equals(1))
        {
            assetPath = AssetDatabase.GUIDToAssetPath(assetDataGuid[0]);
            item = AssetDatabase.LoadAssetAtPath(assetPath, typeof(SpreadSheet)) as SpreadSheet;
        }

        // Multiple Found
        else
        {
            Debug.LogWarning("Find Multiple Data");

            for (int i = 0; i < assetDataGuid.Length; ++i)
            {
                assetPath = AssetDatabase.GUIDToAssetPath(assetDataGuid[i]);
                item = AssetDatabase.LoadAssetAtPath(assetPath, typeof(SpreadSheet)) as SpreadSheet;
                if (item.name.Equals(fileName))
                {
                    Debug.LogWarning("Selected Data : <b><color=yellow>" + item.name + "</color></b>");
                    break;
                }
            }
        }

        return item;
    }
    private void CreateWorkSheet(SpreadSheet spreadSheet, string fileName)
    {
        WorkSheet wsData = CreateInstance<WorkSheet>();

        string autoGeneratedDataPath = $"{SavePath}/{spreadSheet.Title}";

        if (!AssetDatabase.IsValidFolder(autoGeneratedDataPath))
        {
            AssetDatabase.CreateFolder(SavePath, spreadSheet.Title);
        }

        AssetDatabase.CreateAsset
            (wsData, $"{autoGeneratedDataPath}/{fileName}.asset");

        Debug.Log("Create <b><color=green> " + fileName + " </color></b>Sheet File.");
    }
    private bool FindWorkSheet(SpreadSheet spreadSheet, string fileName)
    {
        string[] assetDataGuid = AssetDatabase.FindAssets
            ($"t:{typeof(WorkSheet).FullName} {fileName}", new[] { SavePath });

        // Can't Find
        if (assetDataGuid.Length < 1)
        {
            return false;
        }
        // Sucessfully Found
        else if (assetDataGuid.Length.Equals(1))
        {
            return true;
        }
        // Multiple Found
        else
        {
            Debug.LogWarning("Found Mutiple Name of : <b><color=yellow>" + fileName + "</color></b>");
            return true;
        }
    }
    private WorkSheet GetWorkSheet(SpreadSheet data, string fileName)
    {
        if (!FindWorkSheet(data, fileName))
        {
            Debug.LogError("Can't Find '" + fileName + "' Asset Data");
            return null;
        }

        WorkSheet wsData = null;

        string assetPath;
        string[] assetDataGuid = AssetDatabase.FindAssets
            ($"t:{typeof(WorkSheet).FullName} {fileName}", new[] { SavePath });

        if (assetDataGuid.Length > 1)
        {
            Debug.LogWarning("Find Multiple Data");

            for (int i = 0; i < assetDataGuid.Length; ++i)
            {
                assetPath = AssetDatabase.GUIDToAssetPath(assetDataGuid[i]);
                wsData = AssetDatabase.LoadAssetAtPath(assetPath, typeof(WorkSheet)) as WorkSheet;
                if (wsData.name.Equals(fileName))
                    break;
            }
        }
        else if (assetDataGuid.Length == 1)
        {
            assetPath = AssetDatabase.GUIDToAssetPath(assetDataGuid[0]);
            wsData = AssetDatabase.LoadAssetAtPath(assetPath, typeof(WorkSheet)) as WorkSheet;
        }

        return wsData;
    } 
    #endregion
}