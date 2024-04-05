namespace WarcraftStorm.Network;

public interface IPacketHandler
{
    void HandlePacket(Connection connection);

}
