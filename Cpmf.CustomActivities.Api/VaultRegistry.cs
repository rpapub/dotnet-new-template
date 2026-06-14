using System;
using System.Activities;
using System.Collections.Generic;
using UiPath.CodedWorkflows;

[assembly: CodedWorkflowsServiceRegistry(typeof(Cpmf.CustomActivities.Api.VaultRegistry))]

namespace Cpmf.CustomActivities.Api
{
    public class VaultRegistry : ICodedWorkflowsServiceRegistry
    {
        public IDictionary<string, Type> AutoImportedTypes
            => new Dictionary<string, Type> { ["VaultOperations"] = typeof(VaultOperations) };

        public IEnumerable<string> AutoImportedNamespaces
            => new[] { "Cpmf.CustomActivities.Api", "Cpmf.CustomActivities.Models" };

        public void Register(ICodedWorkflowsServiceLocator locator, ActivityContext context)
        {
            // Register IVaultService for coded workflow injection:
            // locator.RegisterInstance<IVaultService>(new VaultService());
        }
    }
}
