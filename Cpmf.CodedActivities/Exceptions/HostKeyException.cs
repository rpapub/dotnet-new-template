using System;
namespace Cpmf.CodedActivities.Exceptions
{
    public sealed class HostKeyException : Exception
    {
        public HostKeyException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
