using Il2CppDummyDll;

namespace GigaCreation.Essentials.Platforms;

[Token(Token = "0x2000006")]
public static class Platform
{
	[Token(Token = "0x17000003")]
	public static PlatformKind CurrentPlatform
	{
		[Token(Token = "0x6000006")]
		[Address(RVA = "0xCD5B70", Offset = "0xCD4570", VA = "0x180CD5B70")]
		get
		{
			return default(PlatformKind);
		}
	}

	[Token(Token = "0x17000004")]
	public static bool IsPc
	{
		[Token(Token = "0x6000007")]
		[Address(RVA = "0xCD5C70", Offset = "0xCD4670", VA = "0x180CD5C70")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000005")]
	public static bool IsMobile
	{
		[Token(Token = "0x6000008")]
		[Address(RVA = "0xCD5CA0", Offset = "0xCD46A0", VA = "0x180CD5CA0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000006")]
	public static bool IsConsole
	{
		[Token(Token = "0x6000009")]
		[Address(RVA = "0xCD5CD0", Offset = "0xCD46D0", VA = "0x180CD5CD0")]
		get
		{
			return default(bool);
		}
	}
}
