using System;
namespace Cpmf.CodedActivities.Exceptions
{
    public sealed class AuthException : Exception
    {
        public AuthException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
