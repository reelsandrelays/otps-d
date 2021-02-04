using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomPropertyDrawer(typeof(Attribute_Graphics))]
public class Attribute_GraphicsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] splitPath = property.propertyPath.Split('[', ']');
        int index = int.Parse(splitPath[splitPath.Length - 2]);

        EditorGUI.PropertyField(position, property, new GUIContent(((GraphicLevel)index).ToString()), true);
    }
}

[CustomEditor(typeof(GameConfigurations))]
public class GameConfigurations_Editor : Editor
{
    private GameConfigurations configurations;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Delete Local Save", GUILayout.Height(32f)))
        {
            string path = Application.persistentDataPath + "/gameconfig.gcf";
            if (File.Exists(path)) File.Delete(path);

            EditorUtility.SetDirty(configurations);
        }
    }

    private void OnEnable() { configurations = target as GameConfigurations; }
    private void OnDisable() { configurations = null; }
}
