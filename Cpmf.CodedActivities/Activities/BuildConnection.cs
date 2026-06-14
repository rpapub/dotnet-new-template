#nullable enable
using System;
using System.Activities;
using System.ComponentModel;

namespace Cpmf.CodedActivities.Activities
{
    [Category("Cpmf.CodedActivities")]
    [DisplayName("Build Connection")]
    [Description("Assembles a connection object and makes it available to child activities via context.Properties. Plus edition stub.")]
    public sealed class BuildConnection : NativeActivity
    {
        [Category("Connection")]
        [DisplayName("Server")]
        public InArgument<string> Server { get; set; } = new();

        [Category("Connection")]
        [DisplayName("Port")]
        public InArgument<int> Port { get; set; } = new InArgument<int>(22);

        [Category("Auth")]
        [DisplayName("Username")]
        public InArgument<string> Username { get; set; } = new();

        [Category("Auth")]
        [DisplayName("Key File Path")]
        public InArgument<string> KeyFilePath { get; set; } = new();

        [Browsable(false)]
        public Activity? Body { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            throw new NotImplementedException(
                "Cpmf.CodedActivities Plus edition: BuildConnection is not yet implemented.");
        }

        protected override bool CanInduceIdle => false;
    }
}
