using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000058")]
    public class ScriptSerializer
    {
    	[Token(Token = "0x40000D9")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private ISyntax _003Cstx_003EP;

    	[Token(Token = "0x40000DA")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly Utilities utils;

    	[Token(Token = "0x40000DB")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly StringBuilder builder;

    	[Token(Token = "0x40000DC")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly StringBuilder mixedBuilder;

    	[Token(Token = "0x40000DD")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly List<(int, int)> ignoreRanges;

    	[Token(Token = "0x60001D6")]
    	[Address(RVA = "0x10E46F0", Offset = "0x10E30F0", VA = "0x1810E46F0")]
    	public ScriptSerializer(ISyntax stx)
    	{
    	}

    	[Token(Token = "0x60001D7")]
    	[Address(RVA = "0x10E49C0", Offset = "0x10E33C0", VA = "0x1810E49C0")]
    	public string Serialize(IScriptLine line)
    	{
    		return null;
    	}

    	[Token(Token = "0x60001D8")]
    	[Address(RVA = "0x10E4A20", Offset = "0x10E3420", VA = "0x1810E4A20")]
    	public string Serialize(IEnumerable<IScriptLine> lines)
    	{
    		return null;
    	}

    	[Token(Token = "0x60001D9")]
    	[Address(RVA = "0x10E4CF0", Offset = "0x10E36F0", VA = "0x1810E4CF0")]
    	public string Serialize(IEnumerable<IValueComponent> value, SerializationContext ctx)
    	{
    		return null;
    	}

    	[Token(Token = "0x60001DA")]
    	[Address(RVA = "0x10E4D80", Offset = "0x10E3780", VA = "0x1810E4D80")]
    	private void AppendLine(IScriptLine line)
    	{
    	}

    	[Token(Token = "0x60001DB")]
    	[Address(RVA = "0x10E5280", Offset = "0x10E3C80", VA = "0x1810E5280")]
    	private void AppendIndent(IScriptLine line)
    	{
    	}

    	[Token(Token = "0x60001DC")]
    	[Address(RVA = "0x10E53A0", Offset = "0x10E3DA0", VA = "0x1810E53A0")]
    	private void AppendCommentLine(CommentLine commentLine)
    	{
    	}

    	[Token(Token = "0x60001DD")]
    	[Address(RVA = "0x10E54F0", Offset = "0x10E3EF0", VA = "0x1810E54F0")]
    	private void AppendLabelLine(LabelLine labelLine)
    	{
    	}

    	[Token(Token = "0x60001DE")]
    	[Address(RVA = "0x10E5640", Offset = "0x10E4040", VA = "0x1810E5640")]
    	private void AppendCommandLine(CommandLine commandLine)
    	{
    	}

    	[Token(Token = "0x60001DF")]
    	[Address(RVA = "0x10E56F0", Offset = "0x10E40F0", VA = "0x1810E56F0")]
    	private void AppendGenericLine(GenericLine line)
    	{
    	}

    	[Token(Token = "0x60001E0")]
    	[Address(RVA = "0x10E5AE0", Offset = "0x10E44E0", VA = "0x1810E5AE0")]
    	private void AppendCommand(Command command)
    	{
    	}

    	[Token(Token = "0x60001E1")]
    	[Address(RVA = "0x10E5EA0", Offset = "0x10E48A0", VA = "0x1810E5EA0")]
    	private void AppendParameter(Parameter parameter)
    	{
    	}

    	[Token(Token = "0x60001E2")]
    	[Address(RVA = "0x10E6020", Offset = "0x10E4A20", VA = "0x1810E6020")]
    	private void AppendBooleanParameter(Parameter parameter)
    	{
    	}

    	[Token(Token = "0x60001E3")]
    	[Address(RVA = "0x10E6320", Offset = "0x10E4D20", VA = "0x1810E6320")]
    	private void AppendGenericPrefix(GenericPrefix prefix)
    	{
    	}

    	[Token(Token = "0x60001E4")]
    	[Address(RVA = "0x10E64B0", Offset = "0x10E4EB0", VA = "0x1810E64B0")]
    	private void AppendInlinedCommand(InlinedCommand inlined)
    	{
    	}

    	[Token(Token = "0x60001E5")]
    	[Address(RVA = "0x10E65A0", Offset = "0x10E4FA0", VA = "0x1810E65A0")]
    	private void AppendMixed(IEnumerable<IValueComponent> mixed, bool wrap, bool leadGeneric, bool nameless)
    	{
    	}

    	[Token(Token = "0x60001E6")]
    	[Address(RVA = "0x10E69A0", Offset = "0x10E53A0", VA = "0x1810E69A0")]
    	private void AppendComponent(IValueComponent component)
    	{
    	}

    	[Token(Token = "0x60001E7")]
    	[Address(RVA = "0x10E6AF0", Offset = "0x10E54F0", VA = "0x1810E6AF0")]
    	private void AppendExpression(Expression expression)
    	{
    	}

    	[Token(Token = "0x60001E8")]
    	[Address(RVA = "0x10E6CA0", Offset = "0x10E56A0", VA = "0x1810E6CA0")]
    	private void AppendIdentifiedText(IdentifiedText identifiedText)
    	{
    	}

    	[Token(Token = "0x60001E9")]
    	[Address(RVA = "0x10E6E70", Offset = "0x10E5870", VA = "0x1810E6E70")]
    	private string Encode(string value, bool considerWrap, bool nameless)
    	{
    		return null;
    	}

    	[Token(Token = "0x60001EA")]
    	[Address(RVA = "0x10E71E0", Offset = "0x10E5BE0", VA = "0x1810E71E0")]
    	private string EscapeAuthor(string value)
    	{
    		return null;
    	}

    	[Token(Token = "0x60001EB")]
    	[Address(RVA = "0x10E74B0", Offset = "0x10E5EB0", VA = "0x1810E74B0")]
    	private static StringBuilder TrimTrailingLineBreaks(StringBuilder builder)
    	{
    		return null;
    	}

    	[Token(Token = "0x60001EC")]
    	[Address(RVA = "0x10E7520", Offset = "0x10E5F20", VA = "0x1810E7520")]
    	private bool IsBooleanParameter(Parameter parameter)
    	{
    		return default(bool);
    	}
    }
}
