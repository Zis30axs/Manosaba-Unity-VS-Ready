using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x20000B8")]
internal class NegateNumeric : IUnaryOperator
{
	[Token(Token = "0x60003CD")]
	[Address(RVA = "0x1101610", Offset = "0x1100010", VA = "0x181101610", Slot = "4")]
	public IOperand Operate(IOperand operand)
	{
		return null;
	}

	[Token(Token = "0x60003CE")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public NegateNumeric()
	{
	}
}
