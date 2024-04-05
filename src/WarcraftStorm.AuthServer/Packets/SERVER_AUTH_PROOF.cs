using WarcraftStorm.AuthServer.Network;
using WarcraftStorm.Network;

namespace WarcraftStorm.AuthServer.Packets
{
	internal class SERVER_AUTH_PROOF(AuthConnection connection) : IServerPacket
	{
          public byte[] GetData()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(ms);
            WritePacketData(writer);
            ms.Seek(0, SeekOrigin.Begin);
            return ms.ToArray();
        }

		protected void WritePacketData(BinaryWriter writer)
		{
			writer.Write((byte)AuthCommands.AUTH_LOGON_PROOF);
			writer.Write((byte)0);
			writer.Write(connection.Account.SecureRemotePassword.M2);
			//WriteUInt32(8388608u);
			writer.Write((UInt32)0);
			//WriteUInt16(0);
		}
	}
}