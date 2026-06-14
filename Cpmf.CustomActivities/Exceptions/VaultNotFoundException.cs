using System;
namespace Cpmf.CustomActivities.Exceptions
{
    public sealed class VaultNotFoundException : Exception
    {
        public VaultNotFoundException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
