#nullable enable
// Inlined from Community.Activities (MIT). Adds ContinueOnError wrapping to AsyncTaskNativeActivity.
using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;

namespace CPMForge.Template.Shared
{
    public abstract class ContinuableAsyncNativeActivity : AsyncTaskNativeActivity
    {
        public virtual InArgument<bool> ContinueOnError { get; set; } = new(false);

        protected sealed override async Task<Action<NativeActivityContext>?> InternalExecuteAsync(
            NativeActivityContext context, CancellationToken cancellationToken)
        {
            try
            {
                return await ExecuteAsync(context, cancellationToken);
            }
            catch (Exception) when (ContinueOnError.Get(context))
            {
                return null;
            }
        }
    }
}
