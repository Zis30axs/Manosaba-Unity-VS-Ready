using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Commands;
using Naninovel.UI;
using TMPro;
using UniRx;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x20000E8")]
public class DebateUi : CustomUI, IDebateUi, IManagedUI
{
	[Token(Token = "0x4000472")]
	private const string SubroutineScriptPath = "System/System_Subroutine";

	[Token(Token = "0x4000473")]
	private const string TimeIsUpLabelName = "TimeIsUp";

	[Token(Token = "0x4000474")]
	private const string MaxHpVarName = "maxHp";

	[Token(Token = "0x4000475")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	[Header("References for Debate")]
	[SerializeField]
	private TextMeshProUGUI _label;

	[Token(Token = "0x4000476")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	[SerializeField]
	private SkipDebateButton _skipDebateButton;

	[Token(Token = "0x4000477")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	private readonly StringBuilder _builder;

	[Token(Token = "0x4000478")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

	[Token(Token = "0x4000479")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x400047A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	private ICustomVariableManager _naniVarManager;

	[Token(Token = "0x400047B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	private CompositeDisposable _disposables;

	// RECONSTRUCTED | Confidence: A | RVA: 0x369D80
	// `_field ??= ServiceLocator.Get<T>()` -- same lazy-cache shape as the other
	// GigaCreation.Tools.Service-backed accessors elsewhere in this project.
	// Note the call uses the default `includeDerivedType: false` overload
	// (`xor ecx,ecx` before the call), unlike some other lazy caches in this
	// codebase that pass `true`.
	[Token(Token = "0x1700007F")]
	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
	{
		[Token(Token = "0x6000398")]
		[Address(RVA = "0x369D80", Offset = "0x368780", VA = "0x180369D80")]
		get
		{
			return _witchTrialsNaninovelManager ??= ServiceLocator.Get<IWitchTrialsNaninovelManager>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x369E50
	[Token(Token = "0x17000080")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x6000399")]
		[Address(RVA = "0x369E50", Offset = "0x368850", VA = "0x180369E50")]
		get
		{
			return _naniScriptPlayer ??= Engine.GetServiceOrErr<IScriptPlayer>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x369F20
	[Token(Token = "0x17000081")]
	private ICustomVariableManager NaniVarManager
	{
		[Token(Token = "0x600039A")]
		[Address(RVA = "0x369F20", Offset = "0x368920", VA = "0x180369F20")]
		get
		{
			return _naniVarManager ??= Engine.GetServiceOrErr<ICustomVariableManager>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x369FF0
	//
	// Native evidence: reaches into `_skipDebateButton` and reproduces, field for
	// field, the exact null-check/constant-folded shape of
	// `SkipDebateButton::SetIconShadowEnabled(false)` (RVA 0x36B860, verified by
	// independent disassembly: same `_iconShadow.Size = 0f` then
	// `_iconImage.color = Color.white` sequence, `data_2AE36E0` = (1,1,1,1)).
	// IL2Cpp inlined the callee's `false`-branch body at this call site; written
	// here as a call to the named method rather than reproducing its internals,
	// per this project's inlining convention.
	[Token(Token = "0x600039B")]
	[Address(RVA = "0x369FF0", Offset = "0x3689F0", VA = "0x180369FF0", Slot = "83")]
	public override UniTask Initialize()
	{
		_skipDebateButton.SetIconShadowEnabled(false);
		return default(UniTask);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36A090
	[Token(Token = "0x600039C")]
	[Address(RVA = "0x36A090", Offset = "0x368A90", VA = "0x18036A090", Slot = "44")]
	public override UniTask ChangeVisibility(bool visible, [Optional] float? duration, [Optional] AsyncToken asyncToken)
	{
		OnVisibilityChangedByCustomUi(visible);
		return base.ChangeVisibility(visible, duration, asyncToken);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36A100
	//
	// Native evidence:
	//   - visible == false: `_disposables.Clear(); _disposables.Dispose();
	//     _disposables = null;` then `NaniScriptPlayer.OnSkip -=
	//     _skipDebateButton.SetIconShadowEnabled;` -- `IScriptPlayer.OnSkip`'s
	//     add/remove pair is dump.cs Slot 8/9 exactly (confirmed against
	//     `src/il2cpp/dump.cs` line ~171692, not guessed from declaration order).
	//   - visible == true: fresh `CompositeDisposable`, then three `.Subscribe(...)
	//     .AddTo(_disposables)` chains and one `+=` subscription:
	//       * `WitchTrialsNaninovelManager.OnRemainingTimeChangedAsObservable()`
	//         (dump.cs Slot 9 on `IWitchTrialsNaninovelManager`) -> `b__18_0`
	//       * `WitchTrialsNaninovelManager.OnDebateSkippableChangedAsObservable()`
	//         (Slot 10) -> `b__18_1`
	//       * `_skipDebateButton.OnPointerDownAsObservable()` -> `b__18_2`
	//         (verified byte-identical to `SkipDebateButton::OnPointerDownAsObservable`,
	//         called by name rather than re-inlined)
	//       * `_skipDebateButton.OnPointerUpAsObservable()` -> `b__18_3`
	//       * `NaniScriptPlayer.OnSkip += _skipDebateButton.SetIconShadowEnabled;`
	[Token(Token = "0x600039D")]
	[Address(RVA = "0x36A100", Offset = "0x368B00", VA = "0x18036A100")]
	private void OnVisibilityChangedByCustomUi(bool visible)
	{
		if (!visible)
		{
			_disposables?.Clear();
			_disposables?.Dispose();
			_disposables = null;
			NaniScriptPlayer.OnSkip -= _skipDebateButton.SetIconShadowEnabled;
			return;
		}
		_disposables = new CompositeDisposable();
		WitchTrialsNaninovelManager.OnRemainingTimeChangedAsObservable()
			.Subscribe(new Action<TimeSpan>(_003COnVisibilityChangedByCustomUi_003Eb__18_0))
			.AddTo(_disposables);
		WitchTrialsNaninovelManager.OnDebateSkippableChangedAsObservable()
			.Subscribe(new Action<bool>(_003COnVisibilityChangedByCustomUi_003Eb__18_1))
			.AddTo(_disposables);
		_skipDebateButton.OnPointerDownAsObservable()
			.Subscribe(new Action<Unit>(_003COnVisibilityChangedByCustomUi_003Eb__18_2))
			.AddTo(_disposables);
		_skipDebateButton.OnPointerUpAsObservable()
			.Subscribe(new Action<Unit>(_003COnVisibilityChangedByCustomUi_003Eb__18_3))
			.AddTo(_disposables);
		NaniScriptPlayer.OnSkip += _skipDebateButton.SetIconShadowEnabled;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36AEF0 (compiler-generated closure,
	// dump.cs: DebateUi.<>c__DisplayClassNN.<OnVisibilityChangedByCustomUi>b__18_0)
	//
	// Native evidence: `_label.SetText(TimeSpanToText(timeSpan))` unconditionally
	// (throws NRE if `_label` is null -- not a `?.`); then compares `timeSpan`
	// against `TimeSpan.Zero` and returns early unless `timeSpan <= TimeSpan.Zero`.
	// On timeout, builds `new Goto { Path = new NamedString(SubroutineScriptPath,
	// TimeIsUpLabelName), ResetState = new List<string> { Goto.NoResetFlag } }`
	// (field offsets 0x30/0x38 confirmed against `Naninovel.Commands.Goto`'s own
	// declaration; `Goto.NoResetFlag == "-"`, matching the literal string operand)
	// and calls `.Execute(default).Forget()`.
	private void _003COnVisibilityChangedByCustomUi_003Eb__18_0(TimeSpan timeSpan)
	{
		_label.SetText(TimeSpanToText(timeSpan));
		if (timeSpan > TimeSpan.Zero) return;
		new Goto
		{
			Path = new NamedString(SubroutineScriptPath, TimeIsUpLabelName),
			ResetState = new List<string> { Goto.NoResetFlag }
		}.Execute(default).Forget();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36B190
	//
	// Native evidence: byte-identical to `SkipDebateButton::Present()`
	// (RVA 0x36B6C0) when canSkip == true, and to `SkipDebateButton::Dismiss()`
	// (RVA 0x36B710) when canSkip == false -- both independently disassembled
	// and confirmed to touch the same `_canvasGroup.alpha/interactable/
	// blocksRaycasts` fields with matching constants (1f/true/true vs
	// 0f/false/false). Written as calls to the named methods.
	private void _003COnVisibilityChangedByCustomUi_003Eb__18_1(bool canSkip)
	{
		if (canSkip) _skipDebateButton.Present();
		else _skipDebateButton.Dismiss();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36B220
	// Native evidence: `NaniScriptPlayer.SetSkipEnabled(true)` -- selector 0x22
	// matches dump.cs `IScriptPlayer` Slot 34 = `SetSkipEnabled(bool)` exactly.
	// Fires on the skip button's pointer-DOWN (hold-to-skip).
	private void _003COnVisibilityChangedByCustomUi_003Eb__18_2(Unit _)
	{
		NaniScriptPlayer.SetSkipEnabled(true);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36B280
	// Same evidence as b__18_2, `enabled: false`, fires on pointer-UP.
	private void _003COnVisibilityChangedByCustomUi_003Eb__18_3(Unit _)
	{
		NaniScriptPlayer.SetSkipEnabled(false);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36A6A0
	//
	// Native evidence: `NaniVarManager.TryGetVariableValue<float>("maxHp", out
	// float maxHp)`; on success, `Mathf.Clamp(currentHp, 0f, maxHp)` (confirmed
	// by the exact `0 > x ? 0 : (x <= max ? x : max)` compare/branch shape) is
	// formatted as `"hp={0}"` and sent to `Debug.Log`; on failure,
	// `Debug.LogWarning("Failed to get maxHp variable.")`. Notably this method
	// does NOT touch `_label`/`_builder` in this build -- it is diagnostic-only,
	// not a UI update path. Not assumed from the method name; this is what the
	// disassembly shows.
	[Token(Token = "0x600039E")]
	[Address(RVA = "0x36A6A0", Offset = "0x3690A0", VA = "0x18036A6A0")]
	public void OnCurrentHpChanged(float currentHp)
	{
		if (NaniVarManager.TryGetVariableValue<float>("maxHp", out float maxHp))
		{
			Debug.Log(string.Format("hp={0}", Mathf.Clamp(currentHp, 0f, maxHp)));
		}
		else
		{
			Debug.LogWarning("Failed to get maxHp variable.");
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36A900
	//
	// Native evidence: builds "mm:ss:fff" into `_builder`, appending every digit
	// through `AppendSpriteTag` (so digits render as `<sprite name="N">` glyphs)
	// and the `:` separators as plain text. The minutes segment is
	// `(int)Math.Floor(timeSpan.TotalMinutes)` (confirmed: multiplies raw ticks
	// by the double constant `1.0/600000000` = `1.0/TimeSpan.TicksPerMinute`,
	// which is exactly .NET's own `TotalMinutes` formula, then floors and
	// truncates via a `Math.Floor`+`(int)` helper). The seconds/milliseconds
	// segments are computed directly from ticks via the standard magic-multiply
	// division sequences .NET emits for `TimeSpan.Seconds`/`.Milliseconds`
	// (D2/D3 padding matches). Ends with a tail-call to `_builder.ToString()`.
	[Token(Token = "0x600039F")]
	[Address(RVA = "0x36A900", Offset = "0x369300", VA = "0x18036A900")]
	private string TimeSpanToText(TimeSpan timeSpan)
	{
		_builder.Length = 0;
		int totalMinutes = (int)Math.Floor(timeSpan.TotalMinutes);
		foreach (char c in totalMinutes.ToString("D2")) AppendSpriteTag(c);
		_builder.Append(':');
		foreach (char c in timeSpan.Seconds.ToString("D2")) AppendSpriteTag(c);
		_builder.Append(':');
		foreach (char c in timeSpan.Milliseconds.ToString("D3")) AppendSpriteTag(c);
		return _builder.ToString();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36AD90
	[Token(Token = "0x60003A0")]
	[Address(RVA = "0x36AD90", Offset = "0x369790", VA = "0x18036AD90")]
	private void AppendSpriteTag(char c)
	{
		_builder.Append("<sprite name=\"").Append(c).Append("\">");
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36AE30
	[Token(Token = "0x60003A1")]
	[Address(RVA = "0x36AE30", Offset = "0x369830", VA = "0x18036AE30")]
	public DebateUi()
	{
		_builder = new StringBuilder();
	}
}
