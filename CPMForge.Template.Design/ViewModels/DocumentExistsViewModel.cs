#nullable enable
using System.Activities.DesignViewModels;

namespace CPMForge.Template.Design.ViewModels
{
    public class DocumentExistsViewModel : DesignPropertiesViewModel
    {
        public DesignInArgument<string> DocumentId { get; set; } = null!;

        public DocumentExistsViewModel(IDesignServices services) : base(services) { }

        protected override void InitializeModel()
        {
            base.InitializeModel();

            DocumentId.DisplayName = "Document ID";
            DocumentId.Tooltip     = "Unique identifier of the document to look up in the vault.";
            DocumentId.IsRequired  = true;
            DocumentId.IsPrincipal = true;
            DocumentId.OrderIndex  = 0;
        }
    }
}
