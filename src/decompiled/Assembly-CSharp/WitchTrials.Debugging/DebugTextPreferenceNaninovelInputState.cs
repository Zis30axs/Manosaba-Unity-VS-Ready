using System.Text;
using GigaCreation.Tools.Debugging.TextDisplays;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001C8")]
public class DebugTextPreferenceNaninovelInputState : DebugTextPreferenceBase
{
	[Token(Token = "0x4000865")]
	[FieldOffset(Offset = "0x50")]
	[SerializeField]
	private string[] _bindingNames;

	[Token(Token = "0x4000866")]
	[FieldOffset(Offset = "0x58")]
	private readonly StringBuilder _builder;

	[Token(Token = "0x4000867")]
	[FieldOffset(Offset = "0x60")]
	private IInputManager _naniInputManager;

	[Token(Token = "0x1700014C")]
	private IInputManager NaniInputManager
	{
		[Token(Token = "0x60007BD")]
		[Address(RVA = "0x3B4270", Offset = "0x3B2C70", VA = "0x1803B4270")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700014D")]
	protected override string LabelText
	{
		[Token(Token = "0x60007BE")]
		[Address(RVA = "0x3B4330", Offset = "0x3B2D30", VA = "0x1803B4330", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60007BF")]
	[Address(RVA = "0x3B43B0", Offset = "0x3B2DB0", VA = "0x1803B43B0")]
	private string BuildText()
	{
		return null;
	}

	[Token(Token = "0x60007C0")]
	[Address(RVA = "0x3B4740", Offset = "0x3B3140", VA = "0x1803B4740")]
	public DebugTextPreferenceNaninovelInputState()
	{
	}
}
