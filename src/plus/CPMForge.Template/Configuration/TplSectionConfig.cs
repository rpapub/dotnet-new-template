#nullable enable

using System.Collections.Generic;
using CPMForge.Config.Binding;

namespace CPMForge.Template.Configuration
{
    /// <summary>The TplSection library's config section. Replace the example key with the library's own.</summary>
    /// <remarks>
    /// Leave a key out rather than writing an empty value: an empty value is a value, and the library
    /// treats it differently from a missing one.
    /// </remarks>
    [ConfigSection("TplSection", SchemaVersion = 1)]
    public sealed class TplSectionConfig
    {
        /// <summary>Example key: whether the library's activities do anything. Replace or remove.</summary>
        public bool Enabled { get; set; } = true;

        /// <summary>Names of config fields overwritten with library-owned values by
        /// <c>GetEffectiveTplSectionConfig</c>. Set by code, not read from config.</summary>
        [ConfigRuntime]
        public List<string> AppliedImmutables { get; set; } = new();

        /// <summary>Folder of the packaged library. Set by code, not read from config.</summary>
        [ConfigRuntime]
        public string LibraryRoot { get; set; } = "";
    }
}
