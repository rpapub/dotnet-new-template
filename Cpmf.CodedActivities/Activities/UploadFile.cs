#nullable enable
using System;
using System.Activities;
using System.ComponentModel;

namespace Cpmf.CodedActivities.Activities
{
    [Category("Cpmf.CodedActivities")]
    [DisplayName("Upload File")]
    [Description("Uploads a local file to a remote path. Plus edition stub.")]
    public sealed class UploadFile : CodeActivity
    {
        [Category("Input")]
        [DisplayName("Local Path")]
        public InArgument<string> LocalPath { get; set; } = new();

        [Category("Input")]
        [DisplayName("Remote Path")]
        public InArgument<string> RemotePath { get; set; } = new();

        [Category("Output")]
        [DisplayName("Status")]
        public OutArgument<OperationStatus> Status { get; set; } = new();

        protected override void Execute(CodeActivityContext context)
        {
            throw new NotImplementedException(
                "Cpmf.CodedActivities Plus edition: UploadFile is not yet implemented.");
        }
    }
}
