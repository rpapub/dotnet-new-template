#nullable enable
using System.Activities.DesignViewModels;
using System.Collections.Generic;
using CPMForge.Template.Configuration;

namespace CPMForge.Template.Design.ViewModels
{
    public class GetEffectiveTplSectionConfigViewModel : DesignPropertiesViewModel
    {
        public DesignInArgument<TplSectionConfig> Config { get; set; } = null!;
        public DesignOutArgument<TplSectionConfig> Payload { get; set; } = null!;
        public DesignOutArgument<Dictionary<string, object>> Result { get; set; } = null!;
        public DesignOutArgument<Dictionary<string, object>> Status { get; set; } = null!;

        public GetEffectiveTplSectionConfigViewModel(IDesignServices services) : base(services) { }

        protected override void InitializeModel()
        {
            base.InitializeModel();
            PersistValuesChangedDuringInit();

            Config.DisplayName = "Config";
            Config.Tooltip     = "The process's TplSection section. Leave empty to use the defaults shipped with the library.";
            Config.IsPrincipal = true;
            Config.OrderIndex  = 0;

            Payload.DisplayName = "Payload";
            Payload.Tooltip     = "The effective config. Pass it on to the other TplSection activities.";
            Payload.OrderIndex  = 1;

            Result.DisplayName = "Result";
            Result.Tooltip     = "The effective values, flat, for the caller's log.";
            Result.OrderIndex  = 2;

            Status.DisplayName = "Status";
            Status.Tooltip     = "Execution metadata: success flag, source, applied immutables.";
            Status.OrderIndex  = 3;
        }
    }
}
