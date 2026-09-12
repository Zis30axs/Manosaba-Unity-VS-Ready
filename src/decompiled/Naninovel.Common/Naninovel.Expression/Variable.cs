using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x20000BE")]
public class Variable : IExpression
{
	[Token(Token = "0x170000E8")]
	public string Name
	{
		[Token(Token = "0x60003DA")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60003D9")]
	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
	public Variable(string name)
	{
	}
}
