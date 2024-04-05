namespace WarcraftStorm.Network;

public interface IClientPacket
{
    void ReadData(Stream stream);
    void Execute();

}
