using System;
using CPMForge.Template.Exceptions;

namespace CPMForge.Template
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
