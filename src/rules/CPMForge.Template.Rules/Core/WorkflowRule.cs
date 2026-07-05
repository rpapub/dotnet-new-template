// <copyright file="WorkflowRule.cs" company="CPMForge">
// Copyright (c) 2026 Christian Prior-Mamulyan. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>

using UiPath.Studio.Analyzer.Models;

namespace CPMForge.Template.Rules.Core;

/// <summary>Base class for rules that inspect whole workflows.</summary>
public abstract class WorkflowRule : ScopedRule<IWorkflowModel>
{
}
