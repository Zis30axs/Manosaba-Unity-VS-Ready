using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003E6")]
public class LabelScriptLine : ScriptLine
{
	[Token(Token = "0x4000E9F")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private string labelText;

	[Token(Token = "0x17000290")]
	public string LabelText
	{
		[Token(Token = "0x60012C2")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60012C3")]
	[Address(RVA = "0x5A4A80", Offset = "0x5A3480", VA = "0x1805A4A80")]
	public LabelScriptLine(string labelText, int lineIndex, int indent, string lineHash)
	{
	}
}
