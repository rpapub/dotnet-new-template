using System;
using Cpmf.CodedActivities.Exceptions;

namespace Cpmf.CodedActivities
{
    public static class ErrorClassifier
    {
        public static bool IsRetryable(Exception ex) => ex switch
        {
            Exceptions.TimeoutException  => true,
            Exceptions.NetworkException  => true,
            AuthException             => false,
            HostKeyException          => false,
            PermissionException       => false,
            PathNotFoundException     => false,
            ArgumentException         => false,
            InvalidOperationException => false,
            _                         => false
        };
    }
}
