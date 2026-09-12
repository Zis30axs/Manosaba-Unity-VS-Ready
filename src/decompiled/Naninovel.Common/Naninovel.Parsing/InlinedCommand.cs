using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x200004D")]
public class InlinedCommand : ILineComponent, IGenericContent
{
	[Token(Token = "0x1700004A")]
	public Command Command
	{
		[Token(Token = "0x60001AC")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60001AB")]
	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
	public InlinedCommand(Command command)
	{
	}

	[Token(Token = "0x60001AD")]
	[Address(RVA = "0x10E2D50", Offset = "0x10E1750", VA = "0x1810E2D50", Slot = "3")]
	public override string ToString()
	{
		return null;
	}
}
