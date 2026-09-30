using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x2000097")]
    internal class AssignmentParser
    {
    	[Token(Token = "0x40001EC")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private Action<ParseDiagnostic> _003Cerr_003EP;

    	[Token(Token = "0x40001ED")]
    	[FieldOffset(Offset = "0x0")]
    	private static readonly char[] separator;

    	[Token(Token = "0x6000356")]
    	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    	public AssignmentParser(Action<ParseDiagnostic> err)
    	{
    	}

    	[Token(Token = "0x6000357")]
    	[Address(RVA = "0x10F72C0", Offset = "0x10F5CC0", VA = "0x1810F72C0")]
    	public void Parse(string text, IList<(string var, string text)> asses)
    	{
    	}

    	[Token(Token = "0x6000358")]
    	[Address(RVA = "0x10F74E0", Offset = "0x10F5EE0", VA = "0x1810F74E0")]
    	private (string, string) BuildAssignment(string text)
    	{
    		return default((string, string));
    	}

    	[Token(Token = "0x6000359")]
    	[Address(RVA = "0x10F7C10", Offset = "0x10F6610", VA = "0x1810F7C10")]
    	private void ProcessUnaryOperators(ref string text)
    	{
    	}

    	[Token(Token = "0x600035A")]
    	[Address(RVA = "0x10F83C0", Offset = "0x10F6DC0", VA = "0x1810F83C0")]
    	private void ExtractVariableAndExpression(string text, out string var, out string exp)
    	{
    		@var = default;
    		exp = default;
    		@var = default;
    		exp = default;
    	}

    	[Token(Token = "0x600035B")]
    	[Address(RVA = "0x10F8BA0", Offset = "0x10F75A0", VA = "0x1810F8BA0")]
    	private void ProcessCompoundAssignment(ref string var, ref string exp)
    	{
    	}
    }
}
