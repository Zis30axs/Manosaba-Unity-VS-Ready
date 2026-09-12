using System.Collections.Generic;
using System.Text;
using Il2CppDummyDll;

namespace Naninovel.ManagedText;

[Token(Token = "0x200008D")]
public class MultilineManagedTextParser
{
	[Token(Token = "0x40001DA")]
	[FieldOffset(Offset = "0x10")]
	private readonly HashSet<ManagedTextRecord> records;

	[Token(Token = "0x40001DB")]
	[FieldOffset(Offset = "0x18")]
	private readonly List<string> keys;

	[Token(Token = "0x40001DC")]
	[FieldOffset(Offset = "0x20")]
	private readonly List<string> comments;

	[Token(Token = "0x40001DD")]
	[FieldOffset(Offset = "0x28")]
	private readonly List<string> values;

	[Token(Token = "0x40001DE")]
	[FieldOffset(Offset = "0x30")]
	private readonly StringBuilder valueBuilder;

	[Token(Token = "0x40001DF")]
	[FieldOffset(Offset = "0x38")]
	private readonly StringBuilder commentBuilder;

	[Token(Token = "0x40001E0")]
	[FieldOffset(Offset = "0x40")]
	private string header;

	[Token(Token = "0x6000333")]
	[Address(RVA = "0x10F4690", Offset = "0x10F3090", VA = "0x1810F4690")]
	public ManagedTextDocument Parse(string text)
	{
		return null;
	}

	[Token(Token = "0x6000334")]
	[Address(RVA = "0x10F4BD0", Offset = "0x10F35D0", VA = "0x1810F4BD0")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000335")]
	[Address(RVA = "0x10F4CF0", Offset = "0x10F36F0", VA = "0x1810F4CF0")]
	private void ParseKeyLine(string line)
	{
	}

	[Token(Token = "0x6000336")]
	[Address(RVA = "0x10F5150", Offset = "0x10F3B50", VA = "0x1810F5150")]
	private void ParseCommentLine(string line, int index)
	{
	}

	[Token(Token = "0x6000337")]
	[Address(RVA = "0x10F5690", Offset = "0x10F4090", VA = "0x1810F5690")]
	private void ParseValueLine(string line)
	{
	}

	[Token(Token = "0x6000338")]
	[Address(RVA = "0x10F56B0", Offset = "0x10F40B0", VA = "0x1810F56B0")]
	private void AddLastRecord()
	{
	}

	[Token(Token = "0x6000339")]
	[Address(RVA = "0x10F5A50", Offset = "0x10F4450", VA = "0x1810F5A50")]
	public MultilineManagedTextParser()
	{
	}
}
