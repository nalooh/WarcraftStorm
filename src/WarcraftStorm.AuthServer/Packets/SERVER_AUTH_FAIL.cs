using WarcraftStorm.AuthServer.Network;
using WarcraftStorm.Network;

namespace WarcraftStorm.AuthServer.Packets;

internal class SERVER_AUTH_FAIL(AuthConnection connection, AuthResult result) : IServerPacket
{
    public byte[] GetData()
    {
        MemoryStream ms = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(ms);
        writer.Write((byte)AuthCommands.AUTH_LOGON_CHALLENGE);
        writer.Write((byte)0);
        writer.Write((byte)result);
        ms.Seek(0, SeekOrigin.Begin);
        return ms.ToArray();
    }

}
