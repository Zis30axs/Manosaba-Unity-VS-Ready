using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000087")]
public class AdvChoiceHandlerButton : ChoiceHandlerButton
{
	[Token(Token = "0x4000211")]
	[FieldOffset(Offset = "0x108")]
	[Header("ADV Choice Handler Button")]
	[SerializeField]
	private CanvasGroup _criticalChoiceHintCanvasGroup;

	[Token(Token = "0x4000212")]
	[FieldOffset(Offset = "0x110")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _sfxPath;

	[Token(Token = "0x4000213")]
	[FieldOffset(Offset = "0x118")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x4000214")]
	[FieldOffset(Offset = "0x120")]
	private IWitchTrialsChoiceHandlerManager _witchTrialsChoiceHandlerManager;

	[Token(Token = "0x17000028")]
	private IAudioManager NaniAudioManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x335F90
		// Lazy cache in +0x118 via `Engine.GetServiceOrErr<IAudioManager>()`.
		[Token(Token = "0x60001B2")]
		[Address(RVA = "0x335F90", Offset = "0x334990", VA = "0x180335F90")]
		get
		{
			return _naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();
		}
	}

	[Token(Token = "0x17000029")]
	private IWitchTrialsChoiceHandlerManager WitchTrialsChoiceHandlerManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x336060, 202 B
		// Lazy cache in +0x120 via `Naninovel.Engine.GetServiceOrErr<IWitchTrialsChoiceHandlerManager>()`.
		[Token(Token = "0x60001B3")]
		[Address(RVA = "0x336060", Offset = "0x334A60", VA = "0x180336060")]
		get
		{
			return _witchTrialsChoiceHandlerManager ??= Naninovel.Engine.GetServiceOrErr<IWitchTrialsChoiceHandlerManager>();
		}
	}

	// RECONSTRUCTED | Confidence: A (null-test spelling: B) | RVA: 0x336130
	// `base.OnEnable()` = ScriptableUIControl<Button>.OnEnable inlined (virtual slot 60 BindUIEvents on this); then a
	// liveness test on `_criticalChoiceHintCanvasGroup` (+0x108) gates IWitchTrialsChoiceHandlerManager slot 2
	// add_OnCriticalChoiceHintChanged(ChangeCriticalChoiceHintVisibility).
	[Token(Token = "0x60001B4")]
	[Address(RVA = "0x336130", Offset = "0x334B30", VA = "0x180336130", Slot = "5")]
	protected override void OnEnable()
	{
		base.OnEnable();
		if (_criticalChoiceHintCanvasGroup != null)
		{
			WitchTrialsChoiceHandlerManager.OnCriticalChoiceHintChanged += ChangeCriticalChoiceHintVisibility;
		}
	}

	// RECONSTRUCTED | Confidence: A (null-test spelling: B) | RVA: 0x3362C0
	// Mirror of OnEnable: inlined ScriptableUIControl<Button>.OnDisable (slot 61 UnbindUIEvents), then slot 3
	// remove_OnCriticalChoiceHintChanged behind the same liveness test.
	[Token(Token = "0x60001B5")]
	[Address(RVA = "0x3362C0", Offset = "0x334CC0", VA = "0x1803362C0", Slot = "7")]
	protected override void OnDisable()
	{
		base.OnDisable();
		if (_criticalChoiceHintCanvasGroup != null)
		{
			WitchTrialsChoiceHandlerManager.OnCriticalChoiceHintChanged -= ChangeCriticalChoiceHintVisibility;
		}
	}

	// RECONSTRUCTED | Confidence: A (absent base call: B) | RVA: 0x336450
	// NaniAudioManager (inlined) PlaySfx(_sfxPath +0x110, 1f, 0f, false, null, default) -> Naninovel ForgetCore. No base
	// call is visible, but ScriptableButton.OnButtonClick is the shared empty body 0x30C000, so one would vanish anyway.
	[Token(Token = "0x60001B6")]
	[Address(RVA = "0x336450", Offset = "0x334E50", VA = "0x180336450", Slot = "62")]
	protected override void OnButtonClick()
	{
		NaniAudioManager.PlaySfx(_sfxPath).Forget();
	}

	// RECONSTRUCTED | Confidence: A (tail call spelling: B) | RVA: 0x336590
	// - ChoiceHandlerButton.Initialize inlined byte-for-byte (0x615400: ChoiceState store at +0xA0, OnSummaryTextChanged
	//   +0x90 and onSummaryTextChanged +0xF8 with the summary string, OnLock +0x98 and onLock +0x100 with Locked).
	// - Liveness test on `_criticalChoiceHintCanvasGroup`, then IWitchTrialsChoiceHandlerManager slot 0
	//   CriticalChoiceHintEnabled -> alpha 1f / 0f: ChangeCriticalChoiceHintVisibility inlined.
	[Token(Token = "0x60001B7")]
	[Address(RVA = "0x336590", Offset = "0x334F90", VA = "0x180336590", Slot = "63")]
	public override void Initialize(ChoiceState choiceState)
	{
		base.Initialize(choiceState);
		if (_criticalChoiceHintCanvasGroup != null)
		{
			ChangeCriticalChoiceHintVisibility(WitchTrialsChoiceHandlerManager.CriticalChoiceHintEnabled);
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3368B0
	// `_criticalChoiceHintCanvasGroup` (+0x108).alpha = enable ? 1f : 0f.
	[Token(Token = "0x60001B8")]
	[Address(RVA = "0x3368B0", Offset = "0x3352B0", VA = "0x1803368B0")]
	private void ChangeCriticalChoiceHintVisibility(bool enable)
	{
		_criticalChoiceHintCanvasGroup.alpha = enable ? 1f : 0f;
	}

	[Token(Token = "0x60001B9")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public AdvChoiceHandlerButton()
	{
	}
}
