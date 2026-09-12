using System;
using System.Runtime.InteropServices;
using System.Text;
using Il2CppDummyDll;
using Naninovel;
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

	[Token(Token = "0x1700007F")]
	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
	{
		[Token(Token = "0x6000398")]
		[Address(RVA = "0x369D80", Offset = "0x368780", VA = "0x180369D80")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000080")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x6000399")]
		[Address(RVA = "0x369E50", Offset = "0x368850", VA = "0x180369E50")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000081")]
	private ICustomVariableManager NaniVarManager
	{
		[Token(Token = "0x600039A")]
		[Address(RVA = "0x369F20", Offset = "0x368920", VA = "0x180369F20")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600039B")]
	[Address(RVA = "0x369FF0", Offset = "0x3689F0", VA = "0x180369FF0", Slot = "83")]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600039C")]
	[Address(RVA = "0x36A090", Offset = "0x368A90", VA = "0x18036A090", Slot = "44")]
	public override UniTask ChangeVisibility(bool visible, [Optional] float? duration, [Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600039D")]
	[Address(RVA = "0x36A100", Offset = "0x368B00", VA = "0x18036A100")]
	private void OnVisibilityChangedByCustomUi(bool visible)
	{
	}

	[Token(Token = "0x600039E")]
	[Address(RVA = "0x36A6A0", Offset = "0x3690A0", VA = "0x18036A6A0")]
	public void OnCurrentHpChanged(float currentHp)
	{
	}

	[Token(Token = "0x600039F")]
	[Address(RVA = "0x36A900", Offset = "0x369300", VA = "0x18036A900")]
	private string TimeSpanToText(TimeSpan timeSpan)
	{
		return null;
	}

	[Token(Token = "0x60003A0")]
	[Address(RVA = "0x36AD90", Offset = "0x369790", VA = "0x18036AD90")]
	private void AppendSpriteTag(char c)
	{
	}

	[Token(Token = "0x60003A1")]
	[Address(RVA = "0x36AE30", Offset = "0x369830", VA = "0x18036AE30")]
	public DebateUi()
	{
	}
}
