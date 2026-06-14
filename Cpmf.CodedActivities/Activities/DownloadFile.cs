#nullable enable
using System;
using System.Activities;
using System.ComponentModel;

namespace Cpmf.CodedActivities.Activities
{
    [Category("Cpmf.CodedActivities")]
    [DisplayName("Download File")]
    [Description("Downloads a remote file to a local directory. Plus edition stub.")]
    public sealed class DownloadFile : CodeActivity
    {
        [Category("Input")]
        [DisplayName("Remote Path")]
        public InArgument<string> RemotePath { get; set; } = new();

        [Category("Input")]
        [DisplayName("Local Directory")]
        public InArgument<string> LocalDirectory { get; set; } = new();

        [Category("Output")]
        [DisplayName("Status")]
        public OutArgument<OperationStatus> Status { get; set; } = new();

        protected override void Execute(CodeActivityContext context)
        {
            throw new NotImplementedException(
                "Cpmf.CodedActivities Plus edition: DownloadFile is not yet implemented.");
        }
    }
}
