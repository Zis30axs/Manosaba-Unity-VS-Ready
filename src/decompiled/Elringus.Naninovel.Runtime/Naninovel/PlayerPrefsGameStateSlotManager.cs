using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000478")]
public class PlayerPrefsGameStateSlotManager : PlayerPrefsSaveSlotManager<GameStateMap>
{
	[Token(Token = "0x40010A7")]
	[FieldOffset(Offset = "0x60")]
	private readonly string savesFolderPath;

	[Token(Token = "0x40010A8")]
	[FieldOffset(Offset = "0x68")]
	private readonly StateConfiguration config;

	[Token(Token = "0x1700031F")]
	protected override string KeyPrefix
	{
		[Token(Token = "0x6001628")]
		[Address(RVA = "0x5DB710", Offset = "0x5DA110", VA = "0x1805DB710", Slot = "35")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000320")]
	protected override bool Binary
	{
		[Token(Token = "0x6001629")]
		[Address(RVA = "0x5DB790", Offset = "0x5DA190", VA = "0x1805DB790", Slot = "31")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x600162A")]
	[Address(RVA = "0x5DB7B0", Offset = "0x5DA1B0", VA = "0x1805DB7B0")]
	public PlayerPrefsGameStateSlotManager(StateConfiguration config, string savesFolderPath)
	{
	}
}
