/* SpreadSheet.cs
 * Wayway Studio
 * Google Spread Sheet Parsing Pacakge
 * Independent Packages
 * Odin Inspector, Wayway.Editor Require
 * 2021.09.13 */
using System.Collections.Generic;
using UnityEngine;

using Wayway.Engine.SpreadSheets;

namespace Wayway.Engine
{
    public class SpreadSheet : ScriptableObject
    {
        public string Title;
        public string ID;
        public string Url;
        public int Headline = 2;
        
        [SerializeField] private List<WorkSheet> workSheetList;
        
        public List<WorkSheet> WorkSheetList => workSheetList ??= new List<WorkSheet>();

#if UNITY_EDITOR
        #region -editor Functions::Utility
        public void ShowInWeb() => Application.OpenURL(Url);
        public void Update() => SpreadSheetEditorUtility.UpdateSpreadSheet(this);
        public void RemoveEmpty() => WorkSheetList.RemoveNull();
        #endregion
#endif
    }
}