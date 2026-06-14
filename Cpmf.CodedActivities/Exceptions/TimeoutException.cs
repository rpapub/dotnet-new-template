using System;
namespace Cpmf.CodedActivities.Exceptions
{
    public sealed class TimeoutException : Exception
    {
        public TimeoutException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
