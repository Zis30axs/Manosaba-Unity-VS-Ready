using System;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using Naninovel.UI;
using TMPro;
using UniRx;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000E0")]
public class DebateText : NaninovelTMProText
{
	[Token(Token = "0x4000446")]
	[FieldOffset(Offset = "0x820")]
	private readonly ISubject<string> _onLinkClickedSubject;

	[Token(Token = "0x4000447")]
	[FieldOffset(Offset = "0x828")]
	private IInputManager _inputManager;

	[Token(Token = "0x4000448")]
	[FieldOffset(Offset = "0x830")]
	private int _selectedLink;

	[Token(Token = "0x4000449")]
	[FieldOffset(Offset = "0x838")]
	private IDisposable _disposable;

	[Token(Token = "0x17000077")]
	private IInputManager InputManager
	{
		[Token(Token = "0x6000367")]
		[Address(RVA = "0x364FC0", Offset = "0x3639C0", VA = "0x180364FC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000368")]
	[Address(RVA = "0x365090", Offset = "0x363A90", VA = "0x180365090", Slot = "8")]
	protected override void OnDestroy()
	{
	}

	[Token(Token = "0x6000369")]
	[Address(RVA = "0x3650F0", Offset = "0x363AF0", VA = "0x1803650F0", Slot = "156")]
	protected override void OnLinkClicked(TMP_LinkInfo linkInfo)
	{
	}

	[Token(Token = "0x600036A")]
	[Address(RVA = "0x365190", Offset = "0x363B90", VA = "0x180365190", Slot = "158")]
	protected override string ProcessText(string content)
	{
		return null;
	}

	[Token(Token = "0x600036B")]
	[Address(RVA = "0x3133E0", Offset = "0x311DE0", VA = "0x1803133E0", Slot = "160")]
	protected override string ProcessRubyTags(string content)
	{
		return null;
	}

	[Token(Token = "0x600036C")]
	[Address(RVA = "0x3656E0", Offset = "0x3640E0", VA = "0x1803656E0")]
	private void ModifyLinkedTextColor(Vector2 position, Camera renderCamera)
	{
	}

	[Token(Token = "0x600036D")]
	[Address(RVA = "0x365EB0", Offset = "0x3648B0", VA = "0x180365EB0")]
	public IObservable<string> OnLinkClickedAsObservable()
	{
		return null;
	}

	[Token(Token = "0x600036E")]
	[Address(RVA = "0x365F20", Offset = "0x364920", VA = "0x180365F20")]
	public DebateText()
	{
	}
}
