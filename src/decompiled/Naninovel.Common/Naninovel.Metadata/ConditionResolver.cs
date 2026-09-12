using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel.Metadata;

[Token(Token = "0x2000068")]
public class ConditionResolver
{
	[Token(Token = "0x4000139")]
	[FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private IMetadata _003Cmeta_003EP;

	[Token(Token = "0x400013A")]
	[FieldOffset(Offset = "0x18")]
	private readonly HashSet<Naninovel.Parsing.Command> extractedCommands;

	[Token(Token = "0x400013B")]
	[FieldOffset(Offset = "0x20")]
	private IList<Condition> conditions;

	[Token(Token = "0x400013C")]
	[FieldOffset(Offset = "0x28")]
	private Naninovel.Parsing.Command selfCommand;

	[Token(Token = "0x400013D")]
	[FieldOffset(Offset = "0x30")]
	private IScriptLine? selfLine;

	[Token(Token = "0x400013E")]
	[FieldOffset(Offset = "0x38")]
	private bool conditionAdded;

	[Token(Token = "0x400013F")]
	[FieldOffset(Offset = "0x40")]
	private string? switchRoot;

	[Token(Token = "0x4000140")]
	[FieldOffset(Offset = "0x48")]
	private int minIndent;

	[Token(Token = "0x600022F")]
	[Address(RVA = "0x10E98C0", Offset = "0x10E82C0", VA = "0x1810E98C0")]
	public ConditionResolver(IMetadata meta)
	{
	}

	[Token(Token = "0x6000230")]
	[Address(RVA = "0x10E99E0", Offset = "0x10E83E0", VA = "0x1810E99E0")]
	public bool TryResolve(Naninovel.Parsing.Command command, IReadOnlyList<IScriptLine> script, IList<Condition> conditions)
	{
		return default(bool);
	}

	[Token(Token = "0x6000231")]
	[Address(RVA = "0x10E9C40", Offset = "0x10E8640", VA = "0x1810E9C40")]
	public bool TryResolve(Naninovel.Parsing.Command command, out Naninovel.Parsing.Parameter param)
	{
		param = default;
		param = default;
		return default(bool);
	}

	[Token(Token = "0x6000232")]
	[Address(RVA = "0x10E9E40", Offset = "0x10E8840", VA = "0x1810E9E40")]
	public bool IsCondition(string paramAliasOrId, string commandAliasOrId)
	{
		return default(bool);
	}

	[Token(Token = "0x6000233")]
	[Address(RVA = "0x10E9F60", Offset = "0x10E8960", VA = "0x1810E9F60")]
	private void Reset(Naninovel.Parsing.Command command, IList<Condition> conditions)
	{
	}

	[Token(Token = "0x6000234")]
	[Address(RVA = "0x10EA0C0", Offset = "0x10E8AC0", VA = "0x1810EA0C0")]
	private bool Move(IScriptLine line)
	{
		return default(bool);
	}

	[Token(Token = "0x6000235")]
	[Address(RVA = "0x10EA490", Offset = "0x10E8E90", VA = "0x1810EA490")]
	private void AddCondition(IScriptLine line, Naninovel.Parsing.Command command, Naninovel.Parsing.Parameter? param, bool inverse = false)
	{
	}

	[Token(Token = "0x6000236")]
	[Address(RVA = "0x10EA5E0", Offset = "0x10E8FE0", VA = "0x1810EA5E0")]
	private IReadOnlyCollection<Naninovel.Parsing.Command> ExtractCommands(IScriptLine line)
	{
		return null;
	}
}
