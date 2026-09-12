using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003E5")]
public class GenericTextScriptLine : ScriptLine
{
	[Token(Token = "0x4000E9E")]
	[FieldOffset(Offset = "0x20")]
	[SerializeReference]
	private List<Command> inlinedCommands;

	[Token(Token = "0x1700028F")]
	public IReadOnlyList<Command> InlinedCommands
	{
		[Token(Token = "0x60012C0")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60012C1")]
	[Address(RVA = "0x5A4BB0", Offset = "0x5A35B0", VA = "0x1805A4BB0")]
	public GenericTextScriptLine(IEnumerable<Command> inlinedCommands, int lineIndex, int indent, string lineHash)
	{
	}
}
