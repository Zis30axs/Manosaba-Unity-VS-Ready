using GigaCreation.Tools.Debugging.TextDisplays;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001C6")]
public class DebugTextPreferenceCustomVariable : DebugTextPreferenceBase
{
	[Token(Token = "0x4000863")]
	[FieldOffset(Offset = "0x50")]
	[Space]
	[SerializeField]
	private string _variableName;

	[Token(Token = "0x4000864")]
	[FieldOffset(Offset = "0x58")]
	private ICustomVariableManager _naniVariableManager;

	[Token(Token = "0x17000149")]
	private ICustomVariableManager NaniVariableManager
	{
		[Token(Token = "0x60007B8")]
		[Address(RVA = "0x3B3F80", Offset = "0x3B2980", VA = "0x1803B3F80")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700014A")]
	protected override string LabelText
	{
		[Token(Token = "0x60007B9")]
		[Address(RVA = "0x3B4040", Offset = "0x3B2A40", VA = "0x1803B4040", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60007BA")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugTextPreferenceCustomVariable()
	{
	}
}
