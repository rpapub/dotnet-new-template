#nullable enable
namespace Cpmf.CodedActivities
{
    public abstract class AuthMethod { }

    public sealed class PrivateKeyAuth : AuthMethod
    {
        public string Username { get; init; } = "";
        public string KeyFilePath { get; init; } = "";
        public string Passphrase { get; init; } = "";
    }
}
