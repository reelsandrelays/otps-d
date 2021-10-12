/* ObjectUtility.cs
 * Wayway Studio
 * Editor class
 * Assembly c#.Editor - Assembly c# bridge
 * Nothing Require
 * 2021.09.13 */
#if UNITY_EDITOR
using UnityEditor;
using System.Linq;

namespace Wayway.Engine
{
    public static class ObjectUtility
    {
        /// <summary>
        /// Get Object "Only" Name without Path
        /// </summary>
        /// <param name="folderPath">Searching FolderPath</param>
        /// <param name="defaultName">Set Default Name</param>
        /// <returns>FileName</returns>
        public static string GetUniqueName(string folderPath, string defaultName)
        {
            var uniqueID = GetUniqueNameWithPath(folderPath, defaultName);
            uniqueID = uniqueID.Remove(0, folderPath.Length + 1);

            return uniqueID;
        }

        /// <summary>
        /// Get Object Unique name, with index;
        /// ex. Assets/Project/File -> File 01, File 02...
        /// </summary>
        /// <param name="folderPath">Searching Folder Path, ex Assets/Project</param>
        /// <param name="defaultName">Set Default Name, ex FileName (if isExist, return FileName 01)</param>
        /// <returns>Unique Name with path ex Assets/Project/FileName 01.asset</returns>
        public static string GetUniqueNameWithPath(string folderPath, string defaultName)
        {
            return AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/{defaultName}.asset");
        }

        /// <summary>
        /// Get Type Class Path
        /// </summary>
        /// <typeparam name="T">Any of Class</typeparam>
        /// <param name="includeFileName">Return include FileName</param>
        /// <returns>ex Aseets/Project/FileName.asset</returns>
        public static string GetObjectPath<T>(bool includeFileName) where T : class
        {
            string gUID = AssetDatabase.FindAssets($"t:{typeof(T).FullName}").First();
            string assetPath = AssetDatabase.GUIDToAssetPath(gUID);

            return includeFileName ?
                assetPath :
                assetPath.Remove(assetPath.LastIndexOf('/') + 1, assetPath.Length - (assetPath.LastIndexOf('/') + 1));
        }

        /// <summary>
        /// Get Type Class Name, without Path
        /// </summary>
        /// <typeparam name="T">Any of Class</typeparam>
        /// <param name="includeExtension">include fileExtension ex .asset .meta .cs</param>
        /// <returns>FileName</returns>
        public static string GetObjectName<T>(bool includeExtension)
        {
            string gUID = AssetDatabase.FindAssets($"t:{typeof(T).FullName}").First();
            string assetPath = AssetDatabase.GUIDToAssetPath(gUID);

            return includeExtension ?
                assetPath.Remove(0, assetPath.LastIndexOf('/') + 1) :
                assetPath.Remove(0, assetPath.LastIndexOf('/') + 1).Replace(".asset", "");
        }

        public static void Save(UnityEngine.Object target)
        {
            EditorUtility.SetDirty(target);
        } 
    }
}
#endif