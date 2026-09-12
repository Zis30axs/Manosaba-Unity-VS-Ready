using System.Text;
using Il2CppDummyDll;

namespace Naninovel.ManagedText;

[Token(Token = "0x2000088")]
public class InlineManagedTextSerializer
{
	[Token(Token = "0x40001CF")]
	[FieldOffset(Offset = "0x10")]
	private readonly StringBuilder builder;

	[Token(Token = "0x40001D0")]
	[FieldOffset(Offset = "0x18")]
	private readonly int spacing;

	[Token(Token = "0x6000323")]
	[Address(RVA = "0x10F3270", Offset = "0x10F1C70", VA = "0x1810F3270")]
	public InlineManagedTextSerializer(int spacing = 1)
	{
	}

	[Token(Token = "0x6000324")]
	[Address(RVA = "0x10F3330", Offset = "0x10F1D30", VA = "0x1810F3330")]
	public string Serialize(ManagedTextDocument document)
	{
		return null;
	}

	[Token(Token = "0x6000325")]
	[Address(RVA = "0x10F3790", Offset = "0x10F2190", VA = "0x1810F3790")]
	private void AppendHeader(string header)
	{
	}

	[Token(Token = "0x6000326")]
	[Address(RVA = "0x10F3860", Offset = "0x10F2260", VA = "0x1810F3860")]
	private void AppendRecord(ManagedTextRecord record)
	{
	}
}
