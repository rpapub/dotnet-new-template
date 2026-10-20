using System;
using System.Activities;
using System.Collections.Generic;
using UiPath.CodedWorkflows;

[assembly: CodedWorkflowsServiceRegistry(typeof(CPMForge.Template.Api.CodedWorkflowsRegistry))]

namespace CPMForge.Template.Api
{
    /// <summary>What coded workflows see of this library. Add services and auto-imported types here.</summary>
    public class CodedWorkflowsRegistry : ICodedWorkflowsServiceRegistry
    {
        public IDictionary<string, Type> AutoImportedTypes
            => new Dictionary<string, Type>();

        public IEnumerable<string> AutoImportedNamespaces
            => new[] { "CPMForge.Template.Configuration" };

        public void Register(ICodedWorkflowsServiceLocator locator, ActivityContext context)
        {
            // Register services for coded workflow injection, e.g.:
            // locator.RegisterInstance<IMyService>(new MyService());
        }
    }
}
