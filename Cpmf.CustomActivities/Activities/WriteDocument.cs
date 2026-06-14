#nullable enable
using System;
using System.Activities;
using System.ComponentModel;
using Cpmf.CustomActivities.Models;

namespace Cpmf.CustomActivities.Activities
{
    [Category("Cpmf.CustomActivities")]
    [DisplayName("Write Document")]
    [Description("Writes a document to the vault. Plus edition stub.")]
    public sealed class WriteDocument : CodeActivity
    {
        [RequiredArgument]
        [Category("Input")]
        [DisplayName("Document ID")]
        [Description("Unique identifier to assign to the document when writing to the vault.")]
        public InArgument<string>? DocumentId { get; set; }

        [Category("Input")]
        [DisplayName("Content")]
        [Description("Text content to write to the vault document.")]
        public InArgument<string>? Content { get; set; }

        [Category("Output")]
        [DisplayName("Result")]
        [Description("Operation result containing success status and any error details.")]
        public OutArgument<OperationResult>? Result { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            throw new NotImplementedException(
                "Cpmf.CustomActivities Plus edition: WriteDocument is not yet implemented.");
        }
    }
}
