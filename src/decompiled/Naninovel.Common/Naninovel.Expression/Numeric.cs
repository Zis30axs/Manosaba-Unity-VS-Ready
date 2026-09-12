using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x20000A5")]
public class Numeric : IOperand, IExpression
{
	[Token(Token = "0x170000DE")]
	public double Value
	{
		[Token(Token = "0x60003A3")]
		[Address(RVA = "0x10FF8A0", Offset = "0x10FE2A0", VA = "0x1810FF8A0")]
		[CompilerGenerated]
		get
		{
			return default(double);
		}
	}

	[Token(Token = "0x60003A2")]
	[Address(RVA = "0x10FF890", Offset = "0x10FE290", VA = "0x1810FF890")]
	public Numeric(double value)
	{
	}

	[Token(Token = "0x60003A4")]
	[Address(RVA = "0x10FF8B0", Offset = "0x10FE2B0", VA = "0x1810FF8B0", Slot = "4")]
	public object GetValue()
	{
		return null;
	}
}
