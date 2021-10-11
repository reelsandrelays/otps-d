#if UNITY_EDITOR

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuStyleTest", menuName = "Wayway/rnr/MenuStyleTest")]
public class MenuStyle_Test : ScriptableObject
    {
        /// <summary> style configs. </summary>
        [SerializeField, OnValueChanged("SaveMenuStyle")] private int height;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private float offset;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private float indentAmount;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private float iconSize;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private float iconOffset;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private float notSelectedIconAlpha;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private float iconPadding;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private float triangleSize;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private float trianglePadding;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private bool alignTriangleLeft;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private bool borders;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private float borderPadding;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private float borderAlpha;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private Color selectedColorDarkSkin;
        [SerializeField, OnValueChanged("SaveMenuStyle")] private Color selectedColorLightSkin;

        private OdinMenuTree tree;
        public OdinMenuTree Tree { set => tree = value; }
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
        
        // Used At OnValueChanged Attribute
        public void SaveMenuStyle()
        {
            tree.DefaultMenuStyle.Height = height;
            tree.DefaultMenuStyle.Offset = offset;
            tree.DefaultMenuStyle.IndentAmount = indentAmount;
            tree.DefaultMenuStyle.IconSize = iconSize;
            tree.DefaultMenuStyle.IconOffset = iconOffset;
            tree.DefaultMenuStyle.NotSelectedIconAlpha = notSelectedIconAlpha;
            tree.DefaultMenuStyle.IconPadding = iconPadding;
            tree.DefaultMenuStyle.TriangleSize = triangleSize;
            tree.DefaultMenuStyle.TrianglePadding = trianglePadding;
            tree.DefaultMenuStyle.AlignTriangleLeft = alignTriangleLeft;
            tree.DefaultMenuStyle.Borders = borders;
            tree.DefaultMenuStyle.BorderPadding = borderPadding;
            tree.DefaultMenuStyle.BorderAlpha = borderAlpha;
            tree.DefaultMenuStyle.SelectedColorDarkSkin = selectedColorDarkSkin;
            tree.DefaultMenuStyle.SelectedColorLightSkin = selectedColorLightSkin;
        }
        
        
        [Button("Set As Odin Default Configs.", ButtonSizes.Large),
         PropertySpace(0f, 10f),
         PropertyOrder(-1)]
        public void SetDefaultStyleConfigs()
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
            
            SaveMenuStyle();
        }
    }

#endif
