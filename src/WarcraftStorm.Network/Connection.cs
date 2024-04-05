using System.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace WarcraftStorm.Network;

public class Connection(ILogger logger, TcpClient client, IPacketHandler packetHandler) : IDisposable
{
    public string ClientAddress => String.Format("{0}", client.Client.RemoteEndPoint);

    internal NetworkStream Stream { get; } = client.GetStream();

    protected IPacketHandler PacketHandler { get; } = packetHandler;

    public void Run(object? state)
    {
        while (client.Connected)
        {
            PacketHandler.HandlePacket(this);
            Thread.Sleep(1);
        }
    }

    public void ReceivePacket(IClientPacket packet)
    {
        logger.LogInformation("[{client}] RECEIVED {packet}", ClientAddress, packet);
        packet.ReadData(Stream);
        packet.Execute();
    }

    public virtual void SendPacket(IServerPacket packet)
    {
        logger.LogInformation("[{client}] SENDING {packet}", ClientAddress, packet);
        byte[] data = packet.GetData();

        BinaryWriter binaryWriter = new(Stream);
        binaryWriter.Write(data, 0, data.Length);
    }

     public void Dispose()
    {
        client.Close();
    }

}
