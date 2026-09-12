using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Bridging;

[Token(Token = "0x20000CB")]
public struct Message
{
	[Token(Token = "0x170000F0")]
	public string Id
	{
		[Token(Token = "0x6000415")]
		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
		[CompilerGenerated]
		readonly get
		{
			return null;
		}
		[Token(Token = "0x6000416")]
		[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170000F1")]
	public MessageType Type
	{
		[Token(Token = "0x6000417")]
		[Address(RVA = "0x55F3F0", Offset = "0x55DDF0", VA = "0x18055F3F0")]
		[CompilerGenerated]
		readonly get
		{
			return default(MessageType);
		}
		[Token(Token = "0x6000418")]
		[Address(RVA = "0xF9F160", Offset = "0xF9DB60", VA = "0x180F9F160")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170000F2")]
	public string? Payload
	{
		[Token(Token = "0x6000419")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		readonly get
		{
			return null;
		}
		[Token(Token = "0x600041A")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		[CompilerGenerated]
		set
		{
		}
	}
}
