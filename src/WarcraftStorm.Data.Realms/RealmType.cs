using System;

namespace WarcraftStorm.Data.Realms
{
	public enum RealmType : byte
	{
		Normal,
		PVP,
		RP = 6,
		RPPVP = 8
	}
}