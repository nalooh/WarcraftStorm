using System.Security.Cryptography;
using WarcraftStorm.Data.Realms;
using WarcraftStorm.AuthServer.Network;
using WarcraftStorm.Network;

namespace WarcraftStorm.AuthServer.Packets;

internal class SERVER_AUTH_SUCCESS(AuthConnection connection) : IServerPacket
{
    public byte[] GetData()
    {
        MemoryStream ms = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(ms);
        WritePacketData(writer);
        ms.Seek(0, SeekOrigin.Begin);
        return ms.ToArray();
    }

    protected void WritePacketData(BinaryWriter writer)
    {
        writer.Write((byte)AuthCommands.AUTH_LOGON_CHALLENGE);
        writer.Write((byte)0);
        writer.Write((byte)AuthResult.SUCCESS);
        connection.Account.SecureRemotePassword.CalculateX(connection.Account.Password);
        if (connection.Account.Salt == null || connection.Account.Salt.Length == 0 || connection.Account.Verifier == null || connection.Account.Verifier.Length == 0)
        {
            connection.Account.Salt = connection.Account.SecureRemotePassword.Salt;
            connection.Account.Verifier = connection.Account.SecureRemotePassword.V;

            RealmsDbContext db = connection.Db;
            var account = db.Accounts.Find(connection.Account.Id);
            account.Salt = connection.Account.Salt;
            account.Verifier = connection.Account.Verifier;
            db.SaveChanges();
        }
        byte[] array = new byte[16];
        RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(array);
        writer.Write(connection.Account.SecureRemotePassword.B);
        writer.Write((byte)1);
        writer.Write(connection.Account.SecureRemotePassword.g.ToByteArray()[0]);
        writer.Write((byte)32);
        writer.Write(connection.Account.SecureRemotePassword.N);
        writer.Write(connection.Account.Salt);
        writer.Write(array);
        writer.Write((byte)0);
    }
}
