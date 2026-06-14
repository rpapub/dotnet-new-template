#nullable enable
namespace Cpmf.CodedActivities
{
    public sealed class ConnectionOptions
    {
        public const string ContextKey = "Cpmf.CodedActivities.Connection";

        public string Server { get; init; } = "";
        public int Port { get; init; } = 22;
        public AuthMethod Auth { get; init; } = null!;
        public ResilienceOptions Resilience { get; init; } = new();
    }
}
