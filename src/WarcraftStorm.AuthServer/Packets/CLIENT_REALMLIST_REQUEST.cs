using WarcraftStorm.AuthServer.Network;
using WarcraftStorm.Network;

namespace WarcraftStorm.AuthServer.Packets;

public class CLIENT_REALMLIST_REQUEST(Connection connection) : IClientPacket
{
    public void ReadData(Stream stream)
    {
        stream.ReadByte();
        stream.ReadByte();
        stream.ReadByte();
        stream.ReadByte();
    }

    public void Execute()
    {
        AuthConnection authConnection = (AuthConnection)connection;
        if (authConnection.State != AuthConnectionState.Authorised || authConnection.Account == null)
            return;

        SERVER_REALMLIST_RESPONSE packet = new(authConnection);
        connection.SendPacket(packet);
    }

}
