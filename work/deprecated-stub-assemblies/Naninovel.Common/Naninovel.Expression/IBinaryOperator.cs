using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x20000B2")]
    public interface IBinaryOperator
    {
    	[Token(Token = "0x60003C3")]
    	IOperand Operate(IOperand lhs, IOperand rhs);
    }
}
