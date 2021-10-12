using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using System;
using System.Collections.Generic;
using System.Reflection;

using Wayway.Engine;
using Wayway.Engine.SpreadSheets;

namespace AttributionDrawer
{
    public class SpreadSheetDrawer : OdinAttributeProcessor<SpreadSheet>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            if (member.Name == "Title") attributes.Add(new DisplayAsStringAttribute());
            if (member.Name == "ID") attributes.Add(new DisplayAsStringAttribute());
            if (member.Name == "Url") attributes.Add(new DisplayAsStringAttribute());
            if (member.Name == "Headline")
            {
                attributes.Add(new PropertySpaceAttribute(0, 15));
                attributes.Add(new DisplayAsStringAttribute());
            }
            if (member.Name == "workSheetList")
            {
                var listDrawerSetting = new ListDrawerSettingsAttribute()
                {
                    HideRemoveButton = true,
                    HideAddButton = true,
                    IsReadOnly = true,
                    Expanded = true
                };

                attributes.Add(listDrawerSetting);                
            }

            if (member.Name == "ShowInWeb") attributes.Add(new ButtonAttribute(ButtonSizes.Medium));
            if (member.Name == "Update") attributes.Add(new ButtonAttribute(ButtonSizes.Medium));
            if (member.Name == "RemoveEmpty") attributes.Add(new ButtonAttribute(ButtonSizes.Medium));
        }
    }

    public class WorkSheetDrawer : OdinAttributeProcessor<WorkSheet>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            if (member.Name == "SpreadSheet") attributes.Add(new ReadOnlyAttribute());
            if (member.Name == "Title") attributes.Add(new DisplayAsStringAttribute());
            if (member.Name == "HeadLine") attributes.Add(new DisplayAsStringAttribute());
            if (member.Name == "RowMax") attributes.Add(new DisplayAsStringAttribute());
            if (member.Name == "ColumnMax") attributes.Add(new DisplayAsStringAttribute());

            if (member.Name == "Cells")
            {
                var listDrawerSetting = new ListDrawerSettingsAttribute()
                {
                    HideRemoveButton = true,
                    HideAddButton = true,
                    IsReadOnly = true,
                    Expanded = true
                };
                attributes.Add(listDrawerSetting);                
                attributes.Add(new PropertySpaceAttribute(10f, 0f));
            }

            if (member.Name == "ShowInWeb") attributes.Add(new ButtonAttribute(ButtonSizes.Medium));
            if (member.Name == "Update") attributes.Add(new ButtonAttribute(ButtonSizes.Medium));            
        }
    }

    public class WorkSheetRowGroupDrawer : OdinAttributeProcessor<WorkSheet.RowGroup>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            if (member.Name == "Index")
            {
                attributes.Add(new HorizontalGroupAttribute(0.1f));
                attributes.Add(new DisplayAsStringAttribute());
                attributes.Add(new HideLabelAttribute());
            }
            if (member.Name == "Cell")
            {
                var listDrawerSetting = new ListDrawerSettingsAttribute()
                {
                    HideRemoveButton = true,
                    HideAddButton = true,
                    IsReadOnly = true,
                };
                attributes.Add(listDrawerSetting);
                attributes.Add(new DisplayAsStringAttribute());
                attributes.Add(new HorizontalGroupAttribute());
            }
        }
    }

    public class SpreadSheetImporterDrawer : OdinAttributeProcessor<SpreadSheetImporter>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            if (member.Name == "clientInformation")
            {
                attributes.Add(new TitleGroupAttribute("Pre Requirement", "Client Information, url, and savePath"));
                attributes.Add(new InfoBoxAttribute("Google Client Information"));
                attributes.Add(new RequiredAttribute());
                attributes.Add(new PropertySpaceAttribute(0, 5));
            }
            if (member.Name == "savePath")
            {
                attributes.Add(new TitleGroupAttribute("Pre Requirement"));
                attributes.Add(new InfoBoxAttribute("SpreadSheet Create Target Folder. ex <b>Assets/Project/Data/Spreadsheets</b>"));
                attributes.Add(new RequiredAttribute());
                attributes.Add(new FolderPathAttribute());
                attributes.Add(new PropertySpaceAttribute(0, 5));
            }
            if (member.Name == "sheetID")
            {                
                attributes.Add(new TitleGroupAttribute("Pre Requirement"));
                attributes.Add(new InfoBoxAttribute("sheetURL after /d/ <b><color=yellow>ID</color></b> until /edit"));
                attributes.Add(new RequiredAttribute());
                attributes.Add(new PropertySpaceAttribute(0, 15));                
            }

            if (member.Name == "ignorePrefix")
            {
                attributes.Add(new TitleGroupAttribute("Generate Options", "worksheet ignore prefix"));
                attributes.Add(new RequiredAttribute());                
            }
            if (member.Name == "Generate")
            {
                attributes.Add(new TitleGroupAttribute("Generate Options"));
                attributes.Add(new EnableIfAttribute("@savePath != string.Empty && sheetID != string.Empty"));
                attributes.Add(new ButtonAttribute(ButtonSizes.Large));
            }
        }
    }
}