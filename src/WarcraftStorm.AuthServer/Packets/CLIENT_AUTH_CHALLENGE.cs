using WarcraftStorm.AuthServer.Network;
using WarcraftStorm.Network;

namespace WarcraftStorm.AuthServer.Packets;

public class CLIENT_AUTH_CHALLENGE(Connection connection) : IClientPacket
{
    private class DATA
    {
        public byte Error;
        public short Size;
        public string GameName;
        public byte Version1;
        public byte Version2;
        public byte Version3;
        public ushort Build;
        public string Platform;
        public string OS;
        public string Country;
        public uint Timezone;
        public byte[] IP;
        public string UserName;

        public byte Unused1;
        public byte Unused2;
        public byte Unused3;
    }

    private DATA data = new();

    public void ReadData(Stream stream)
    {
        BinaryReader reader = new(stream);
        data.Error = reader.ReadByte();
        data.Size = reader.ReadInt16();
        data.GameName = new string(reader.ReadChars(3));
        data.Unused1 = reader.ReadByte();
        data.Version1 = reader.ReadByte();
        data.Version2 = reader.ReadByte();
        data.Version3 = reader.ReadByte();
        data.Build = reader.ReadUInt16();
        data.Platform = new string(reader.ReadChars(3).Reverse().ToArray());
        data.Unused2 = reader.ReadByte();
        data.OS =  new string(reader.ReadChars(3).Reverse().ToArray());
        data.Unused3 = reader.ReadByte();
        data.Country = new string(reader.ReadChars(4));
        data.Timezone = reader.ReadUInt32();
        data.IP = reader.ReadBytes(4);
        byte userNameLength = reader.ReadByte();
        data.UserName = new string(reader.ReadChars(userNameLength));
    }

    public void Execute()
    {
        AuthConnection authConnection = (AuthConnection)connection;
        var account = authConnection.Db.Accounts.Where(item => item.UserName == data.UserName).FirstOrDefault();

        // Account doesnt exists
        if (account == null)
        {
            SERVER_AUTH_FAIL packet = new(authConnection, AuthResult.FAIL_UNKNOWN_ACCOUNT);
            connection.SendPacket(packet);
            connection.Dispose();
            return;
        }

        // Account is locked
        if(account.IsLocked)
        {
            SERVER_AUTH_FAIL packet = new(authConnection, AuthResult.FAIL_LOCKED);
            connection.SendPacket(packet);
            connection.Dispose();
            return;
        }

        // Account is OK
        authConnection.Account = new Account
        {
            Id = account.Id,
            SessionKey = account.SessionKey,
            Password = account.Password,
            Salt = account.Salt,
            Verifier = account.Verifier
        };

        SERVER_AUTH_SUCCESS packet2 = new(authConnection);
        connection.SendPacket(packet2);
    }

}
