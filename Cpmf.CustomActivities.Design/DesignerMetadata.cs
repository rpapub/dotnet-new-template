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
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
