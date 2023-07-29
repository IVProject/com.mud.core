using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mud
{
    public interface IAsyncResourceLoader<TResult> : IDisposable
    {
        Task<TResult> LoadAsync(string address, CancellationToken cancellationToken = default);
    }
}