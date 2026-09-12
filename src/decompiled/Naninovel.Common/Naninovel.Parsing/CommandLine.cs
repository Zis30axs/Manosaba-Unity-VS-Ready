using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000045")]
public class CommandLine : IScriptLine
{
	[Token(Token = "0x1700003E")]
	public Command Command
	{
		[Token(Token = "0x6000193")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700003F")]
	public int Indent
	{
		[Token(Token = "0x6000194")]
		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x6000192")]
	[Address(RVA = "0x3A2CB0", Offset = "0x3A16B0", VA = "0x1803A2CB0")]
	public CommandLine(Command command, int indent = 0)
	{
	}

	[Token(Token = "0x6000195")]
	[Address(RVA = "0x10E2540", Offset = "0x10E0F40", VA = "0x1810E2540", Slot = "3")]
	public override string ToString()
	{
		return null;
	}
}
