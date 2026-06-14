using System;
using Cpmf.CustomActivities.Exceptions;

namespace Cpmf.CustomActivities
{
    public static class ErrorClassifier
    {
        public static bool IsRetryable(Exception ex) => ex switch
        {
            VaultTimeoutException  => true,
            VaultAuthException     => false,
            VaultNotFoundException => false,
            ArgumentException         => false,
            InvalidOperationException => false,
            _                         => false
        };
    }
}
