/* ScriptableObjectUtility.cs
 * Wayway Studio
 * Editor class
 * Assembly c#.Editor - Assembly c# bridge
 * ObjectUtility Require
 * 2021.09.13 */
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

namespace Wayway.Engine
{
    public static class ScriptableObjectUtility
    {
        public static T GetScriptableObject<T>() where T : ScriptableObject => GetScriptableObject<T>("");
        public static T GetScriptableObject<T>(string folderPath) where T : ScriptableObject => GetScriptableObject<T>(folderPath, "");

        /// <summary>
        /// Get Single ScriptableObject in folderPath with filter
        /// </summary>
        /// <typeparam name="T">ScriptableObject</typeparam>
        /// <param name="folderPath">Searching Target Path, ex Assets/Project</param>
        /// <param name="filter">Unity Filter rule, ex l:data, t:className</param>
        /// <returns>(if multiple), Return List.First()</returns>
        public static T GetScriptableObject<T>(string folderPath, string filter) where T : ScriptableObject
        {
            T result;

            if (string.IsNullOrEmpty(folderPath)) folderPath = "Assets";
            if (string.IsNullOrEmpty(filter)) filter = $"t:{typeof(T).FullName}";

            string[] gUIDs = AssetDatabase.FindAssets(filter, new string[] { folderPath });

            if (gUIDs == null || gUIDs.Length == 0)
            {
                Debug.Log($"Can't Find in {folderPath} of {typeof(T).FullName}");
                return null;
            }
            else if (gUIDs.Length > 1)
            {
                Debug.LogWarning($"Multiple Founded : count : {gUIDs.Length}");
            }

            var assetPath = AssetDatabase.GUIDToAssetPath(gUIDs[0]);
            result = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T;

            return result;
        }

        public static T[] GetScriptableObjectArray<T>() where T : ScriptableObject => GetScriptableObjectArray<T>("");
        public static T[] GetScriptableObjectArray<T>(string folderPath) where T : ScriptableObject => GetScriptableObjectArray<T>(folderPath, "");
        public static T[] GetScriptableObjectArray<T>(string folderPath, string filter) where T : ScriptableObject => GetScriptableObjects<T>(folderPath, filter).ToArray();
        public static List<T> GetScriptableObjects<T>() where T : ScriptableObject => GetScriptableObjects<T>("");
        public static List<T> GetScriptableObjects<T>(string folderPath) where T : ScriptableObject => GetScriptableObjects<T>(folderPath, "");

        /// <summary>
        /// Get ScriptableObjects in folderPath with filter
        /// </summary>
        /// <typeparam name="T">ScriptableObject</typeparam>
        /// <param name="folderPath">Searching Target Path, ex Assets/Project</param>
        /// <param name="filter">Unity Filter rule, ex l:data, t:className</param>
        /// <returns>ScriptableObject List</returns>
        public static List<T> GetScriptableObjects<T>(string folderPath, string filter) where T : ScriptableObject
        {
            var result = new List<T>();

            if (string.IsNullOrEmpty(folderPath)) folderPath = "Assets";
            if (string.IsNullOrEmpty(filter)) filter = $"t:{typeof(T).FullName}";

            string[] gUIDs = AssetDatabase.FindAssets(filter, new string[] { folderPath });

            gUIDs.ForEach(x =>
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(x);
                var data = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T;

                if (!result.Contains(data))
                    result.Add(data);
            });

            return result;
        }

        public static T CreateScriptableObject<T>(string folderPath) where T : ScriptableObject => CreateScriptableObject<T>(folderPath, "");
        public static T CreateScriptableObject<T>(string folderPath, string defaultName) where T : ScriptableObject
        {
            if (folderPath == null)
            {
                Debug.LogError("at least one of a Parameta value is <b><color=red>NULL!</color></b>");
                return null;
            }
            if (string.IsNullOrEmpty(defaultName))
            {
                defaultName = typeof(T).FullName;
                Debug.Log($"name Created by automatically <b><color=green>{typeof(T).FullName}</color></b>");
            }

            var data = ScriptableObject.CreateInstance<T>();
            var uniqueName = ObjectUtility.GetUniqueNameWithPath(folderPath, defaultName);

            AssetDatabase.CreateAsset(data, uniqueName);

            Debug.Log($"Create <b><color=green>{uniqueName}</color></b> Scriptable Object.");

            return data;
        }
    }
}
#endif