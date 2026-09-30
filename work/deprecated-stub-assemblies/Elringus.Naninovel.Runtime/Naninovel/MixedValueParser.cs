using System.Collections.Generic;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel
{
    [Token(Token = "0x2000412")]
    public class MixedValueParser
    {
    	[Token(Token = "0x4000EEA")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly List<RawValuePart> parts;

    	[Token(Token = "0x4000EEB")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly ITextIdentifier identifier;

    	[Token(Token = "0x60013BD")]
    	[Address(RVA = "0x5B20A0", Offset = "0x5B0AA0", VA = "0x1805B20A0")]
    	public MixedValueParser(ITextIdentifier identifier)
    	{
    	}

    	[Token(Token = "0x60013BE")]
    	[Address(RVA = "0x5B21C0", Offset = "0x5B0BC0", VA = "0x1805B21C0")]
    	public RawValue Parse(MixedValue mixed, bool hashPlainText)
    	{
    		return default(RawValue);
    	}

    	[Token(Token = "0x60013BF")]
    	[Address(RVA = "0x5B26C0", Offset = "0x5B10C0", VA = "0x1805B26C0")]
    	private RawValuePart HashPlainText(PlainText plain)
    	{
    		return default(RawValuePart);
    	}
    }
}
