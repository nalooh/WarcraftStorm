using System.Net.Sockets;
using WarcraftStorm.Data.Realms;
using WarcraftStorm.Network;

namespace WarcraftStorm.GameServer.Network;

internal class GameConnectionFactory(ILogger<GameConnection> logger, RealmsDbContext db) : IConnectionFactory
{
    public Connection CreateConnection(TcpClient client)
    {
        return new GameConnection(logger, client, new GamePacketHandler(logger), db);
    }
}
