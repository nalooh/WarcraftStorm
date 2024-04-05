using Microsoft.Extensions.Logging;

namespace WarcraftStorm.Network;

public abstract class PacketHandler<TOpCode>(ILogger logger) : IPacketHandler
where TOpCode : struct
{
    protected readonly Dictionary<TOpCode, Type> _packetTypes = [];

    public void HandlePacket(Connection connection)
    {
        TOpCode opcode = GetOpCodeFromStream(connection.Stream);
        if(!_packetTypes.ContainsKey(opcode))
        {
            logger.LogError("[{client}] UNKNOWN PACKET 0x{opcode:X}", connection.ClientAddress, opcode);
            return;
        }
        ExecutePacketType(_packetTypes[opcode], connection);
    }

    protected abstract TOpCode GetOpCodeFromStream(Stream stream);

    protected void ExecutePacketType(Type packetType, Connection connection)    
    {
        var constructor = packetType.GetConstructor([ connection.GetType() ]);
        IClientPacket packet = (IClientPacket)constructor.Invoke([ connection ]);
        connection.ReceivePacket(packet);
    }

}
