using System;
namespace CPMForge.Template.Exceptions
{
    public sealed class VaultTimeoutException : Exception
    {
        public VaultTimeoutException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
