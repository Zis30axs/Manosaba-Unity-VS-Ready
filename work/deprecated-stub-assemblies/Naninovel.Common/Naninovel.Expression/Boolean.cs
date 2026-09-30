using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x20000A3")]
    public class Boolean : IOperand, IExpression
    {
    	[Token(Token = "0x170000DD")]
    	public bool Value
    	{
    		[Token(Token = "0x600039F")]
    		[Address(RVA = "0x44D320", Offset = "0x44BD20", VA = "0x18044D320")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x600039E")]
    	[Address(RVA = "0x30C130", Offset = "0x30AB30", VA = "0x18030C130")]
    	public Boolean(bool value)
    	{
    	}

    	[Token(Token = "0x60003A0")]
    	[Address(RVA = "0x10FF860", Offset = "0x10FE260", VA = "0x1810FF860", Slot = "4")]
    	public object GetValue()
    	{
    		return null;
    	}
    }
}
