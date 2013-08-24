using System;
using System.Threading;

namespace TenSecondHero.Core
{
    public class SyncContext : SynchronizationContext
    {
        Action<SendOrPostCallback, object> _post;

        private SyncContext(Action<SendOrPostCallback, object> post = null)
        {
            _post = post;
        }

        public static SynchronizationContext Create(Action<SendOrPostCallback, object> post)
        {
            return new SyncContext(post);
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            if (_post != null)
                _post(d, state);
            else
                base.Post(d, state);
        }
    }
}
