using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.ArabicSupport;

[Token(Token = "0x20004EC")]
public struct TashkeelLocation
{
	[Token(Token = "0x1700037B")]
	public char Tashkeel
	{
		[Token(Token = "0x6001885")]
		[Address(RVA = "0x4364B0", Offset = "0x434EB0", VA = "0x1804364B0")]
		[CompilerGenerated]
		readonly get
		{
			return default(char);
		}
		[Token(Token = "0x6001886")]
		[Address(RVA = "0x4364C0", Offset = "0x434EC0", VA = "0x1804364C0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x1700037C")]
	public int Position
	{
		[Token(Token = "0x6001887")]
		[Address(RVA = "0x4364D0", Offset = "0x434ED0", VA = "0x1804364D0")]
		[CompilerGenerated]
		readonly get
		{
			return default(int);
		}
		[Token(Token = "0x6001888")]
		[Address(RVA = "0x4364E0", Offset = "0x434EE0", VA = "0x1804364E0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x6001889")]
	[Address(RVA = "0x608620", Offset = "0x607020", VA = "0x180608620")]
	public TashkeelLocation(TashkeelCharacters tashkeel, int position)
	{
	}
}
