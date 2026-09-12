using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x20000B3")]
public interface IUnaryOperator
{
	[Token(Token = "0x60003C4")]
	IOperand Operate(IOperand operand);
}
