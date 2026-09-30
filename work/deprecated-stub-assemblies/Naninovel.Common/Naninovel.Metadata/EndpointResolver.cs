#nullable enable annotations
﻿using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel.Metadata
{
    [Token(Token = "0x200006D")]
    public class EndpointResolver
    {
    	[Token(Token = "0x4000150")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private IMetadata _003Cmeta_003EP;

    	[Token(Token = "0x4000151")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly NamedValueParser namedParser;

    	[Token(Token = "0x4000152")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly ExpressionEvaluator eval;

    	[Token(Token = "0x6000246")]
    	[Address(RVA = "0x10EAB70", Offset = "0x10E9570", VA = "0x1810EAB70")]
    	public EndpointResolver(IMetadata meta)
    	{
    	}

    	[Token(Token = "0x6000247")]
    	[Address(RVA = "0x10EAE50", Offset = "0x10E9850", VA = "0x1810EAE50")]
    	public bool TryResolve(Naninovel.Parsing.Command command, out Endpoint endpoint)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000248")]
    	[Address(RVA = "0x10EB1E0", Offset = "0x10E9BE0", VA = "0x1810EB1E0")]
    	public bool TryResolve(Naninovel.Parsing.Parameter parameter, string commandAliasOrId, out Endpoint endpoint)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000249")]
    	[Address(RVA = "0x10EB360", Offset = "0x10E9D60", VA = "0x1810EB360")]
    	private bool HasEndpointContext(string commandAliasOrId, string? paramAliasOrId)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600024A")]
    	[Address(RVA = "0x10EB620", Offset = "0x10EA020", VA = "0x1810EB620")]
    	private bool ResolveFromExpression(string expression, ref Endpoint endpoint)
    	{
    		return default(bool);
    	}
    }
}
