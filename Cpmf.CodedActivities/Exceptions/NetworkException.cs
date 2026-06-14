using System;
namespace Cpmf.CodedActivities.Exceptions
{
    public sealed class NetworkException : Exception
    {
        public NetworkException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
