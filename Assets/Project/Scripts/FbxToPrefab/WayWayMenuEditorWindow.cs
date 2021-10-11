#if UNITY_EDITOR

using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class WayWayMenuEditorWindow : OdinMenuEditorWindow
{
    [SerializeField] private MenuStyle_Test menuStyle;
    [SerializeField] private string menuStylePath = "Assets/Project/Data/FbxToPrefab/MenuStyleTest.asset";

    protected override OdinMenuTree BuildMenuTree() 
    {
        var tree = new OdinMenuTree();
        InsertMenuItems(tree);
        return tree;
    }

    protected virtual void InsertMenuItems(OdinMenuTree tree)
    {
        menuStyle ??= (MenuStyle_Test)AssetDatabase.LoadAssetAtPath(menuStylePath, typeof(MenuStyle_Test));
        if (!menuStyle) return;
        
        tree.DefaultMenuStyle = menuStyle.Style;
        menuStyle.Tree = tree;
        
        var menuStyleItem = new OdinMenuItem(tree, "Menu Style", tree.DefaultMenuStyle);
        tree.MenuItems.Add(menuStyleItem);
        
        tree.Add("Menu Style/Save Styles", menuStyle);
    }
}

#endif