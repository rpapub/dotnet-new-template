using System;
using System.Activities;
using System.Collections.Generic;
using UiPath.CodedWorkflows;

[assembly: CodedWorkflowsServiceRegistry(typeof(CPMForge.Template.Api.VaultRegistry))]

namespace CPMForge.Template.Api
{
    public class VaultRegistry : ICodedWorkflowsServiceRegistry
    {
        public IDictionary<string, Type> AutoImportedTypes
            => new Dictionary<string, Type> { ["VaultOperations"] = typeof(VaultOperations) };

        public IEnumerable<string> AutoImportedNamespaces
            => new[] { "CPMForge.Template.Api", "CPMForge.Template.Models" };

        public void Register(ICodedWorkflowsServiceLocator locator, ActivityContext context)
        {
            // Register IVaultService for coded workflow injection:
            // locator.RegisterInstance<IVaultService>(new VaultService());
        }
    }
}
