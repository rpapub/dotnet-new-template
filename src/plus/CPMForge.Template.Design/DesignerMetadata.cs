using System.Activities.Presentation.Metadata;

namespace CPMForge.Template.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            // Register custom ActivityDesigners here, e.g. for a NativeActivity scope with a WorkflowItemPresenter:
            // builder.AddCustomAttributes(typeof(MyScope), new DesignerAttribute(typeof(MyScopeDesigner)));
            var builder = new AttributeTableBuilder();
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
