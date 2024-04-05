using WarcraftStorm.Data.Realms;
using WarcraftStorm.AuthServer.Network;
using WarcraftStorm.Network;

namespace WarcraftStorm.AuthServer.Packets
{
	public class CLIENT_AUTH_PROOF(Connection connection) : IClientPacket
	{
		private class PacketData
		{
			public byte[] A { get; set; }
			public byte[] M1 { get; set; }
			public byte[] crc_hash { get; set; }
			public byte number_of_keys { get; set; }
			public byte securityFlags { get; set; }
		}

		private PacketData data = new();

		public void ReadData(Stream stream)
		{
            BinaryReader reader = new(stream);
			data.A = reader.ReadBytes(32);
			data.M1 = reader.ReadBytes(20);
			data.crc_hash = reader.ReadBytes(20);
			data.number_of_keys = reader.ReadByte();
			data.securityFlags = reader.ReadByte();
		}

		public void Execute()
		{
            AuthConnection authConnection = (AuthConnection)connection;
			RealmsDbContext db = authConnection.Db;
			authConnection.Account.SecureRemotePassword.CalculateU(data.A);
			authConnection.Account.SecureRemotePassword.CalculateM2(data.M1);
			authConnection.SessionKey = BitConverter.ToString(authConnection.Account.SecureRemotePassword.K).ToLower().Replace("-", "");
			authConnection.Account.SessionKey = authConnection.Account.SecureRemotePassword.K;

			var account = db.Accounts.Find(authConnection.Account.Id);
			account.SessionKey = authConnection.Account.SecureRemotePassword.K;

			db.AccountSessions.Add(
                new AccountSession
                {
                    AccountId = authConnection.Account.Id,
                    IP = authConnection.ClientAddress,
                    Start = System.DateTime.Now
                });
			db.SaveChanges();
			
			authConnection.State = AuthConnectionState.Authorised;
			SERVER_AUTH_PROOF packet = new SERVER_AUTH_PROOF(authConnection);
			connection.SendPacket(packet);
		}
	}
}