#nullable enable
using System.Activities;
using System.Activities.DesignViewModels;
using Cpmf.CustomActivities.Activities;
using Cpmf.CustomActivities.Models;

namespace Cpmf.CustomActivities.Design.ViewModels
{
    // Discovered by System.Activities.ViewModels framework via naming convention: {ActivityClass}ViewModel.
    // Handles the property panel. WithVaultSessionDesigner.xaml handles the canvas + body drop zone.
    public class WithVaultSessionViewModel : DesignPropertiesViewModel
    {
        public DesignInArgument<string>   Host     { get; set; } = null!;
        public DesignInArgument<AuthMode>  AuthMode { get; set; } = null!;
        // Body lives on the canvas (WorkflowItemPresenter) — hidden from property panel.
        public DesignProperty<ActivityAction<VaultSession>> Body { get; set; } = null!;

        public WithVaultSessionViewModel(IDesignServices services) : base(services) { }

        protected override void InitializeModel()
        {
            base.InitializeModel();

            Host.DisplayName = "Host";
            Host.Tooltip     = "Hostname or URL of the vault server (e.g. vault.example.com).";
            Host.IsRequired  = true;
            Host.IsPrincipal = true;
            Host.OrderIndex  = 0;

            AuthMode.DisplayName = "Auth Mode";
            AuthMode.Tooltip     = "Authentication method used to connect to the vault (e.g. Password, Token).";
            AuthMode.IsPrincipal = true;
            AuthMode.OrderIndex  = 1;

            Body.IsVisible = false;
        }
    }
}
