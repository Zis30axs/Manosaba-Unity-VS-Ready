using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x20000B9")]
internal class NotEqual : IBinaryOperator
{
	[Token(Token = "0x4000223")]
	private const double tolerance = 1E-05;

	[Token(Token = "0x60003CF")]
	[Address(RVA = "0x1101740", Offset = "0x1100140", VA = "0x181101740", Slot = "4")]
	public IOperand Operate(IOperand lhs, IOperand rhs)
	{
		return null;
	}

	[Token(Token = "0x60003D0")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public NotEqual()
	{
	}
}
