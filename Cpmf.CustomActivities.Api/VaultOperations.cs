#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using Cpmf.CustomActivities.Models;

namespace Cpmf.CustomActivities.Api
{
    public static class VaultOperations
    {
        public static Task<VaultDocument> ReadDocumentAsync(
            this IVaultSessionHandle handle,
            string documentId,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException("Cpmf.CustomActivities: ReadDocumentAsync not yet implemented.");

        public static Task<OperationResult> WriteDocumentAsync(
            this IVaultSessionHandle handle,
            string documentId,
            string content,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException("Cpmf.CustomActivities: WriteDocumentAsync not yet implemented.");

        public static Task<bool> DocumentExistsAsync(
            this IVaultSessionHandle handle,
            string documentId,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException("Cpmf.CustomActivities: DocumentExistsAsync not yet implemented.");
    }
}
