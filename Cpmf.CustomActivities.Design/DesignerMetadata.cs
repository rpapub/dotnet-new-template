using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using Cpmf.CustomActivities.Activities;

namespace Cpmf.CustomActivities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.AddCustomAttributes(typeof(WithVaultSession),
                new DesignerAttribute(typeof(WithVaultSessionDesigner)));
            builder.AddCustomAttributes(typeof(DocumentExists),
                new DesignerAttribute(typeof(DocumentExistsDesigner)));
            builder.AddCustomAttributes(typeof(ReadDocument),
                new DesignerAttribute(typeof(ReadDocumentDesigner)));
            builder.AddCustomAttributes(typeof(WriteDocument),
                new DesignerAttribute(typeof(WriteDocumentDesigner)));
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
