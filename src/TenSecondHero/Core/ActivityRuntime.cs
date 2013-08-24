using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TenSecondHero.Core
{
    class ActivityRuntime
    {
        System.Threading.SynchronizationContext _synchronizationContext;
        Queue<KeyValuePair<SendOrPostCallback, object>> _asyncQueue;

        public IActivity Level { get; private set; }

        public ActivityRuntime(IActivity level)
        {
            Level = level;
            _asyncQueue = new Queue<KeyValuePair<SendOrPostCallback, object>>();
            _synchronizationContext = SyncContext.Create((c, s) => _asyncQueue.Enqueue(new KeyValuePair<SendOrPostCallback, object>(c, s)));
        }

        public void RunEvents()
        {
            while (_asyncQueue.Count > 0)
            {
                var method = _asyncQueue.Dequeue();
                method.Key(method.Value);
            }
        }

        public IDisposable Activate()
        {
            return _synchronizationContext.AsDefault();
        }
    }
}
