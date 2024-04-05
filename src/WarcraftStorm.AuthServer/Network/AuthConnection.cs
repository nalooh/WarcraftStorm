using System.Net.Sockets;
using WarcraftStorm.Data.Realms;
using WarcraftStorm.Network;

namespace WarcraftStorm.AuthServer.Network;

internal class AuthConnection(ILogger logger, TcpClient client, IPacketHandler packetHandler, RealmsDbContext db) : Connection(logger, client, packetHandler)
{
    public RealmsDbContext Db { get; private set; } = db;

    public AuthConnectionState State { get; internal set; } = AuthConnectionState.Connected;

    public Account Account { get; internal set; }

    public string SessionKey { get; set; }

}
