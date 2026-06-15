#nullable enable
namespace CPMForge.Template.Models
{
    /// <summary>
    /// Represents an open Vault session injected into child activities via WithVaultSession.
    /// Replace with your real connection handle when implementing.
    /// </summary>
    public sealed class VaultSession
    {
        public string Host { get; init; } = "";
    }
}
