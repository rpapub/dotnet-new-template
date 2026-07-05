#nullable enable
// Inlined from Community.Activities (MIT). Provides async execution via WF4 bookmarks + CancellationToken.
using System;
using System.Activities;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace CPMForge.Template.Shared
{
    public abstract class AsyncTaskNativeActivity : NativeActivity
    {
        private readonly Variable<NoPersistHandle> _noPersistHandle = new();

        protected override bool CanInduceIdle => true;

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddImplementationVariable(_noPersistHandle);
        }

        // Override point for ContinuableAsyncNativeActivity to inject ContinueOnError wrapping.
        protected virtual Task<Action<NativeActivityContext>?> InternalExecuteAsync(
            NativeActivityContext context, CancellationToken cancellationToken)
            => ExecuteAsync(context, cancellationToken);

        // Subclasses implement this. Return an action to run synchronously on the WF thread after the
        // async work completes (e.g. schedule the body via ctx.ScheduleAction), or return null.
        protected abstract Task<Action<NativeActivityContext>?> ExecuteAsync(
            NativeActivityContext context, CancellationToken cancellationToken);

        protected sealed override void Execute(NativeActivityContext context)
        {
            _noPersistHandle.Get(context).Enter(context);
            var cts      = new CancellationTokenSource();
            var bookmark = context.CreateBookmark(OnBookmarkResumed);
            InternalExecuteAsync(context, cts.Token)
                .ContinueWith(t => context.ResumeBookmark(bookmark, t),
                              TaskScheduler.Default);
        }

        private void OnBookmarkResumed(NativeActivityContext context, Bookmark bookmark, object value)
        {
            _noPersistHandle.Get(context).Exit(context);
            var task = (Task<Action<NativeActivityContext>?>)value;
            if (task.IsFaulted)
                ExceptionDispatchInfo.Capture(
                    task.Exception?.InnerException ?? task.Exception!).Throw();
            task.Result?.Invoke(context);
        }
    }
}
