using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarcraftStorm.Data.Realms
{
	[Table("AccountSessions", Schema = "Realms")]
	public class AccountSession
	{
		[Key, Column("AccountSessionID"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; protected set; }

		[Column("AccountID")]
		public int AccountId { get; set; }

		[Column("AccountSessionIP")]
		public string IP { get; set; }

		[Column("AccountSessionStart")]
		public DateTime Start { get; set; } = DateTime.Now;
	}
}