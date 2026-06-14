using System.Activities.Presentation.Metadata;
using Cpmf.CodedActivities.Activities;

namespace Cpmf.CodedActivities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.AddCustomAttributes(typeof(BuildConnection),
                new System.ComponentModel.DesignerAttribute(typeof(BuildConnectionDesigner)));
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
