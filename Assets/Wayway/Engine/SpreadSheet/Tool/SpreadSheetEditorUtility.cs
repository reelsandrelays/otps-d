/* Package Info - SpreadSheet.cs */
#if UNITY_EDITOR
using UnityEditor;

namespace Wayway.Engine.SpreadSheets
{
    public static class SpreadSheetEditorUtility
    {
        public static SpreadSheet TryGetSpreadSheet(string folderPath, string spreadSheetName)
        {
            var filter = $"t:{typeof(SpreadSheet).FullName} {spreadSheetName}";
            var targetFolder = $"{folderPath}/{spreadSheetName}";

            if (!AssetDatabase.IsValidFolder(targetFolder))
            {
                AssetDatabase.CreateFolder(folderPath, spreadSheetName);
            }

            var spreadSheet = ScriptableObjectUtility.GetScriptableObject<SpreadSheet>(targetFolder, filter);

            return spreadSheet ?? ScriptableObjectUtility.CreateScriptableObject<SpreadSheet>(targetFolder, spreadSheetName);
        }

        public static WorkSheet TryGetWorkSheet(string folderPath, string workSheetName)
        {
            var filter = $"t:{typeof(WorkSheet).FullName} {workSheetName}";            
            var workSheet = ScriptableObjectUtility.GetScriptableObject<WorkSheet>(folderPath, filter);     

            return workSheet ?? ScriptableObjectUtility.CreateScriptableObject<WorkSheet>(folderPath, workSheetName);
        }
        
        public static void UpdateSpreadSheet(SpreadSheet spreadSheet)
        {
            ScriptableObjectUtility.GetScriptableObject<SpreadSheetImporter>().GenerateSpreadSheet(spreadSheet.ID);
        }
    }
}
#endif