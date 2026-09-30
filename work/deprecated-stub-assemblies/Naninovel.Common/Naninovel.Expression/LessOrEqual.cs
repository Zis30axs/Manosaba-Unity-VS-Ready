using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x20000B5")]
    internal class LessOrEqual : IBinaryOperator
    {
    	[Token(Token = "0x4000222")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly Equal eq;

    	[Token(Token = "0x60003C7")]
    	[Address(RVA = "0x1100F70", Offset = "0x10FF970", VA = "0x181100F70", Slot = "4")]
    	public IOperand Operate(IOperand lhs, IOperand rhs)
    	{
    		return null;
    	}

    	[Token(Token = "0x60003C8")]
    	[Address(RVA = "0x1101230", Offset = "0x10FFC30", VA = "0x181101230")]
    	public LessOrEqual()
    	{
    	}
    }
}
