namespace WarcraftStorm.Network;

public abstract class Packet<TOpCode>(PacketType packetType, TOpCode opcode, Connection connection)
{
    public PacketType PacketType { get; } = packetType;

    public TOpCode OpCode { get; } = opcode;

    protected Connection Connection { get; } = connection;

    public override string ToString()
        => $"[{PacketType.Name}] 0x{OpCode:X2} {GetType().Name}";

}
