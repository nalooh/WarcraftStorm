using WarcraftStorm.AuthServer.Cryptography;

namespace WarcraftStorm.AuthServer.Network;

public class Account
{
    public int Id { get; set; }
    public byte[] SessionKey { get; set; }
    public SRP6 SecureRemotePassword { get; protected set; } = new SRP6();
    internal byte[] Password { get; set; }
    internal byte[] Salt { get; set; }
    internal byte[] Verifier { get; set; }
}
