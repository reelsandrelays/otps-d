#if UNITY_EDITOR

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuStyleTest", menuName = "Wayway/rnr/MenuStyleTest")]
public class MenuStyle_Test : ScriptableObject
    {
        /// <summary> style configs. </summary>
        private int height;
        private float offset;
        private float indentAmount;
        private float iconSize;
        private float iconOffset;
        private float notSelectedIconAlpha;
        private float iconPadding;
        private float triangleSize;
        private float trianglePadding;
        private bool alignTriangleLeft;
        private bool borders;
        private float borderPadding;
        private float borderAlpha;
        private Color selectedColorDarkSkin;
        private Color selectedColorLightSkin;


        private OdinMenuTree tree;
        public OdinMenuTree Tree
        {
            set => tree = value;
        }
        
        public OdinMenuStyle Style => new OdinMenuStyle() 
        {
            Height = height,
            Offset = offset,
            IndentAmount = indentAmount,
            IconSize = iconSize,
            IconOffset = iconOffset,
            NotSelectedIconAlpha = notSelectedIconAlpha,
            IconPadding = iconPadding,
            TriangleSize = triangleSize,
            TrianglePadding = trianglePadding,
            AlignTriangleLeft = alignTriangleLeft,
            Borders = borders,
            BorderPadding = borderPadding,
            BorderAlpha = borderAlpha,
            SelectedColorDarkSkin = selectedColorDarkSkin,
            SelectedColorLightSkin = selectedColorLightSkin,
        };
        
        
        [Button(ButtonSizes.Gigantic)]
        public void SaveMenuStyle()
        {
            height = tree.DefaultMenuStyle.Height;
            offset = tree.DefaultMenuStyle.Offset;
            indentAmount = tree.DefaultMenuStyle.IndentAmount;
            iconSize = tree.DefaultMenuStyle.IconSize;
            iconOffset = tree.DefaultMenuStyle.IconOffset;
            notSelectedIconAlpha = tree.DefaultMenuStyle.NotSelectedIconAlpha;
            iconPadding = tree.DefaultMenuStyle.IconPadding;
            triangleSize = tree.DefaultMenuStyle.TriangleSize;
            trianglePadding = tree.DefaultMenuStyle.TrianglePadding;
            alignTriangleLeft = tree.DefaultMenuStyle.AlignTriangleLeft;
            borders = tree.DefaultMenuStyle.Borders;
            borderPadding = tree.DefaultMenuStyle.BorderPadding;
            borderAlpha = tree.DefaultMenuStyle.BorderAlpha;
            selectedColorDarkSkin = tree.DefaultMenuStyle.SelectedColorDarkSkin;
            selectedColorLightSkin = tree.DefaultMenuStyle.SelectedColorLightSkin;
        }
        
        
        [Button]
        public void GetDefaultStyleConfigs()
        {
            height = 30;
            offset = 16.00f;
            indentAmount = 15.00f;
            iconSize = 16.00f;
            iconOffset = 0.00f;
            notSelectedIconAlpha = 0.85f;
            iconPadding = 3.00f;
            triangleSize = 17.00f;
            trianglePadding = 8.00f;
            alignTriangleLeft = false;
            borders = true;
            borderPadding = 13.00f;
            borderAlpha = 0.50f;
            selectedColorDarkSkin = new Color(0.243f, 0.373f, 0.588f, 1.000f);
            selectedColorLightSkin = new Color(0.243f, 0.490f, 0.900f, 1.000f);
            
            tree.DefaultMenuStyle = Style;
            tree.ScrollToMenuItem(tree.MenuItems[0]);
        }
    }

#endif
