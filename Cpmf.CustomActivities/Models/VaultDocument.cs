#nullable enable
using System;

namespace Cpmf.CustomActivities.Models
{
    public sealed class VaultDocument
    {
        public string Id              { get; init; } = "";
        public string Content         { get; init; } = "";
        public string MimeType        { get; init; } = "";
        public DateTimeOffset ModifiedAt { get; init; }
    }
}
