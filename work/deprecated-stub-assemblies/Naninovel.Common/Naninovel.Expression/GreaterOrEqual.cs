using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x20000B1")]
    internal class GreaterOrEqual : IBinaryOperator
    {
    	[Token(Token = "0x4000221")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly Equal eq;

    	[Token(Token = "0x60003C1")]
    	[Address(RVA = "0x11009E0", Offset = "0x10FF3E0", VA = "0x1811009E0", Slot = "4")]
    	public IOperand Operate(IOperand lhs, IOperand rhs)
    	{
    		return null;
    	}

    	[Token(Token = "0x60003C2")]
    	[Address(RVA = "0x1100CA0", Offset = "0x10FF6A0", VA = "0x181100CA0")]
    	public GreaterOrEqual()
    	{
    	}
    }
}
