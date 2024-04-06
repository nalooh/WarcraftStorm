using WarcraftStorm.Network;

namespace WarcraftStorm.GameServer.Network;

internal class GamePacketHandler : PacketHandler<byte>
{
    public GamePacketHandler(ILogger logger) : base(logger)
    {
    }

    protected override byte GetOpCodeFromStream(Stream stream)
    {
        return (byte)stream.ReadByte();
    }

}
