using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000047")]
    public class Expression : IValueComponent, ILineComponent
    {
    	[Token(Token = "0x17000042")]
    	public PlainText Body
    	{
    		[Token(Token = "0x600019B")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x600019A")]
    	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    	public Expression(PlainText body)
    	{
    	}

    	[Token(Token = "0x600019C")]
    	[Address(RVA = "0x10E26E0", Offset = "0x10E10E0", VA = "0x1810E26E0", Slot = "3")]
    	public override string ToString()
    	{
    		return null;
    	}
    }
}
