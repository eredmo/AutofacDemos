using System;
using System.Threading.Tasks;

namespace AD11.Infrastructure
{
    public interface IDispatcherHelper
    {
        Task RunAsync(Action action);
        Task<T> RunAsync<T>(Func<T> action);
    }
}
