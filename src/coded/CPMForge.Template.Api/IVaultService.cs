#nullable enable
using System.Threading;
using System.Threading.Tasks;

namespace CPMForge.Template.Api
{
    public interface IVaultService
    {
        Task<IVaultSessionHandle> OpenSessionAsync(
            string host, CancellationToken cancellationToken = default);
    }
}
