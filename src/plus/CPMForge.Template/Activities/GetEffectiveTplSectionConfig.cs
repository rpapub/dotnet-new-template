#nullable enable
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using CPMForge.Template.Configuration;

namespace CPMForge.Template.Activities
{
    [Category("CPMForge.Template.Activities")]
    [DisplayName("Get Effective TplSection Config")]
    [Description("Turns the process's TplSection section into the config every other TplSection activity trusts: library-owned values enforced, allowed values checked. Pass Nothing to use the shipped defaults.")]
    public sealed class GetEffectiveTplSectionConfig : CodeActivity
    {
        [Category("Input")]
        [DisplayName("Config")]
        [Description("The process's TplSection section (the property of its config root typed TplSectionConfig). Leave empty to use the defaults shipped with the library.")]
        public InArgument<TplSectionConfig>? Config { get; set; }

        [Category("Output")]
        [DisplayName("Payload")]
        [Description("The effective config. Pass it on to the other TplSection activities.")]
        public OutArgument<TplSectionConfig>? Payload { get; set; }

        [Category("Output")]
        [DisplayName("Result")]
        [Description("The effective values, flat, for the caller's log.")]
        public OutArgument<Dictionary<string, object>>? Result { get; set; }

        [Category("Output")]
        [DisplayName("Status")]
        [Description("Execution metadata: success flag, source, applied immutables.")]
        public OutArgument<Dictionary<string, object>>? Status { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var libraryRoot = EffectiveTplSectionConfig.ContentFolderOf(typeof(GetEffectiveTplSectionConfig).Assembly.Location);
            var fromCaller = Config?.Get(context);
            var config = EffectiveTplSectionConfig.Resolve(fromCaller, libraryRoot);

            Payload?.Set(context, config);
            Result?.Set(context, EffectiveTplSectionConfig.ToResult(config));
            Status?.Set(context, new Dictionary<string, object>
            {
                { "isSuccess",         true },
                { "source",            fromCaller == null ? "shipped defaults" : "caller section" },
                { "appliedImmutables", string.Join(",", config.AppliedImmutables) },
                { "libraryRoot",       libraryRoot },
            });
        }
    }
}
