using System.IO;
using CPMForge.Template.Configuration;
using Xunit;

namespace CPMForge.Template.Tests
{
    public class EffectiveTplSectionConfigTests
    {
        [Fact]
        public void Resolve_NoSectionAndNoDefaultsFile_UsesCodeDefaults()
        {
            var emptyRoot = Directory.CreateTempSubdirectory().FullName;

            var config = EffectiveTplSectionConfig.Resolve(null, emptyRoot);

            Assert.True(config.Enabled);
            Assert.Equal(emptyRoot, config.LibraryRoot);
        }

        [Fact]
        public void Resolve_SectionFromCaller_IsKept()
        {
            var config = EffectiveTplSectionConfig.Resolve(new TplSectionConfig { Enabled = false }, Path.GetTempPath());

            Assert.False(config.Enabled);
        }

        [Fact]
        public void LoadDefaults_ReadsShippedToml()
        {
            var root = Directory.CreateTempSubdirectory().FullName;
            Directory.CreateDirectory(Path.Combine(root, "Config"));
            File.WriteAllText(Path.Combine(root, "Config", "TplSection.default.toml"), "Enabled = false\n");

            var config = EffectiveTplSectionConfig.LoadDefaults(root);

            Assert.False(config.Enabled);
        }
    }
}
