using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel;

[Token(Token = "0x2000416")]
public class ScriptAssetParser : IScriptParser
{
	[Token(Token = "0x4000EF4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private readonly List<ScriptLine> lines;

	[Token(Token = "0x4000EF5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly ParseErrorHandler errorHandler;

	[Token(Token = "0x4000EF6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private readonly TextMapper textMapper;

	[Token(Token = "0x4000EF7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private readonly ScriptParser modelParser;

	[Token(Token = "0x170002BB")]
	protected virtual CommentLineParser CommentLineParser
	{
		[Token(Token = "0x60013CE")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "5")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002BC")]
	protected virtual LabelLineParser LabelLineParser
	{
		[Token(Token = "0x60013CF")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "6")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002BD")]
	protected virtual CommandLineParser CommandLineParser
	{
		[Token(Token = "0x60013D0")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "7")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002BE")]
	protected virtual GenericTextLineParser GenericTextLineParser
	{
		[Token(Token = "0x60013D1")]
		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "8")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60013D2")]
	[Address(RVA = "0x5B2B10", Offset = "0x5B1510", VA = "0x1805B2B10")]
	public ScriptAssetParser()
	{
	}

	[Token(Token = "0x60013D3")]
	[Address(RVA = "0x5B32A0", Offset = "0x5B1CA0", VA = "0x1805B32A0", Slot = "9")]
	public virtual Script ParseText(string scriptPath, string scriptText, [Optional] ParseOptions options)
	{
		return null;
	}

	[Token(Token = "0x60013D4")]
	[Address(RVA = "0x5B3610", Offset = "0x5B2010", VA = "0x1805B3610", Slot = "10")]
	protected virtual CommentScriptLine ParseCommentLine(LineParseArgs<CommentLine> args)
	{
		return null;
	}

	[Token(Token = "0x60013D5")]
	[Address(RVA = "0x5B3670", Offset = "0x5B2070", VA = "0x1805B3670", Slot = "11")]
	protected virtual LabelScriptLine ParseLabelLine(LineParseArgs<LabelLine> args)
	{
		return null;
	}

	[Token(Token = "0x60013D6")]
	[Address(RVA = "0x5B36D0", Offset = "0x5B20D0", VA = "0x1805B36D0", Slot = "12")]
	protected virtual CommandScriptLine ParseCommandLine(LineParseArgs<CommandLine> args)
	{
		return null;
	}

	[Token(Token = "0x60013D7")]
	[Address(RVA = "0x5B3730", Offset = "0x5B2130", VA = "0x1805B3730", Slot = "13")]
	protected virtual GenericTextScriptLine ParseGenericTextLine(LineParseArgs<GenericLine> args)
	{
		return null;
	}

	[Token(Token = "0x60013D8")]
	[Address(RVA = "0x5B3790", Offset = "0x5B2190", VA = "0x1805B3790", Slot = "14")]
	protected virtual ScriptTextMap CreateTextMap()
	{
		return null;
	}

	[Token(Token = "0x60013D9")]
	[Address(RVA = "0x5B3BB0", Offset = "0x5B25B0", VA = "0x1805B3BB0")]
	private void Reset(ParseOptions options)
	{
	}
}
