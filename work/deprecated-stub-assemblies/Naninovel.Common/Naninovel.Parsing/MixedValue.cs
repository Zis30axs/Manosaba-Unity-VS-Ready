using System.Collections;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000051")]
    public class MixedValue
    {
    	[Token(Token = "0x40000CD")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly IReadOnlyList<IValueComponent> components;

    	[Token(Token = "0x1700004E")]
    	public bool Dynamic
    	{
    		[Token(Token = "0x60001B4")]
    		[Address(RVA = "0x10E2F00", Offset = "0x10E1900", VA = "0x1810E2F00")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700004F")]
    	public int Count
    	{
    		[Token(Token = "0x60001B5")]
    		[Address(RVA = "0x10E2F10", Offset = "0x10E1910", VA = "0x1810E2F10", Slot = "5")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000050")]
    	public IValueComponent this[int index]
    	{
    		[Token(Token = "0x60001B8")]
    		[Address(RVA = "0x10E30A0", Offset = "0x10E1AA0", VA = "0x1810E30A0", Slot = "4")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60001B3")]
    	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    	public MixedValue(IValueComponent[] components)
    	{
    	}

    	[Token(Token = "0x60001B6")]
    	[Address(RVA = "0x10E2F60", Offset = "0x10E1960", VA = "0x1810E2F60")]
    	public MixedValue()
    	{
    	}

    	[Token(Token = "0x60001B7")]
    	[Address(RVA = "0x10E2FF0", Offset = "0x10E19F0", VA = "0x1810E2FF0")]
    	public static implicit operator MixedValue(IValueComponent[]? components)
    	{
    		return null;
    	}

    	[Token(Token = "0x60001B9")]
    	[Address(RVA = "0x10E3100", Offset = "0x10E1B00", VA = "0x1810E3100", Slot = "6")]
    	public IEnumerator<IValueComponent> GetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60001BA")]
    	[Address(RVA = "0x10E3100", Offset = "0x10E1B00", VA = "0x1810E3100", Slot = "7")]
    	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60001BB")]
    	[Address(RVA = "0x10E3150", Offset = "0x10E1B50", VA = "0x1810E3150", Slot = "3")]
    	public override string ToString()
    	{
    		return null;
    	}

    	[Token(Token = "0x60001BC")]
    	[Address(RVA = "0x10E33A0", Offset = "0x10E1DA0", VA = "0x1810E33A0")]
    	private bool HasExpression()
    	{
    		return default(bool);
    	}
    }
}
