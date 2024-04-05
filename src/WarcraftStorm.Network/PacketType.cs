namespace WarcraftStorm.Network;

public class PacketType
{
    public static readonly PacketType Server = new("S");

    public static readonly PacketType Client = new("C");

    public string Name { get; }

    private PacketType(string name)
    {
        Name = name;
    }
    
}
