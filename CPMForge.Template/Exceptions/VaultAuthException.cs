using System;
namespace CPMForge.Template.Exceptions
{
    public sealed class VaultAuthException : Exception
    {
        public VaultAuthException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
