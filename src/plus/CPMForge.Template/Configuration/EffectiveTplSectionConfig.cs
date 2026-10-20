#nullable enable

using System.Collections.Generic;
using System.IO;
using CPMForge.Config;
using CPMForge.Config.Binding;

namespace CPMForge.Template.Configuration
{
    /// <summary>The logic of Get Effective TplSection Config, kept out of the activity so tests can use it
    /// without loading System.Activities.</summary>
    public static class EffectiveTplSectionConfig
    {
        /// <summary>Turns the section the process passed (or nothing) into the config the library trusts.</summary>
        public static TplSectionConfig Resolve(TplSectionConfig? fromCaller, string libraryRoot)
        {
            // Nothing from the caller: the shipped defaults file if the package carries one, else code defaults.
            var config = fromCaller ?? LoadDefaults(libraryRoot);
            config.LibraryRoot = libraryRoot;

            // Library-owned values go back to their code default.
            config.AppliedImmutables = new List<string>(ImmutableDefaults.Apply(config));

            // Allowed values: one error naming every key, the bad value and the allowed set.
            ConfigSectionValidator.ThrowIfInvalid(config);
            return config;
        }

        /// <summary>The effective values, flat, for the caller's log. Never include secrets, only whether they are set.</summary>
        public static Dictionary<string, object> ToResult(TplSectionConfig config) => new()
        {
            { "Enabled", config.Enabled },
        };

        // lib/net.../X.dll -> two levels up -> content. Falls back to the current folder when there is no content/.
        public static string ContentFolderOf(string assemblyLocation)
        {
            if (!string.IsNullOrEmpty(assemblyLocation))
            {
                var dir = Path.GetDirectoryName(assemblyLocation) ?? string.Empty;
                var content = Path.GetFullPath(Path.Combine(dir, "..", "..", "content"));
                if (Directory.Exists(content)) return content;
            }
            return Directory.GetCurrentDirectory();
        }

        public static TplSectionConfig LoadDefaults(string libraryRoot)
        {
            var path = Path.Combine(libraryRoot, "Config", "TplSection.default.toml");
            return File.Exists(path) ? ConfigBind.FromToml<TplSectionConfig>(path) : new TplSectionConfig();
        }
    }
}
