using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x20000A7")]
public class String : IOperand, IExpression
{
	[Token(Token = "0x170000DF")]
	public string Value
	{
		[Token(Token = "0x60003A8")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60003A7")]
	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
	public String(string value)
	{
	}

	[Token(Token = "0x60003A9")]
	[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
	public object GetValue()
	{
		return null;
	}
}
