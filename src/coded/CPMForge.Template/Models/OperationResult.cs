#nullable enable
using System;

namespace CPMForge.Template.Models
{
    public sealed record OperationResult
    {
        public bool     Success     { get; init; }
        public string?  Message     { get; init; }
        public int      StatusCode  { get; init; }
        public TimeSpan Elapsed     { get; init; }
        public DateTime CompletedAt { get; init; }
        public string?  Error       { get; init; }

        public static OperationResult Ok(int code, TimeSpan elapsed, string? msg = null) => new()
        {
            Success     = true,
            StatusCode  = code,
            Elapsed     = elapsed,
            CompletedAt = DateTime.UtcNow,
            Message     = msg,
        };

        public static OperationResult Fail(int code, string error, TimeSpan elapsed) => new()
        {
            Success     = false,
            StatusCode  = code,
            Elapsed     = elapsed,
            CompletedAt = DateTime.UtcNow,
            Error       = error,
        };
    }
}
