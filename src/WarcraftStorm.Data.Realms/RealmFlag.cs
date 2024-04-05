using System;

namespace WarcraftStorm.Data.Realms
{
	[Flags]
	public enum RealmFlag : byte
	{
		REALM_FLAG_NONE = 0,
		REALM_FLAG_INVALID = 1,
		REALM_FLAG_OFFLINE = 2,
		REALM_FLAG_SPECIFYBUILD = 4,
		REALM_FLAG_UNK1 = 8,
		REALM_FLAG_UNK2 = 16,
		REALM_FLAG_NEW_PLAYERS = 32,
		REALM_FLAG_RECOMMENDED = 64,
		REALM_FLAG_FULL = 128
	}
}