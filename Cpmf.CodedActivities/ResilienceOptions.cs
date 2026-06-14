#nullable enable
namespace Cpmf.CodedActivities
{
    public sealed class ResilienceOptions
    {
        public int ConnectTimeoutSeconds { get; init; } = 30;
        public int OperationTimeoutSeconds { get; init; } = 120;
        public int RetriesNumber { get; init; } = 5;
        public int RetriesIntervalSeconds { get; init; } = 8;
    }
}
