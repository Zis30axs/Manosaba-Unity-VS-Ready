using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x2000088")]
public class AutoToggleUi : CustomUI
{
	[Token(Token = "0x4000215")]
	[FieldOffset(Offset = "0xE0")]
	[Header("References for Auto Toggle UI")]
	[SerializeField]
	private Toggle _toggle;

	[Token(Token = "0x4000216")]
	[FieldOffset(Offset = "0xE8")]
	private GigaCreation.Essentials.Input.IInputManager _inputManager;

	[Token(Token = "0x4000217")]
	[FieldOffset(Offset = "0xF0")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x1700002A")]
	private GigaCreation.Essentials.Input.IInputManager InputManager
	{
		[Token(Token = "0x60001BA")]
		[Address(RVA = "0x336930", Offset = "0x335330", VA = "0x180336930")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700002B")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x60001BB")]
		[Address(RVA = "0x336A00", Offset = "0x335400", VA = "0x180336A00")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60001BC")]
	[Address(RVA = "0x336AD0", Offset = "0x3354D0", VA = "0x180336AD0", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x60001BD")]
	[Address(RVA = "0x336BA0", Offset = "0x3355A0", VA = "0x180336BA0", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x60001BE")]
	[Address(RVA = "0x336C70", Offset = "0x335670", VA = "0x180336C70", Slot = "83")]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60001BF")]
	[Address(RVA = "0x336DE0", Offset = "0x3357E0", VA = "0x180336DE0")]
	private void HandleAutoModeChanged(bool enable)
	{
	}

	[Token(Token = "0x60001C0")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public AutoToggleUi()
	{
	}
}
