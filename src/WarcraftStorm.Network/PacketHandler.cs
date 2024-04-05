using Microsoft.Extensions.Logging;

namespace WarcraftStorm.Network;

public abstract class PacketHandler<TOpcode>(ILogger logger) : IPacketHandler
{
    private readonly Dictionary<TOpcode, Type> _packetTypes = [];

    public void HandlePacket(Connection connection)
    {
        TOpcode opcode = GetOpCodeFromStream(connection.Stream);
        if(!_packetTypes.ContainsKey(opcode))
        {
            logger.LogError("[{client}] UNKNOWN PACKET 0x{opcode:X2}", connection.ClientAddress, opcode);
            return;
        }
        ExecutePacketType(_packetTypes[opcode], connection);
    }

    protected abstract TOpcode GetOpCodeFromStream(Stream stream);

    protected void ExecutePacketType(Type packetType, Connection connection)    
    {
        var constructor = packetType.GetConstructor([ connection.GetType() ]);
        IClientPacket packet = (IClientPacket)constructor.Invoke([ connection ]);
        connection.ReceivePacket(packet);
    }

}
