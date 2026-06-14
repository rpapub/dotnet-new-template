#nullable enable
using System.Activities.DesignViewModels;

namespace Cpmf.CustomActivities.Design.ViewModels
{
    public class ReadDocumentViewModel : DesignPropertiesViewModel
    {
        public DesignInArgument<string> DocumentId { get; set; } = null!;
        public DesignInArgument<string> VaultPath  { get; set; } = null!;

        public ReadDocumentViewModel(IDesignServices services) : base(services) { }

        protected override void InitializeModel()
        {
            base.InitializeModel();

            DocumentId.DisplayName = "Document ID";
            DocumentId.Tooltip     = "Unique identifier of the document to read from the vault.";
            DocumentId.IsRequired  = true;
            DocumentId.IsPrincipal = true;
            DocumentId.OrderIndex  = 0;

            VaultPath.DisplayName = "Vault Path";
            VaultPath.Tooltip     = "Vault path where the document is stored. Leave empty to use the root path.";
            VaultPath.OrderIndex  = 1;
        }
    }
}
