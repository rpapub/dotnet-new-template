// <copyright file="WithVaultSessionPresentRule.cs" company="CPMForge">
// Copyright (c) 2026 Christian Prior-Mamulyan. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>

using System.Linq;

using UiPath.Studio.Activities.Api.Analyzer.Rules;
using UiPath.Studio.Analyzer.Models;

using WatchfulAnvil.Sdk.Core;

namespace CPMForge.Template.Rules.Rules
{
    /// <summary>
    /// Fails if no workflow in the project contains a WithVaultSession activity.
    /// Enforces that vault operations are always wrapped in a session scope.
    /// </summary>
    public class WithVaultSessionPresentRule : ProjectRule
    {
        protected override string Id => "CPMFT-001";

        protected override string Name => "Vault Session Must Be Present";

        protected override string Recommendation =>
            "Wrap vault operations in a WithVaultSession activity to ensure session lifecycle is managed correctly.";

        protected override System.Diagnostics.TraceLevel DefaultSeverity
            => System.Diagnostics.TraceLevel.Warning;

        protected override InspectionResult Inspect(IProjectModel project, Rule rule)
        {
            var found = project.Workflows.Any(workflow =>
                workflow.Root?.Children?.Any(a =>
                {
                    var type = (a.Type ?? string.Empty).Split(',')[0].Trim();
                    return type.EndsWith(".WithVaultSession")
                        || a.DisplayName == "Vault Session";
                }) == true);

            return found
                ? Pass()
                : Fail(rule, "No WithVaultSession activity found in any workflow. Wrap vault calls in a Vault Session scope.");
        }
    }
}
