using System;
namespace Cpmf.CodedActivities.Exceptions
{
    public sealed class PathNotFoundException : Exception
    {
        public PathNotFoundException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
