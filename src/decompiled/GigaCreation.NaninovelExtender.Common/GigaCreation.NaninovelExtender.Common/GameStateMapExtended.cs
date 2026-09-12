using System;
using System.Runtime.CompilerServices;
using GigaCreation.Essentials.SaveLoad;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Common;

[Serializable]
[Token(Token = "0x2000025")]
public class GameStateMapExtended : GameStateMap
{
	[Token(Token = "0x1700000B")]
	public VersionNumber VersionNumber
	{
		[Token(Token = "0x600005D")]
		[Address(RVA = "0xCFF250", Offset = "0xCFDC50", VA = "0x180CFF250")]
		[CompilerGenerated]
		get
		{
			return default(VersionNumber);
		}
		[Token(Token = "0x600005E")]
		[Address(RVA = "0xCFF270", Offset = "0xCFDC70", VA = "0x180CFF270")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x600005F")]
	[Address(RVA = "0x5DA960", Offset = "0x5D9360", VA = "0x1805DA960")]
	public GameStateMapExtended()
	{
	}
}
