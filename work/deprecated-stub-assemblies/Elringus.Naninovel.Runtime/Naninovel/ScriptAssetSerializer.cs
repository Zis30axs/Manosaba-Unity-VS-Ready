using System.Collections.Generic;
using System.Text;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel
{
    [Token(Token = "0x2000420")]
    public class ScriptAssetSerializer
    {
    	[Token(Token = "0x4000F13")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly ISyntax stx;

    	[Token(Token = "0x4000F14")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly ScriptSerializer scriptSerializer;

    	[Token(Token = "0x4000F15")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly ListValueSerializer listSerializer;

    	[Token(Token = "0x4000F16")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly NamedValueSerializer namedSerializer;

    	[Token(Token = "0x4000F17")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly StringBuilder builder;

    	[Token(Token = "0x4000F18")]
    	[FieldOffset(Offset = "0x38")]
    	private readonly List<IValueComponent> components;

    	[Token(Token = "0x4000F19")]
    	[FieldOffset(Offset = "0x40")]
    	private ScriptTextMap map;

    	[Token(Token = "0x600141F")]
    	[Address(RVA = "0x5B6110", Offset = "0x5B4B10", VA = "0x1805B6110")]
    	public ScriptAssetSerializer(ISyntax stx)
    	{
    	}

    	[Token(Token = "0x6001420")]
    	[Address(RVA = "0x5B6560", Offset = "0x5B4F60", VA = "0x1805B6560")]
    	public string Serialize(Script script)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001421")]
    	[Address(RVA = "0x5B67E0", Offset = "0x5B51E0", VA = "0x1805B67E0")]
    	public string Serialize(ScriptLine line, ScriptTextMap map)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001422")]
    	[Address(RVA = "0x5B6840", Offset = "0x5B5240", VA = "0x1805B6840")]
    	private void Reset(ScriptTextMap map)
    	{
    	}

    	[Token(Token = "0x6001423")]
    	[Address(RVA = "0x5B6900", Offset = "0x5B5300", VA = "0x1805B6900")]
    	private void AppendLine(ScriptLine line)
    	{
    	}

    	[Token(Token = "0x6001424")]
    	[Address(RVA = "0x5B6D00", Offset = "0x5B5700", VA = "0x1805B6D00")]
    	private void AppendIndent(ScriptLine line)
    	{
    	}

    	[Token(Token = "0x6001425")]
    	[Address(RVA = "0x5B6E90", Offset = "0x5B5890", VA = "0x1805B6E90")]
    	private void AppendEmptyLine(EmptyScriptLine _)
    	{
    	}

    	[Token(Token = "0x6001426")]
    	[Address(RVA = "0x5B6F00", Offset = "0x5B5900", VA = "0x1805B6F00")]
    	private void AppendCommentLine(CommentScriptLine line)
    	{
    	}

    	[Token(Token = "0x6001427")]
    	[Address(RVA = "0x5B6FF0", Offset = "0x5B59F0", VA = "0x1805B6FF0")]
    	private void AppendLabelLine(LabelScriptLine line)
    	{
    	}

    	[Token(Token = "0x6001428")]
    	[Address(RVA = "0x5B70E0", Offset = "0x5B5AE0", VA = "0x1805B70E0")]
    	private void AppendCommandLine(CommandScriptLine line)
    	{
    	}

    	[Token(Token = "0x6001429")]
    	[Address(RVA = "0x5B7170", Offset = "0x5B5B70", VA = "0x1805B7170")]
    	private void AppendGenericLine(GenericTextScriptLine line)
    	{
    	}

    	[Token(Token = "0x600142A")]
    	[Address(RVA = "0x5B7660", Offset = "0x5B6060", VA = "0x1805B7660")]
    	private void AppendCommand(Command command)
    	{
    	}

    	[Token(Token = "0x600142B")]
    	[Address(RVA = "0x5B7AB0", Offset = "0x5B64B0", VA = "0x1805B7AB0")]
    	private bool ShouldSerializeAsBoolFlag(ICommandParameter param)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600142C")]
    	[Address(RVA = "0x5B7B60", Offset = "0x5B6560", VA = "0x1805B7B60")]
    	private void AppendValue(ICommandParameter param, SerializationContext ctx)
    	{
    	}

    	[Token(Token = "0x600142D")]
    	[Address(RVA = "0x5B7BB0", Offset = "0x5B65B0", VA = "0x1805B7BB0")]
    	private string SerializeValue(ICommandParameter param, SerializationContext ctx)
    	{
    		return null;
    	}

    	[Token(Token = "0x600142E")]
    	[Address(RVA = "0x5B89B0", Offset = "0x5B73B0", VA = "0x1805B89B0")]
    	private string SerializeRaw(RawValue raw, SerializationContext ctx)
    	{
    		return null;
    	}
    }
}
