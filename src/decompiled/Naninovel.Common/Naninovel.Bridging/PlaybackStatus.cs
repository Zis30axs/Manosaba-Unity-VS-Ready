using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Bridging;

[Token(Token = "0x20000CE")]
public struct PlaybackStatus
{
	[Token(Token = "0x170000F6")]
	public bool Playing
	{
		[Token(Token = "0x6000421")]
		[Address(RVA = "0x486730", Offset = "0x485130", VA = "0x180486730")]
		[CompilerGenerated]
		readonly get
		{
			return default(bool);
		}
		[Token(Token = "0x6000422")]
		[Address(RVA = "0x10E7EF0", Offset = "0x10E68F0", VA = "0x1810E7EF0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170000F7")]
	public PlaybackSpot PlayedSpot
	{
		[Token(Token = "0x6000423")]
		[Address(RVA = "0x456320", Offset = "0x454D20", VA = "0x180456320")]
		[CompilerGenerated]
		readonly get
		{
			return default(PlaybackSpot);
		}
		[Token(Token = "0x6000424")]
		[Address(RVA = "0x1104DC0", Offset = "0x11037C0", VA = "0x181104DC0")]
		[CompilerGenerated]
		set
		{
		}
	}
}
