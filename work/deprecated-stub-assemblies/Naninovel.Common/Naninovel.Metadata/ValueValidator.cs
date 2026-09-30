#nullable enable annotations
﻿using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel.Metadata
{
    [Token(Token = "0x2000085")]
    public class ValueValidator
    {
    	[Token(Token = "0x40001C7")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private ISyntax _003Cstx_003EP;

    	[Token(Token = "0x40001C8")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly ListValueParser listParser;

    	[Token(Token = "0x40001C9")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly NamedValueParser namedParser;

    	[Token(Token = "0x40001CA")]
    	[FieldOffset(Offset = "0x28")]
    	private string value;

    	[Token(Token = "0x40001CB")]
    	[FieldOffset(Offset = "0x30")]
    	private ValueType type;

    	[Token(Token = "0x6000316")]
    	[Address(RVA = "0x10F1990", Offset = "0x10F0390", VA = "0x1810F1990")]
    	public ValueValidator(ISyntax stx)
    	{
    	}

    	[Token(Token = "0x6000317")]
    	[Address(RVA = "0x10F1B50", Offset = "0x10F0550", VA = "0x1810F1B50")]
    	public bool Validate(string? value, ValueContainerType container, ValueType type)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000318")]
    	[Address(RVA = "0x10F1CE0", Offset = "0x10F06E0", VA = "0x1810F1CE0")]
    	private void Reset(string value, ValueType type)
    	{
    	}

    	[Token(Token = "0x6000319")]
    	[Address(RVA = "0x10F1D40", Offset = "0x10F0740", VA = "0x1810F1D40")]
    	private bool ValidateList()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600031A")]
    	[Address(RVA = "0x10F1DD0", Offset = "0x10F07D0", VA = "0x1810F1DD0")]
    	private bool ValidateNamedList()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600031B")]
    	[Address(RVA = "0x10F1E70", Offset = "0x10F0870", VA = "0x1810F1E70")]
    	private bool ValidateNamed(string value)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600031C")]
    	[Address(RVA = "0x10F1F40", Offset = "0x10F0940", VA = "0x1810F1F40")]
    	private bool ValidateSingle(string? value)
    	{
    		return default(bool);
    	}
    }
}
