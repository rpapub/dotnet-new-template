#nullable enable
using System;
using System.Activities;
using System.ComponentModel;
using Cpmf.CustomActivities.Models;

namespace Cpmf.CustomActivities.Activities
{
    [Category("Cpmf.CustomActivities")]
    [DisplayName("Read Document")]
    [Description("Reads a document from the vault. Returns a VaultDocument. Plus edition stub.")]
    public sealed class ReadDocument : CodeActivity<VaultDocument>
    {
        [RequiredArgument]
        [Category("Input")]
        [DisplayName("Document ID")]
        [Description("Unique identifier of the document to read from the vault.")]
        public InArgument<string>? DocumentId { get; set; }

        [Category("Input")]
        [DisplayName("Vault Path")]
        [Description("Vault path where the document is stored. Leave empty to use the root path.")]
        public InArgument<string>? VaultPath { get; set; }

        protected override VaultDocument Execute(CodeActivityContext context)
        {
            throw new NotImplementedException(
                "Cpmf.CustomActivities Plus edition: ReadDocument is not yet implemented.");
        }
    }
}
