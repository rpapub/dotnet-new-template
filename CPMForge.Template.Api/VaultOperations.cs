#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using CPMForge.Template.Models;

namespace CPMForge.Template.Api
{
    public static class VaultOperations
    {
        public static Task<VaultDocument> ReadDocumentAsync(
            this IVaultSessionHandle handle,
            string documentId,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException("CPMForge.Template: ReadDocumentAsync not yet implemented.");

        public static Task<OperationResult> WriteDocumentAsync(
            this IVaultSessionHandle handle,
            string documentId,
            string content,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException("CPMForge.Template: WriteDocumentAsync not yet implemented.");

        public static Task<bool> DocumentExistsAsync(
            this IVaultSessionHandle handle,
            string documentId,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException("CPMForge.Template: DocumentExistsAsync not yet implemented.");
    }
}
