using System;
using System.Threading;

namespace Engine.Core {

    // We roll our own class here because the builtin Task<TResult> is one-time-use
    // We re-use these constantly, so the STL isn't viable
    public sealed class Task {

        public Task(Func<bool> _underlying) {
            underlying = _underlying;
        }

        private Func<bool> underlying;

        // Doesn't end up creating race conditions because of how our TaskScheduler works
        // The TaskScheduler ensures that end_invoke() is always finished before another begin_invoke() is started (except for errors)
        private IAsyncResult result;

        public void begin_invoke() {
            result = underlying.BeginInvoke(null, null);
        }

        private delegate bool underlying_runner();

        public bool succeeded {
            get => underlying.EndInvoke(result);
        }

    }

}
