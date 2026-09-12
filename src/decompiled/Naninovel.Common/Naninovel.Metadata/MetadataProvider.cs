using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel.Metadata;

[Token(Token = "0x2000072")]
public class MetadataProvider : IMetadata
{
	[Token(Token = "0x4000167")]
	[FieldOffset(Offset = "0x20")]
	private readonly List<Actor> actors;

	[Token(Token = "0x4000168")]
	[FieldOffset(Offset = "0x28")]
	private readonly List<Command> commands;

	[Token(Token = "0x4000169")]
	[FieldOffset(Offset = "0x30")]
	private readonly List<Constant> constants;

	[Token(Token = "0x400016A")]
	[FieldOffset(Offset = "0x38")]
	private readonly List<Resource> resources;

	[Token(Token = "0x400016B")]
	[FieldOffset(Offset = "0x40")]
	private readonly List<string> variables;

	[Token(Token = "0x400016C")]
	[FieldOffset(Offset = "0x48")]
	private readonly List<Function> functions;

	[Token(Token = "0x400016D")]
	[FieldOffset(Offset = "0x50")]
	private readonly Dictionary<string, Command> commandById;

	[Token(Token = "0x400016E")]
	[FieldOffset(Offset = "0x58")]
	private readonly Dictionary<string, Command> commandByAlias;

	[Token(Token = "0x400016F")]
	[FieldOffset(Offset = "0x60")]
	private readonly Dictionary<string, List<Function>> fnsByName;

	[Token(Token = "0x4000170")]
	[FieldOffset(Offset = "0x68")]
	private readonly SyntaxProvider syntaxProvider;

	[Token(Token = "0x17000086")]
	public string? EntryScript
	{
		[Token(Token = "0x6000269")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x600026A")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000087")]
	public string? TitleScript
	{
		[Token(Token = "0x600026B")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "5")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x600026C")]
		[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000088")]
	public IReadOnlyCollection<Actor> Actors
	{
		[Token(Token = "0x600026D")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "6")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000089")]
	public IReadOnlyCollection<Command> Commands
	{
		[Token(Token = "0x600026E")]
		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "7")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700008A")]
	public IReadOnlyCollection<Constant> Constants
	{
		[Token(Token = "0x600026F")]
		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "8")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700008B")]
	public IReadOnlyCollection<Resource> Resources
	{
		[Token(Token = "0x6000270")]
		[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410", Slot = "9")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700008C")]
	public IReadOnlyCollection<string> Variables
	{
		[Token(Token = "0x6000271")]
		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10", Slot = "10")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700008D")]
	public IReadOnlyCollection<Function> Functions
	{
		[Token(Token = "0x6000272")]
		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470", Slot = "11")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700008E")]
	public ISyntax Syntax
	{
		[Token(Token = "0x6000273")]
		[Address(RVA = "0x398D30", Offset = "0x397730", VA = "0x180398D30", Slot = "12")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000274")]
	[Address(RVA = "0x10ECBC0", Offset = "0x10EB5C0", VA = "0x1810ECBC0")]
	public MetadataProvider()
	{
	}

	[Token(Token = "0x6000275")]
	[Address(RVA = "0x10ED2A0", Offset = "0x10EBCA0", VA = "0x1810ED2A0")]
	public MetadataProvider(Project meta)
	{
	}

	[Token(Token = "0x6000276")]
	[Address(RVA = "0x10ED9A0", Offset = "0x10EC3A0", VA = "0x1810ED9A0")]
	public void Update(Project meta)
	{
	}

	[Token(Token = "0x6000277")]
	[Address(RVA = "0x10EE0A0", Offset = "0x10ECAA0", VA = "0x1810EE0A0", Slot = "13")]
	public Command FindCommand(string aliasOrId)
	{
		return null;
	}

	[Token(Token = "0x6000278")]
	[Address(RVA = "0x10EE150", Offset = "0x10ECB50", VA = "0x1810EE150", Slot = "14")]
	public Parameter? FindParameter(string commandAliasOrId, string? paramAliasOrId)
	{
		return null;
	}

	[Token(Token = "0x6000279")]
	[Address(RVA = "0x10EE320", Offset = "0x10ECD20", VA = "0x1810EE320", Slot = "15")]
	public bool FindFunctions(string name, ICollection<Function> result)
	{
		return default(bool);
	}

	[Token(Token = "0x600027A")]
	[Address(RVA = "0x10EE540", Offset = "0x10ECF40", VA = "0x1810EE540")]
	private void Reset()
	{
	}

	[Token(Token = "0x600027B")]
	[Address(RVA = "0x10EE8A0", Offset = "0x10ED2A0", VA = "0x1810EE8A0")]
	private void IndexCommand(Command command)
	{
	}

	[Token(Token = "0x600027C")]
	[Address(RVA = "0x10EE930", Offset = "0x10ED330", VA = "0x1810EE930")]
	private void IndexFunction(Function fn)
	{
	}
}
