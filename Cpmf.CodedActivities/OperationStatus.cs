#nullable enable
namespace Cpmf.CodedActivities
{
    public sealed class OperationStatus
    {
        public bool IsSuccess { get; init; }
        public string? FailureReason { get; init; }
        public string? ExceptionType { get; init; }
    }
}
