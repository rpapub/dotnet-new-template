using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using CPMForge.Template.Activities;

namespace CPMForge.Template.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.AddCustomAttributes(typeof(WithVaultSession),
                new DesignerAttribute(typeof(WithVaultSessionDesigner)));
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
