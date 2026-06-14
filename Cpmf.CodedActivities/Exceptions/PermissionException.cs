using System;
namespace Cpmf.CodedActivities.Exceptions
{
    public sealed class PermissionException : Exception
    {
        public PermissionException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
