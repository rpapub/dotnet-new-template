using System.Activities.Presentation.Metadata;
using Cpmf.CustomActivities.Activities;

namespace Cpmf.CustomActivities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.AddCustomAttributes(typeof(WithVaultSession),
                new System.ComponentModel.DesignerAttribute(typeof(WithVaultSessionDesigner)));
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
