using WarcraftStorm.AuthServer.Packets;
using WarcraftStorm.Network;

namespace WarcraftStorm.AuthServer.Network;

internal class AuthPacketHandler : PacketHandler<AuthCommands>
{
    public AuthPacketHandler(ILogger logger) : base(logger)
    {
        _packetTypes.Add(AuthCommands.AUTH_LOGON_CHALLENGE, typeof(CLIENT_AUTH_CHALLENGE));
        _packetTypes.Add(AuthCommands.AUTH_LOGON_PROOF, typeof(CLIENT_AUTH_PROOF));
    }

    protected override AuthCommands GetOpCodeFromStream(Stream stream)
    {
        return (AuthCommands)(byte)stream.ReadByte();
    }

}
