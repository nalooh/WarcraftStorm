using System.Net.Sockets;
using WarcraftStorm.Network;

namespace WarcraftStorm.AuthServer.Network;

internal class AuthConnection(ILogger logger, TcpClient client, IPacketHandler packetHandler) : Connection(logger, client, packetHandler)
{
}
