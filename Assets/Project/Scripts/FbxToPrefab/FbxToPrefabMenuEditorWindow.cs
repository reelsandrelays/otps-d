#if UNITY_EDITOR

using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class FbxToPrefabMenuEditorWindow : WayWayMenuEditorWindow
{
    private FbxToPrefab fbxToPrefabItem;
    
    [MenuItem("Wayway/rnr/FbxToPrefab")]
    public static void OpenWindow() => GetWindow<FbxToPrefabMenuEditorWindow>().Show();
    
    protected override void OnEnable()
    {
        base.OnEnable();
        fbxToPrefabItem = (FbxToPrefab)CreateInstance(typeof(FbxToPrefab));
    }

    protected override void InsertMenuItems(OdinMenuTree tree)
    {
        tree.Add("Fbx To Prefab", fbxToPrefabItem);
        base.InsertMenuItems(tree);
    }
    
    /* Using Loading SO(s) 
    protected override void InsertMenuItems(OdinMenuTree tree)
    {
        tree.AddAssetAtPath("Some Second Menu Item", "SomeAssetPath/SomeAssetFile.asset");
        tree.AddAllAssetsAtPath("Some Menu Item", "Some Asset Path", typeof(ScriptableObject), true)
        base.InsertMenuItems(tree);
    }
    */
}

public class FbxToPrefab : ScriptableObject
{
    /// <summary> Origins </summary> ///
    [SerializeField, OnValueChanged("Validation"), TitleGroup("Origins")] private GameObject headOrigin;
    [SerializeField, OnValueChanged("Validation"), TitleGroup("Origins")] private GameObject bodyOrigin;
    [SerializeField, OnValueChanged("Validation"), TitleGroup("Origins")] private GameObject armOrigin; 
    /* ... additional paths */

    /// <summary> FBXs </summary> ///
    [SerializeField, TitleGroup("FBXs")] private GameObject exportedFbx;
    [SerializeField, TitleGroup("FBXs")] private Transform headTransform;
    [SerializeField, TitleGroup("FBXs")] private Transform bodyTransform;
    [SerializeField, TitleGroup("FBXs")] private Transform armTransform;
    /* ... additional paths */

    /// <summary> Validation </summary> ///
    private bool isValidated;
    
    private void Validation() // Used At OnValueChanged Attribute.
    {
        if (true) isValidated = true;
    }


    /// <summary> About Getting Ingredients By AssetPaths Using OnValueChanged Attribute. </summary> ///
    private void GetOrigins()
    {
        GetMasterOrigin();
        GetFbx();
    }

    private void GetMasterOrigin()
    {
        
    }

    private void GetFbx()
    {
        
    }


    /// <summary> Prefab Generate Button </summary> ///
    [Button(ButtonSizes.Large), EnableIf("isValidated"), PropertyOrder(-1)]
    public void GenerateMasterPrefab()
    {
        if (!isValidated) return;
        // Do Something With Ingredients
    }
}

#endif