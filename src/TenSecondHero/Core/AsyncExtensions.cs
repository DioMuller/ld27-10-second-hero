using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace TenSecondHero.Core
{
    public static class AsyncExtensions
    {
        public static TaskAwaiter GetAwaiter(this CancellationToken cancellation)
        {
            return cancellation.AsTask().GetAwaiter();
        }

        public static Task AsTask(this CancellationToken cancellation)
        {
            var tcs = new TaskCompletionSource<bool>();
            cancellation.Register(delegate
            {
                tcs.TrySetResult(true);
            });
            return tcs.Task.ContinueWith(t => { });
        }

        public static IDisposable AsDefault(this SynchronizationContext context)
        {
            var oldContext = System.Threading.SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(context);

            return Disposable.Create(delegate
            {
                SynchronizationContext.SetSynchronizationContext(oldContext);
            });
        }

        public static async Task<T> EventAsync<T>(Action<EventHandler<T>> sign, Action<EventHandler<T>> unassign) where T : EventArgs
        {
            var tcs = new TaskCompletionSource<T>();

            EventHandler<T> completed = (s, e) =>
            {
                tcs.TrySetResult(e);
            };
            sign(completed);
            var res = await tcs.Task;
            unassign(completed);
            return res;
        }
    }
}
