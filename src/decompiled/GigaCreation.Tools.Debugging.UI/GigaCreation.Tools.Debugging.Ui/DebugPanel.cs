using GigaCreation.Tools.Debugging.Core;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Tools.Debugging.Ui;

[Token(Token = "0x2000005")]
public class DebugPanel : MonoBehaviour
{
	[Token(Token = "0x4000008")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Transform[] _contents;

	[Token(Token = "0x4000009")]
	[FieldOffset(Offset = "0x28")]
	[Space]
	[SerializeField]
	private CanvasGroup _showButtonCanvasGroup;

	[Token(Token = "0x400000A")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Button _showButton;

	[Token(Token = "0x400000B")]
	[FieldOffset(Offset = "0x38")]
	[SerializeField]
	private CanvasGroup _hideButtonCanvasGroup;

	[Token(Token = "0x400000C")]
	[FieldOffset(Offset = "0x40")]
	[SerializeField]
	private Button _hideButton;

	// RECONSTRUCTED | Confidence: A | RVA: 0xD47440, 994 B; lambdas <Start>b__5_0 0xD42C90 (vtable slot 4 = ShowContent),
	//   b__5_1 / b__5_3 0xD47F30 (slot 5 = HideContent), <>c.<Start>b__5_2 0x39B030 (`x => x`).
	// - ServiceLocator.TryGet<IDebugManager>(out local) false -> nothing is wired (release build).
	// - `_showButton` (+0x30) / `_hideButton` (+0x40) onClick.AsObservable().Subscribe(Show/HideContent).AddTo(this).
	// - IsDebugMode (IDebugManager slot 0).Where(x => x).Subscribe(_ => HideContent()).AddTo(this): every time debug
	//   mode turns on the panel starts collapsed, with only the show button visible.
	// Annotated disassembly: work/title-path/disas/dbgp_*.txt (regenerate with work/reversing/disas.py).
	[Token(Token = "0x6000007")]
	[Address(RVA = "0xD47440", Offset = "0xD45E40", VA = "0x180D47440")]
	private void Start()
	{
		if (!ServiceLocator.TryGet(out IDebugManager debugManager))
		{
			return;
		}
		_showButton.onClick.AsObservable().Subscribe(_ => ShowContent()).AddTo(this);
		_hideButton.onClick.AsObservable().Subscribe(_ => HideContent()).AddTo(this);
		debugManager.IsDebugMode.Where(x => x).Subscribe(_ => HideContent()).AddTo(this);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0xD47830, 896 B -- icalls Transform.set_localScale_Injected and
	//   CanvasGroup.set_alpha_Injected / set_blocksRaycasts_Injected (names read from the icall strings).
	// - every `_contents` (+0x20) entry: localScale = Vector3.one (Vector3 static +0xC = oneVector).
	// - show button group (+0x28): alpha 0, blocksRaycasts false; hide button group (+0x38): alpha 1 (data_2AE2820),
	//   blocksRaycasts true.
	[Token(Token = "0x6000008")]
	[Address(RVA = "0xD47830", Offset = "0xD46230", VA = "0x180D47830", Slot = "4")]
	public virtual void ShowContent()
	{
		foreach (Transform content in _contents)
		{
			content.localScale = Vector3.one;
		}
		_showButtonCanvasGroup.alpha = 0f;
		_showButtonCanvasGroup.blocksRaycasts = false;
		_hideButtonCanvasGroup.alpha = 1f;
		_hideButtonCanvasGroup.blocksRaycasts = true;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0xD47BB0, 896 B -- mirror of ShowContent: contents scale to Vector3.zero
	//   (static +0x0), show button group alpha 1 / blocksRaycasts true, hide button group alpha 0 / false (tail jump).
	[Token(Token = "0x6000009")]
	[Address(RVA = "0xD47BB0", Offset = "0xD465B0", VA = "0x180D47BB0", Slot = "5")]
	public virtual void HideContent()
	{
		foreach (Transform content in _contents)
		{
			content.localScale = Vector3.zero;
		}
		_showButtonCanvasGroup.alpha = 1f;
		_showButtonCanvasGroup.blocksRaycasts = true;
		_hideButtonCanvasGroup.alpha = 0f;
		_hideButtonCanvasGroup.blocksRaycasts = false;
	}

	[Token(Token = "0x600000A")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugPanel()
	{
	}
}
