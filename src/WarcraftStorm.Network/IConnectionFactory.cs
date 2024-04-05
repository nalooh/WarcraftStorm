using System.Net.Sockets;

namespace WarcraftStorm.Network;

public interface IConnectionFactory
{
    Connection CreateConnection(TcpClient client);
    
}
