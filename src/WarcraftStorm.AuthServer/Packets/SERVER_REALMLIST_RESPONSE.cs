using WarcraftStorm.Data.Realms;
using WarcraftStorm.AuthServer.Network;
using WarcraftStorm.Network;
using System.Text;

namespace WarcraftStorm.AuthServer.Packets;

internal class SERVER_REALMLIST_RESPONSE(AuthConnection connection) : IServerPacket
{
    private readonly RealmsDbContext db = connection.Db;

    public byte[] GetData()
    {
        MemoryStream ms = new();
        BinaryWriter writer = new(ms);
        writer.Write((byte)AuthCommands.REALM_LIST);
        byte[] data = WritePacketData();
        writer.Write((UInt16)data.Length);
        writer.Write(data);
        ms.Seek(0, SeekOrigin.Begin);
        return ms.ToArray();
    }

    protected byte[] WritePacketData()
    {
        MemoryStream ms = new();
        BinaryWriter writer = new(ms);
        writer.Write((UInt32)0);
        writer.Write((byte)db.Realms.Count());
        foreach (Realm current in db.Realms)
        {
            writer.Write((UInt32)(byte)current.Type);
            writer.Write((byte)current.Flags);
            WriteCString(current.Name, writer);
            WriteCString(current.AddressString, writer);
            writer.Write((float)0f);
            writer.Write((byte)db.Characters.Where(ch => ch.AccountId == connection.Account.Id).Count());
            writer.Write((byte)current.Timezone);
            writer.Write((byte)0);
        }
        writer.Write((UInt16)0x0002);
        return ms.ToArray();
    }

    protected void WriteCString(string text, BinaryWriter writer)
    {
        writer.Write(Encoding.ASCII.GetBytes(text));
        writer.Write((byte)0);
    }
}
