using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x20000A9")]
    public class TernaryOperation : IExpression
    {
    	[Token(Token = "0x170000E3")]
    	public IExpression Condition
    	{
    		[Token(Token = "0x60003AF")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170000E4")]
    	public IExpression Truthy
    	{
    		[Token(Token = "0x60003B0")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170000E5")]
    	public IExpression Falsy
    	{
    		[Token(Token = "0x60003B1")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60003AE")]
    	[Address(RVA = "0x38F3B0", Offset = "0x38DDB0", VA = "0x18038F3B0")]
    	public TernaryOperation(IExpression condition, IExpression truthy, IExpression falsy)
    	{
    	}
    }
}
