#nullable enable
using System.Activities.DesignViewModels;
using Cpmf.CustomActivities.Models;

namespace Cpmf.CustomActivities.Design.ViewModels
{
    public class WriteDocumentViewModel : DesignPropertiesViewModel
    {
        public DesignInArgument<string>       DocumentId { get; set; } = null!;
        public DesignInArgument<string>       Content    { get; set; } = null!;
        public DesignOutArgument<OperationResult> Result { get; set; } = null!;

        public WriteDocumentViewModel(IDesignServices services) : base(services) { }

        protected override void InitializeModel()
        {
            base.InitializeModel();

            DocumentId.DisplayName = "Document ID";
            DocumentId.Tooltip     = "Unique identifier to assign to the document when writing to the vault.";
            DocumentId.IsRequired  = true;
            DocumentId.IsPrincipal = true;
            DocumentId.OrderIndex  = 0;

            Content.DisplayName = "Content";
            Content.Tooltip     = "Text content to write to the vault document.";
            Content.OrderIndex  = 1;

            Result.DisplayName = "Result";
            Result.Tooltip     = "Operation result containing success status and any error details.";
            Result.OrderIndex  = 2;
        }
    }
}
