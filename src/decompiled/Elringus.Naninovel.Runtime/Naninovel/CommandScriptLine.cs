using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003E2")]
public class CommandScriptLine : ScriptLine
{
	[Token(Token = "0x4000E9C")]
	[FieldOffset(Offset = "0x20")]
	[SerializeReference]
	private Command command;

	[Token(Token = "0x1700028D")]
	public Command Command
	{
		[Token(Token = "0x60012BB")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60012BC")]
	[Address(RVA = "0x5A4A80", Offset = "0x5A3480", VA = "0x1805A4A80")]
	public CommandScriptLine(Command command, int lineIndex, int indent, string lineHash)
	{
	}
}
