// <copyright file="RegisterAnalyzerConfiguration.cs" company="CPMForge">
// Copyright (c) 2026 Christian Prior-Mamulyan. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>

using CPMForge.Template.Rules.Rules;

using UiPath.Studio.Activities.Api;
using UiPath.Studio.Activities.Api.Analyzer;

namespace CPMForge.Template.Rules
{
    public sealed class RegisterAnalyzerConfiguration : IRegisterAnalyzerConfiguration
    {
        public void Initialize(IAnalyzerConfigurationService api)
        {
            new NoOpRule().Initialize(api);
        }
    }
}
