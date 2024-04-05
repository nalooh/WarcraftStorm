using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarcraftStorm.Data.Realms
{
	[Table("Realms", Schema = "Realms")]
	public class Realm
	{
		[Key, Column("RealmID")]
		public int Id { get; protected set; }

		[Column("RealmName")]
		public string Name { get; protected set; }

		[Column("RealmAddress")]
		public string Address { get; protected set; }

		[Column("RealmPort")]
		public short Port { get; protected set; }

		[Column("RealmType")]
		public RealmType Type { get; protected set; }

		[Column("RealmFlags")]
		public RealmFlag Flags { get; protected set; }

		[Column("RealmTimezone")]
		public byte Timezone { get; protected set; }

		[NotMapped]
		public string AddressString
		{
			get
			{
				return String.Format("{0}:{1}", this.Address, this.Port);
			}
		}
	}
}