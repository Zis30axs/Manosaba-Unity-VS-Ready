using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000048")]
public class GenericLine : IScriptLine
{
	[Token(Token = "0x17000043")]
	public GenericPrefix? Prefix
	{
		[Token(Token = "0x600019E")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000044")]
	public IReadOnlyList<IGenericContent> Content
	{
		[Token(Token = "0x600019F")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000045")]
	public int Indent
	{
		[Token(Token = "0x60001A0")]
		[Address(RVA = "0x440360", Offset = "0x43ED60", VA = "0x180440360", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x600019D")]
	[Address(RVA = "0x10E27C0", Offset = "0x10E11C0", VA = "0x1810E27C0")]
	public GenericLine(GenericPrefix? prefix, IReadOnlyList<IGenericContent> content, int indent = 0)
	{
	}

	[Token(Token = "0x60001A1")]
	[Address(RVA = "0x10E2880", Offset = "0x10E1280", VA = "0x1810E2880")]
	public GenericLine(IReadOnlyList<IGenericContent> content, int indent = 0)
	{
	}

	[Token(Token = "0x60001A2")]
	[Address(RVA = "0x10E28A0", Offset = "0x10E12A0", VA = "0x1810E28A0", Slot = "3")]
	public override string ToString()
	{
		return null;
	}
}
