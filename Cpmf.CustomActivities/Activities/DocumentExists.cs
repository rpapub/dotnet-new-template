#nullable enable
using System;
using System.Activities;
using System.ComponentModel;

namespace Cpmf.CustomActivities.Activities
{
    [Category("Cpmf.CustomActivities")]
    [DisplayName("Document Exists")]
    [Description("Returns true if the document exists in the vault. Plus edition stub.")]
    public sealed class DocumentExists : CodeActivity<bool>
    {
        [RequiredArgument]
        [Category("Input")]
        [DisplayName("Document ID")]
        public InArgument<string>? DocumentId { get; set; }

        protected override bool Execute(CodeActivityContext context)
        {
            throw new NotImplementedException(
                "Cpmf.CustomActivities Plus edition: DocumentExists is not yet implemented.");
        }
    }
}
