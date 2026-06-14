#nullable enable
using System.Threading;
using System.Threading.Tasks;

namespace Cpmf.CustomActivities.Api
{
    public interface IVaultService
    {
        Task<IVaultSessionHandle> OpenSessionAsync(
            string host, CancellationToken cancellationToken = default);
    }
}
