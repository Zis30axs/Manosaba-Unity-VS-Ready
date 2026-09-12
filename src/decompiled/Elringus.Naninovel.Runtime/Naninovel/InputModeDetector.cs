using System;
using Il2CppDummyDll;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Naninovel;

[Token(Token = "0x2000372")]
public class InputModeDetector : IDisposable
{
	[Token(Token = "0x4000CC5")]
	[FieldOffset(Offset = "0x10")]
	private readonly IInputManager input;

	[Token(Token = "0x6001099")]
	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
	public InputModeDetector(IInputManager input)
	{
	}

	[Token(Token = "0x600109A")]
	[Address(RVA = "0x578930", Offset = "0x577330", VA = "0x180578930")]
	public void Start()
	{
	}

	[Token(Token = "0x600109B")]
	[Address(RVA = "0x5789D0", Offset = "0x5773D0", VA = "0x1805789D0")]
	public void Stop()
	{
	}

	[Token(Token = "0x600109C")]
	[Address(RVA = "0x5789D0", Offset = "0x5773D0", VA = "0x1805789D0", Slot = "4")]
	public void Dispose()
	{
	}

	[Token(Token = "0x600109D")]
	[Address(RVA = "0x578A70", Offset = "0x577470", VA = "0x180578A70")]
	private void HandleInputEvent(InputEventPtr ptr, InputDevice device)
	{
	}

	[Token(Token = "0x600109E")]
	[Address(RVA = "0x578D40", Offset = "0x577740", VA = "0x180578D40")]
	private bool ShouldChangeToMouse(InputDevice device)
	{
		return default(bool);
	}

	[Token(Token = "0x600109F")]
	[Address(RVA = "0x578E50", Offset = "0x577850", VA = "0x180578E50")]
	private bool ShouldChangeToKeyboard(InputDevice device)
	{
		return default(bool);
	}

	[Token(Token = "0x60010A0")]
	[Address(RVA = "0x578F10", Offset = "0x577910", VA = "0x180578F10")]
	private bool ShouldChangeToTouch(InputDevice device)
	{
		return default(bool);
	}

	[Token(Token = "0x60010A1")]
	[Address(RVA = "0x578FE0", Offset = "0x5779E0", VA = "0x180578FE0")]
	private bool ShouldChangeToGamepad(InputDevice device)
	{
		return default(bool);
	}
}
