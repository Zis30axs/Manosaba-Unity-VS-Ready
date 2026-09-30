using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x20000AF")]
    internal class Equal : IBinaryOperator
    {
    	[Token(Token = "0x4000220")]
    	private const double tolerance = 1E-05;

    	[Token(Token = "0x60003BD")]
    	[Address(RVA = "0x11003B0", Offset = "0x10FEDB0", VA = "0x1811003B0", Slot = "4")]
    	public IOperand Operate(IOperand lhs, IOperand rhs)
    	{
    		return null;
    	}

    	[Token(Token = "0x60003BE")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public Equal()
    	{
    	}
    }
}
