using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000017")]
public class TextSplitter
{
	[Token(Token = "0x400001B")]
	[FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private char _003Cseparator_003EP;

	[Token(Token = "0x400001C")]
	[FieldOffset(Offset = "0x12")]
	[CompilerGenerated]
	private char _003Cescape_003EP;

	[Token(Token = "0x400001D")]
	[FieldOffset(Offset = "0x18")]
	private readonly StringBuilder buffer;

	[Token(Token = "0x6000056")]
	[Address(RVA = "0x10D0080", Offset = "0x10CEA80", VA = "0x1810D0080")]
	public TextSplitter(char separator, char escape = '\\')
	{
	}

	[Token(Token = "0x6000057")]
	[Address(RVA = "0x10D0140", Offset = "0x10CEB40", VA = "0x1810D0140")]
	public void Split(string str, ICollection<string> entries)
	{
	}

	[Token(Token = "0x6000058")]
	[Address(RVA = "0x10D0400", Offset = "0x10CEE00", VA = "0x1810D0400")]
	public string Join(IEnumerable<string> entries)
	{
		return null;
	}

	[Token(Token = "0x6000059")]
	[Address(RVA = "0x10D0800", Offset = "0x10CF200", VA = "0x1810D0800")]
	private void Reset()
	{
	}
}
