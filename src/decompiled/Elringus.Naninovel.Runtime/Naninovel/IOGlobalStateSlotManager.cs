using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000475")]
public class IOGlobalStateSlotManager : IOSaveSlotManager<GlobalStateMap>
{
	[Token(Token = "0x40010A2")]
	[FieldOffset(Offset = "0x60")]
	private readonly string defaultSlotId;

	[Token(Token = "0x40010A3")]
	[FieldOffset(Offset = "0x68")]
	private readonly string savesFolderPath;

	[Token(Token = "0x40010A4")]
	[FieldOffset(Offset = "0x70")]
	private readonly StateConfiguration config;

	[Token(Token = "0x17000311")]
	protected override string SaveDataPath
	{
		[Token(Token = "0x60015F4")]
		[Address(RVA = "0x5DB120", Offset = "0x5D9B20", VA = "0x1805DB120", Slot = "37")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000312")]
	protected override string Extension
	{
		[Token(Token = "0x60015F5")]
		[Address(RVA = "0x5DB180", Offset = "0x5D9B80", VA = "0x1805DB180", Slot = "32")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000313")]
	protected override bool Binary
	{
		[Token(Token = "0x60015F6")]
		[Address(RVA = "0x5DB1F0", Offset = "0x5D9BF0", VA = "0x1805DB1F0", Slot = "31")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x60015F7")]
	[Address(RVA = "0x5DB210", Offset = "0x5D9C10", VA = "0x1805DB210")]
	public IOGlobalStateSlotManager(StateConfiguration config = default, string savesFolderPath = default)
	{
	}

	[Token(Token = "0x60015F8")]
	[Address(RVA = "0x5DB370", Offset = "0x5D9D70", VA = "0x1805DB370", Slot = "27")]
	public override bool AnySaveExists()
	{
		return default(bool);
	}
}
