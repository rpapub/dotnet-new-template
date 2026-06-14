#nullable enable
using System;
using System.Activities;
using System.ComponentModel;

namespace Cpmf.CodedActivities.Activities
{
    [Category("Cpmf.CodedActivities")]
    [DisplayName("Probe Connection")]
    [Description("Tests connectivity without throwing on failure. Sets IsReachable in Result. Plus edition stub.")]
    public sealed class ProbeConnection : CodeActivity
    {
        [Category("Output")]
        [DisplayName("Result")]
        public OutArgument<OperationStatus> Result { get; set; } = new();

        protected override void Execute(CodeActivityContext context)
        {
            throw new NotImplementedException(
                "Cpmf.CodedActivities Plus edition: ProbeConnection is not yet implemented.");
        }
    }
}
