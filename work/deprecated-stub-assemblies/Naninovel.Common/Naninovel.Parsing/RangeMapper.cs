using System.Collections;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000028")]
    public class RangeMapper
    {
    	[Token(Token = "0x4000046")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly Dictionary<ILineComponent, InlineRange> map;

    	[Token(Token = "0x60000A2")]
    	[Address(RVA = "0x10D3660", Offset = "0x10D2060", VA = "0x1810D3660", Slot = "4")]
    	public void Associate(ILineComponent component, InlineRange range)
    	{
    	}

    	[Token(Token = "0x60000A3")]
    	[Address(RVA = "0x10D36E0", Offset = "0x10D20E0", VA = "0x1810D36E0")]
    	public bool TryResolve(ILineComponent component, out InlineRange range)
    	{
    		range = default;
    		range = default;
    		return default(bool);
    	}

    	[Token(Token = "0x60000A4")]
    	[Address(RVA = "0x10D3750", Offset = "0x10D2150", VA = "0x1810D3750")]
    	public void Clear()
    	{
    	}

    	[Token(Token = "0x60000A5")]
    	[Address(RVA = "0x10D37A0", Offset = "0x10D21A0", VA = "0x1810D37A0", Slot = "5")]
    	public IEnumerator<KeyValuePair<ILineComponent, InlineRange>> GetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60000A6")]
    	[Address(RVA = "0x10D37A0", Offset = "0x10D21A0", VA = "0x1810D37A0", Slot = "6")]
    	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60000A7")]
    	[Address(RVA = "0x10D3840", Offset = "0x10D2240", VA = "0x1810D3840")]
    	public RangeMapper()
    	{
    	}
    }
}
