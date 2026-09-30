using System.Text;
using Il2CppDummyDll;

namespace Naninovel.ManagedText
{
    [Token(Token = "0x200008F")]
    public class MultilineManagedTextSerializer
    {
    	[Token(Token = "0x40001E2")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly StringBuilder builder;

    	[Token(Token = "0x40001E3")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly int spacing;

    	[Token(Token = "0x600033A")]
    	[Address(RVA = "0x10F5DF0", Offset = "0x10F47F0", VA = "0x1810F5DF0")]
    	public MultilineManagedTextSerializer(int spacing = 1)
    	{
    	}

    	[Token(Token = "0x600033B")]
    	[Address(RVA = "0x10F5EB0", Offset = "0x10F48B0", VA = "0x1810F5EB0")]
    	public string Serialize(ManagedTextDocument document)
    	{
    		return null;
    	}

    	[Token(Token = "0x600033C")]
    	[Address(RVA = "0x10F6270", Offset = "0x10F4C70", VA = "0x1810F6270")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x600033D")]
    	[Address(RVA = "0x10F62A0", Offset = "0x10F4CA0", VA = "0x1810F62A0")]
    	private void AppendHeader(string header)
    	{
    	}

    	[Token(Token = "0x600033E")]
    	[Address(RVA = "0x10F6370", Offset = "0x10F4D70", VA = "0x1810F6370")]
    	private void AppendRecord(ManagedTextRecord record)
    	{
    	}

    	[Token(Token = "0x600033F")]
    	[Address(RVA = "0x10F6810", Offset = "0x10F5210", VA = "0x1810F6810")]
    	private string InsertLineBreaksAfterBrTags(string value)
    	{
    		return null;
    	}
    }
}
