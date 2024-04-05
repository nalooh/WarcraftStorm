using WarcraftStorm.Network;

namespace WarcraftStorm.AuthServer.Network;

internal class AuthPacketHandler : PacketHandler<byte>
{
    public AuthPacketHandler(ILogger logger) : base(logger)
    {
    }

    protected override byte GetOpCodeFromStream(Stream stream)
    {
        return (byte)stream.ReadByte();
    }

}
