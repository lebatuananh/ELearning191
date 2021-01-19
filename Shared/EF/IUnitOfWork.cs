using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.EF
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
