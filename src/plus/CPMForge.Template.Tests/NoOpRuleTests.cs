using System.Diagnostics;
using CPMForge.Template.Rules.Rules;
using UiPath.Studio.Activities.Api;
using UiPath.Studio.Activities.Api.Analyzer;
using UiPath.Studio.Activities.Api.Analyzer.Rules;
using UiPath.Studio.Analyzer.Models;
using Xunit;

namespace CPMForge.Template.Tests
{
    public class NoOpRuleTests
    {
        private readonly NoOpRule _rule = new NoOpRule();

        [Fact]
        public void Initialize_RegistersRule_WithCorrectId()
        {
            var api = new FakeAnalyzerConfigurationService();
            _rule.Initialize(api);
            Assert.Single(api.Rules, r => r.Id == "CPMFT-000" && r.DefaultErrorLevel == TraceLevel.Error);
        }

        [Fact]
        public void Inspect_AlwaysPasses()
        {
            var configured = _rule.Get();
            var result = configured.Inspect(null!, configured);
            Assert.False(result.HasErrors);
        }

        private sealed class FakeAnalyzerConfigurationService : IAnalyzerConfigurationService
        {
            public System.Collections.Generic.List<Rule> Rules { get; } = new();

            public void AddRule<T>(Rule<T> rule) where T : IInspectionObject => Rules.Add(rule);

            public bool HasFeature(string featureKey) => true;

            public void AddCounter<T>(Counter<T> counter) where T : IInspectionObject { }

            public void AddMetadata<T>(Metadata<T> metadata) where T : IInspectionObject { }

            public string ActiveProfile => "Studio";
        }
    }
}
