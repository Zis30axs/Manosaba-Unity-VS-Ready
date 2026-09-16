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
	private readonly ISubject<string> _onLinkClickedSubject = new Subject<string>();

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
		// RECONSTRUCTED | Confidence: A | RVA: 0x364FC0, 202 B
		// Lazy cache on `+0x828`: `ServiceLocator.Get<IInputManager>(true)`.
		// Unlike `DebateTextPrinterPanel.LocalizationProvider`, the first
		// argument here is `mov cl, 1`, i.e. `includeDerivedType: true` rather
		// than the default. `IInputManager` is GigaCreation's (this file
		// imports `GigaCreation.Essentials.Input`, not `Naninovel`).
		[Token(Token = "0x6000367")]
		[Address(RVA = "0x364FC0", Offset = "0x3639C0", VA = "0x180364FC0")]
		get
		{
			return _inputManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<IInputManager>(true);
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x365090, 89 B
	// `base.OnDestroy()` (the direct call lands in `TextMeshProUGUI.OnDestroy`,
	// the nearest base override), then, if `_disposable` (`+0x838`) is
	// non-null, a tail call to `IDisposable.Dispose` on it. No method
	// reconstructed so far writes `_disposable`.
	[Token(Token = "0x6000368")]
	[Address(RVA = "0x365090", Offset = "0x363A90", VA = "0x180365090", Slot = "8")]
	protected override void OnDestroy()
	{
		base.OnDestroy();
		_disposable?.Dispose();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3650F0, 150 B
	// `base.OnLinkClicked(linkInfo)` (direct call to
	// `NaninovelTMProText.OnLinkClicked`, the `TMP_LinkInfo` copied by
	// value), then `_onLinkClickedSubject` (`+0x820`) is loaded, then
	// `linkInfo.GetLinkID()`, then a tail jump through the interface-slot
	// helper to `IObserver<string>` index 0, which dump.cs lists as
	// `OnNext`. The subject is loaded before the `GetLinkID()` call, matching
	// receiver-first evaluation. This is the source end of the link-click
	// chain whose sink is `DebateTextPrinterPanel.Initialize`'s subscription
	// (link id -> `Gosub`).
	[Token(Token = "0x6000369")]
	[Address(RVA = "0x3650F0", Offset = "0x363AF0", VA = "0x1803650F0", Slot = "156")]
	protected override void OnLinkClicked(TMP_LinkInfo linkInfo)
	{
		base.OnLinkClicked(linkInfo);
		_onLinkClickedSubject.OnNext(linkInfo.GetLinkID());
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x365190, 1355 B (+ closure lambda 0x366010)
	// Does not call `base.ProcessText`; it re-runs the tag passes itself.
	// - The closure (`<>c__DisplayClass8_0`: `<>4__this` +0x10, `renderCamera`
	//   +0x18) is allocated at entry.
	// - `_disposable` (+0x838) is disposed through IDisposable when non-null;
	//   `_selectedLink` (+0x830) = -1.
	// - Unless `content` is null or empty (inlined length test), it is threaded
	//   through six virtual calls on `this`, in byte order: vtable +0xB58 slot
	//   162 `ProcessExpressionTags`, +0xB28 slot 159 `ProcessLinkTags`, +0xB68
	//   slot 163 `ProcessTipTags`, +0xB38 slot 160 `ProcessRubyTags` (this
	//   class's pass-through override), +0xB48 slot 161 `ProcessEventTags`,
	//   +0xB88 slot 165 `FixArabic`. Slot names are from dump.cs
	//   `NaninovelTMProText`.
	// - The inlined `string.IsNullOrWhiteSpace` (null test plus
	//   `char.IsWhiteSpace` loop) true -> `_disposable = null`, return.
	// - Otherwise `TopmostCanvas` (+0xAA8 slot 151) is read twice: render mode 0
	//   (ScreenSpaceOverlay) leaves the camera null, else `worldCamera`.
	// - Lazy `InputManager` (inlined getter) -> `sub_6E10` = GigaCreation
	//   IInputManager slot 1 `GetAction(actionName: "Point", actionMapName:
	//   "UI")` -> `InputSystemExtensions.AsObservable` -> UniRx `Subscribe` ->
	//   `AddTo<T>(T, Component)` (0x67F4A0) with `this`, stored to `_disposable`.
	// - The lambda reads `context.ReadValue<Vector2>()` and tail-calls
	//   `ModifyLinkedTextColor(position, renderCamera)`.
	// The same processed string is returned on both paths.
	[Token(Token = "0x600036A")]
	[Address(RVA = "0x365190", Offset = "0x363B90", VA = "0x180365190", Slot = "158")]
	protected override string ProcessText(string content)
	{
		_disposable?.Dispose();
		_selectedLink = -1;
		if (!string.IsNullOrEmpty(content))
		{
			content = FixArabic(ProcessEventTags(ProcessRubyTags(ProcessTipTags(ProcessLinkTags(ProcessExpressionTags(content))))));
		}
		if (string.IsNullOrWhiteSpace(content))
		{
			_disposable = null;
			return content;
		}
		Camera renderCamera = TopmostCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : TopmostCanvas.worldCamera;
		_disposable = InputManager.GetAction("Point", "UI")
			.AsObservable()
			.Subscribe((UnityEngine.InputSystem.InputAction.CallbackContext context) => ModifyLinkedTextColor(context.ReadValue<Vector2>(), renderCamera))
			.AddTo(this);
		return content;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3133E0 (shared 4-byte body)
	// `mov rax, rdx; ret`: returns `content` unchanged, in place of whatever
	// the base `NaninovelTMProText.ProcessRubyTags` does (not examined). The
	// native body is folded with unrelated "return the first argument"
	// methods (e.g. `Exception.StripFileInfo`), so it is not counted as
	// progress.
	[Token(Token = "0x600036B")]
	[Address(RVA = "0x3133E0", Offset = "0x311DE0", VA = "0x1803133E0", Slot = "160")]
	protected override string ProcessRubyTags(string content)
	{
		return content;
	}

	// RECONSTRUCTED | Confidence: A (behavior) | RVA: 0x3656E0, 1992 B
	// Hover highlight for <link> text, driven by the "UI/Point" subscription
	// in `ProcessText`.
	// - `TMP_TextUtilities.FindIntersectingLink(this, position, renderCamera)`
	//   with position (x, y, 0).
	// - Un-highlight when `_selectedLink` (+0x830) != -1 and the hit is -1 or
	//   a different link; the explicit -1 test is in the bytes. A hit on the
	//   same link jumps straight to the end.
	// - Highlight when the hit != -1 and differs from `_selectedLink`, which is
	//   set first.
	// - Both passes walk `linkInfo[k]` (+0x48; firstChar +0x14, length +0x18)
	//   and re-read `textInfo` every iteration. Whitespace characters (the
	//   inlined `char.IsWhiteSpace` on `character` +0x4) are skipped.
	//   Otherwise `meshInfo[materialReferenceIndex (+0x30)].colors32` (+0x38)
	//   at `vertexIndex` (+0x44) is tinted once with
	//   `TMPro_ExtensionMethods.Tint(Color32, float)` and written to all four
	//   quad vertices.
	// - The factors are rdata floats: un-highlight 0x2AE2878 = `8faaaa3f` =
	//   1.33333f exactly (not 4f/3f, which would be 0x3FAAAAAB); highlight
	//   0x2AE2780 = 0.75f. Restoring divides only approximately, so repeated
	//   hovering can drift the colour slightly.
	// - Each pass ends with vtable +0x7F8 = slot 108
	//   `UpdateVertexData(TMP_VertexDataUpdateFlags)` with 0xFF = `All`, even
	//   when the link has no characters. The un-highlight pass then stores -1.
	// The two TMP_CharacterInfo stack copies per iteration, and the loop form
	// (counter vs. char index), are codegen-level and Confidence B.
	[Token(Token = "0x600036C")]
	[Address(RVA = "0x3656E0", Offset = "0x3640E0", VA = "0x1803656E0")]
	private void ModifyLinkedTextColor(Vector2 position, Camera renderCamera)
	{
		int linkIndex = TMP_TextUtilities.FindIntersectingLink(this, position, renderCamera);
		if (_selectedLink != -1 && (linkIndex == -1 || linkIndex != _selectedLink))
		{
			TMP_LinkInfo selectedLinkInfo = textInfo.linkInfo[_selectedLink];
			for (int i = 0; i < selectedLinkInfo.linkTextLength; i++)
			{
				TMP_CharacterInfo characterInfo = textInfo.characterInfo[selectedLinkInfo.linkTextfirstCharacterIndex + i];
				if (char.IsWhiteSpace(characterInfo.character))
				{
					continue;
				}
				Color32[] colors = textInfo.meshInfo[characterInfo.materialReferenceIndex].colors32;
				int vertexIndex = characterInfo.vertexIndex;
				Color32 color = colors[vertexIndex].Tint(1.33333f);
				colors[vertexIndex] = color;
				colors[vertexIndex + 1] = color;
				colors[vertexIndex + 2] = color;
				colors[vertexIndex + 3] = color;
			}
			UpdateVertexData(TMP_VertexDataUpdateFlags.All);
			_selectedLink = -1;
		}
		if (linkIndex != -1 && linkIndex != _selectedLink)
		{
			_selectedLink = linkIndex;
			TMP_LinkInfo linkInfo = textInfo.linkInfo[linkIndex];
			for (int i = 0; i < linkInfo.linkTextLength; i++)
			{
				TMP_CharacterInfo characterInfo = textInfo.characterInfo[linkInfo.linkTextfirstCharacterIndex + i];
				if (char.IsWhiteSpace(characterInfo.character))
				{
					continue;
				}
				Color32[] colors = textInfo.meshInfo[characterInfo.materialReferenceIndex].colors32;
				int vertexIndex = characterInfo.vertexIndex;
				Color32 color = colors[vertexIndex].Tint(0.75f);
				colors[vertexIndex] = color;
				colors[vertexIndex + 1] = color;
				colors[vertexIndex + 2] = color;
				colors[vertexIndex + 3] = color;
			}
			UpdateVertexData(TMP_VertexDataUpdateFlags.All);
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x365EB0, 107 B
	// Loads `_onLinkClickedSubject` (`+0x820`) and tail-calls UniRx
	// `Observable.AsObservable<string>`. The same body is inlined verbatim
	// into `DebateTextPrinterPanel.<Initialize>b__26_0`.
	[Token(Token = "0x600036D")]
	[Address(RVA = "0x365EB0", Offset = "0x3648B0", VA = "0x180365EB0")]
	public IObservable<string> OnLinkClickedAsObservable()
	{
		return _onLinkClickedSubject.AsObservable();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x365F20, 234 B
	// Allocates a `UniRx.Subject<string>`, stores it to `_onLinkClickedSubject`
	// (`+0x820`), and only then tail-jumps into `NaninovelTMProText..ctor`.
	// The store precedes the base constructor call, so it is the
	// `= new Subject<string>()` field initializer; the constructor body is
	// empty and no other field has an initializer.
	[Token(Token = "0x600036E")]
	[Address(RVA = "0x365F20", Offset = "0x364920", VA = "0x180365F20")]
	public DebateText()
	{
	}
}
