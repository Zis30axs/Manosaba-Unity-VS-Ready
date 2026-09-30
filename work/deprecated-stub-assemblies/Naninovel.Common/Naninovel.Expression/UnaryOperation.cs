using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x20000AA")]
    public class UnaryOperation : IExpression
    {
    	[Token(Token = "0x170000E6")]
    	public IUnaryOperator Operator
    	{
    		[Token(Token = "0x60003B3")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170000E7")]
    	public IExpression Operand
    	{
    		[Token(Token = "0x60003B4")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60003B2")]
    	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
    	public UnaryOperation(IUnaryOperator op, IExpression operand)
    	{
    	}
    }
}
