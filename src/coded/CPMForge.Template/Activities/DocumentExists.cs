#nullable enable
using System;
using System.Activities;
using System.ComponentModel;

namespace CPMForge.Template.Activities
{
    [Category("CPMForge.Template")]
    [DisplayName("Document Exists")]
    [Description("Returns true if the document exists in the vault. Plus edition stub.")]
    public sealed class DocumentExists : CodeActivity<bool>
    {
        [RequiredArgument]
        [Category("Input")]
        [DisplayName("Document ID")]
        [Description("Unique identifier of the document to look up in the vault.")]
        public InArgument<string>? DocumentId { get; set; }

        protected override bool Execute(CodeActivityContext context)
        {
            throw new NotImplementedException(
                "CPMForge.Template Plus edition: DocumentExists is not yet implemented.");
        }
    }
}
