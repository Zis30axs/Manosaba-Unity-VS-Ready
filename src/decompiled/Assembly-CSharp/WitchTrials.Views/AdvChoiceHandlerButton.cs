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
		[Token(Token = "0x60001B2")]
		[Address(RVA = "0x335F90", Offset = "0x334990", VA = "0x180335F90")]
		get
		{
			return null;
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

	[Token(Token = "0x60001B4")]
	[Address(RVA = "0x336130", Offset = "0x334B30", VA = "0x180336130", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x60001B5")]
	[Address(RVA = "0x3362C0", Offset = "0x334CC0", VA = "0x1803362C0", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x60001B6")]
	[Address(RVA = "0x336450", Offset = "0x334E50", VA = "0x180336450", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x60001B7")]
	[Address(RVA = "0x336590", Offset = "0x334F90", VA = "0x180336590", Slot = "63")]
	public override void Initialize(ChoiceState choiceState)
	{
	}

	[Token(Token = "0x60001B8")]
	[Address(RVA = "0x3368B0", Offset = "0x3352B0", VA = "0x1803368B0")]
	private void ChangeCriticalChoiceHintVisibility(bool enable)
	{
	}

	[Token(Token = "0x60001B9")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public AdvChoiceHandlerButton()
	{
	}
}
