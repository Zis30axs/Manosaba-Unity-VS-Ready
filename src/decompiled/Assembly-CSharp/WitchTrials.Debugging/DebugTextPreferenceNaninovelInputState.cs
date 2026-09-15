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
		// RECONSTRUCTED | Confidence: A | RVA: 0x3B4270, 186 B
		// Lazy cache in +0x60 via `Engine.GetServiceOrErr<IInputManager>()`.
		[Token(Token = "0x60007BD")]
		[Address(RVA = "0x3B4270", Offset = "0x3B2C70", VA = "0x1803B4270")]
		get
		{
			return _naniInputManager ??= Engine.GetServiceOrErr<IInputManager>();
		}
	}

	[Token(Token = "0x1700014D")]
	protected override string LabelText
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3B4330, 119 B
		// Engine.Initialized ? tail jump to BuildText : literal "Naninovel has not been initialized".
		[Token(Token = "0x60007BE")]
		[Address(RVA = "0x3B4330", Offset = "0x3B2D30", VA = "0x1803B4330", Slot = "4")]
		get
		{
			return Engine.Initialized ? BuildText() : "Naninovel has not been initialized";
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3B43B0, 899 B
	// - `_builder` (+0x58) Length = 0; "ProcessInput: " + (IInputManager slot 2 ProcessInput ? "O" : "X") + " |".
	// - `_bindingNames` (+0x50) is loaded once and walked by index against the cached array length (foreach over an
	//   array): "| " + name + ": " + (slot 9 IsSampling(name) ? "O" : "X") + " ".
	// - Returns `_builder` vtable slot 3 ToString() through String.TrimWhiteSpaceHelper(Both) = Trim().
	[Token(Token = "0x60007BF")]
	[Address(RVA = "0x3B43B0", Offset = "0x3B2DB0", VA = "0x1803B43B0")]
	private string BuildText()
	{
		_builder.Length = 0;
		_builder.Append("ProcessInput: ");
		_builder.Append(NaniInputManager.ProcessInput ? "O" : "X");
		_builder.Append(" |");
		foreach (string bindingName in _bindingNames)
		{
			_builder.Append("| ");
			_builder.Append(bindingName);
			_builder.Append(": ");
			_builder.Append(NaniInputManager.IsSampling(bindingName) ? "O" : "X");
			_builder.Append(" ");
		}
		return _builder.ToString().Trim();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3B4740, 210 B
	// `_builder` (+0x58) = new StringBuilder(), then the inlined MonoBehaviour base ctor.
	[Token(Token = "0x60007C0")]
	[Address(RVA = "0x3B4740", Offset = "0x3B3140", VA = "0x1803B4740")]
	public DebugTextPreferenceNaninovelInputState()
	{
		_builder = new StringBuilder();
	}
}
