using System.Net.Sockets;
using WarcraftStorm.Data.Realms;
using WarcraftStorm.Network;

namespace WarcraftStorm.AuthServer.Network;

internal class AuthConnectionFactory(ILogger<AuthConnection> logger, RealmsDbContext db) : IConnectionFactory
{
    public Connection CreateConnection(TcpClient client)
    {
        return new AuthConnection(logger, client, new AuthPacketHandler(logger), db);
    }

}
