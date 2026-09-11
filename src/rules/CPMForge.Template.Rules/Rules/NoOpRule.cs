// <copyright file="NoOpRule.cs" company="CPMForge">
// Copyright (c) 2026 Christian Prior-Mamulyan. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>

using UiPath.Studio.Activities.Api.Analyzer.Rules;
using UiPath.Studio.Analyzer.Models;

using WatchfulAnvil.Sdk.Core;

namespace CPMForge.Template.Rules.Rules
{
    /// <summary>
    /// Always-passing placeholder rule. Proves the Workflow Analyzer registration
    /// pipeline end-to-end. Replace with real library usage-enforcement rules.
    /// </summary>
    public class NoOpRule : ActivityRule
    {
        protected override string Id => "CPMFT-000";

        protected override string Name => "No-Op Placeholder Rule";

        protected override string Recommendation =>
            "Placeholder rule — always passes. Replace with a real CPMForge.Template usage-enforcement rule.";

        protected override InspectionResult Inspect(IActivityModel activity, Rule rule)
            => Pass();
    }
}
