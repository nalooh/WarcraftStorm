namespace WarcraftStorm.AuthServer.Network;

public enum AuthResult : byte
{
	SUCCESS = 0x00,
	FAIL_BANNED = 0x03,
	FAIL_UNKNOWN_ACCOUNT = 0x04,
	FAIL_INCORRECT_PASSWORD = 0x05,
	FAIL_ALREADY_ONLINE = 0x06,
	FAIL_VERSION_INVALID = 0x09,
	FAIL_LOCKED = 0x10,
}
