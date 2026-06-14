#nullable enable
namespace Cpmf.CustomActivities.Models
{
    public sealed class VaultOptions
    {
        public string   Host     { get; init; } = "";
        public AuthMode AuthMode { get; init; } = AuthMode.Password;
    }
}
