namespace WarcraftStorm.Network;

public interface IClientPacket
{
    void ReadData();
    void Execute();

}
