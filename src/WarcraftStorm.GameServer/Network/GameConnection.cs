using System.Net.Sockets;
using WarcraftStorm.Data.Realms;
using WarcraftStorm.Network;

namespace WarcraftStorm.GameServer.Network;

internal class GameConnection(ILogger logger, TcpClient client, GamePacketHandler packetHandler, RealmsDbContext db) : Connection(logger, client, packetHandler)
{

}
