using System;
using System.Collections.Generic;
using System.Reflection;

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using Wayway.Engine;
using Wayway.Engine.Pool;

namespace AttributionDrawer
{
    public class PoolManagerDrawer : OdinAttributeProcessor<PoolManager>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            if (member.Name == "poolingComponents")
            {
                attributes.Add(new PropertySpaceAttribute(0, 15f));
            }

            if (member.Name == "dummyPool")
            {
                attributes.Add(new TitleGroupAttribute("Dummy", "miss poolType Components"));
                attributes.Add(new HideLabelAttribute());
            }
        }
    }

    public class PoolableDrawer : OdinAttributeProcessor<Poolable>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            if (member.Name == "preventPool")
            {
                attributes.Add(new HideInInlineEditorsAttribute());
            }
            if (member.Name == "iD")
            {
                attributes.Add(new HideLabelAttribute());
                attributes.Add(new DisplayAsStringAttribute());
                attributes.Add(new HorizontalGroupAttribute("Attribution", 0.3f));                
            }

            if (member.Name == "type")
            {
                attributes.Add(new HideLabelAttribute());
                attributes.Add(new HorizontalGroupAttribute("Attribution"));
            }

            if (member.Name == "poolCount")
            {
                attributes.Add(new HideLabelAttribute());
                attributes.Add(new HorizontalGroupAttribute("Attribution", 0.25f));
                attributes.Add(new SuffixLabelAttribute("count", true));
                attributes.Add(new PropertySpaceAttribute(0, 15));
            }

            if (member.Name == "GenerateID")
            {
                attributes.Add(new HideInInlineEditorsAttribute());
                attributes.Add(new ButtonAttribute(ButtonSizes.Medium));                
            }

            if (member.Name == "OnDrawed")
            {
                attributes.Add(new HideInInlineEditorsAttribute());
                attributes.Add(new TabGroupAttribute("OnDrawed"));
                attributes.Add(new PropertyOrderAttribute(.9f));
            }
            if (member.Name == "OnReturned")
            {
                attributes.Add(new HideInInlineEditorsAttribute());
                attributes.Add(new TabGroupAttribute("OnReturned"));
                attributes.Add(new PropertyOrderAttribute(1f));
            }
        }
    }    

    public class PoolingComponentDrawer : OdinAttributeProcessor<PoolChannel>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            if (member.Name == "targetField")
            {
                attributes.Add(new PropertySpaceAttribute(0, 15f));
            }

            if (member.Name == "poolTable")
            {
                attributes.Add(new ShowInInspectorAttribute());
            }
        }
    }
}


