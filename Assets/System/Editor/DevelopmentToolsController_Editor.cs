using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DevelopmentToolsController))]
public class DevelopmentToolsController_Editor : Editor
{
    private DevelopmentToolsController controller;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Get Tool Pieces", GUILayout.Height(32f)))
        {
            controller.developmentToolPieces = FindObjectsOfTypeAll(typeof(DevelopmentTool)) as DevelopmentTool[];
            EditorUtility.SetDirty(controller);
        }

        if (GUILayout.Button("Activate", GUILayout.Height(32f)))
        {
            if (controller.developmentToolPieces == null) return;

            for (int i = 0; i < controller.developmentToolPieces.Length; i++) { controller.developmentToolPieces[i].gameObject.SetActive(true); }
            EditorUtility.SetDirty(controller);
        }

        if (GUILayout.Button("Deactivate", GUILayout.Height(32f)))
        {
            if (controller.developmentToolPieces == null) return;

            for (int i = 0; i < controller.developmentToolPieces.Length; i++) { controller.developmentToolPieces[i].gameObject.SetActive(false); }
            EditorUtility.SetDirty(controller);
        }
    }

    private void OnEnable() { controller = target as DevelopmentToolsController; }
    private void OnDisable() { controller = null; }
}
