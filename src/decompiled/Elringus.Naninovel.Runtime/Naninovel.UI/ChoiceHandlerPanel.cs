using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x200050D")]
[RequireComponent(typeof(CanvasGroup))]
public class ChoiceHandlerPanel : CustomUI, IManagedUI, ILocalizableUI
{
	[Serializable]
	[Token(Token = "0x200050E")]
	public new class GameState
	{
		[Token(Token = "0x40013A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public List<ChoiceState> Buttons;

		[Token(Token = "0x6001946")]
		[Address(RVA = "0x6175B0", Offset = "0x615FB0", VA = "0x1806175B0")]
		public GameState()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000517")]
	[CompilerGenerated]
	private struct _003CDeserializeState_003Ed__29
{
		[Token(Token = "0x40013B8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40013B9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40013BA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ChoiceHandlerPanel _003C_003E4__this;

		[Token(Token = "0x40013BB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public GameStateMap stateMap;

		[Token(Token = "0x40013BC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600195A")]
		[Address(RVA = "0x618200", Offset = "0x616C00", VA = "0x180618200", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600195B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000518")]
	[CompilerGenerated]
	private struct _003CHandleLocalizationChanged_003Ed__27
{
		[Token(Token = "0x40013BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40013BE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40013BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ChoiceHandlerPanel _003C_003E4__this;

		[Token(Token = "0x40013C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600195C")]
		[Address(RVA = "0x618CF0", Offset = "0x6176F0", VA = "0x180618CF0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600195D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40013A0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	[Tooltip("Container that will hold spawned choice buttons.")]
	[SerializeField]
	private RectTransform buttonsContainer;

	[Token(Token = "0x40013A1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	[Tooltip("Button prototype to use by default.")]
	[SerializeField]
	private ChoiceHandlerButton defaultButtonPrefab;

	[Token(Token = "0x40013A2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	[Tooltip("Whether to focus added choice buttons for keyboard and gamepad control.")]
	[SerializeField]
	private bool focusChoiceButtons;

	[Token(Token = "0x40013A3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	private IResourceLoader<GameObject> customButtonLoader;

	[Token(Token = "0x40013A4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	private bool removeAllButtonsPending;

	[Token(Token = "0x1700039B")]
	protected virtual List<ChoiceHandlerButton> ChoiceButtons
	{
		[Token(Token = "0x6001932")]
		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "96")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700039C")]
	protected virtual RectTransform ButtonsContainer
	{
		[Token(Token = "0x6001933")]
		[Address(RVA = "0x49D2B0", Offset = "0x49BCB0", VA = "0x18049D2B0", Slot = "97")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700039D")]
	protected virtual ChoiceHandlerButton DefaultButtonPrefab
	{
		[Token(Token = "0x6001934")]
		[Address(RVA = "0x49D250", Offset = "0x49BC50", VA = "0x18049D250", Slot = "98")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700039E")]
	protected virtual bool FocusChoiceButtons
	{
		[Token(Token = "0x6001935")]
		[Address(RVA = "0x60E340", Offset = "0x60CD40", VA = "0x18060E340", Slot = "99")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1400008D")]
	public event Action<ChoiceState> OnChoice
	{
		[Token(Token = "0x6001930")]
		[Address(RVA = "0x615660", Offset = "0x614060", VA = "0x180615660")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001931")]
		[Address(RVA = "0x615780", Offset = "0x614180", VA = "0x180615780")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6001936")]
	[Address(RVA = "0x6158A0", Offset = "0x6142A0", VA = "0x1806158A0", Slot = "68")]
	private UniTask Naninovel_002EUI_002EIManagedUI_002EChangeVisibility(bool visible, float? duration, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001937")]
	[Address(RVA = "0x615950", Offset = "0x614350", VA = "0x180615950", Slot = "100")]
	public virtual void AddChoiceButton(ChoiceState choice)
	{
	}

	[Token(Token = "0x6001938")]
	[Address(RVA = "0x615F30", Offset = "0x614930", VA = "0x180615F30", Slot = "101")]
	public virtual void RemoveChoiceButton(string id)
	{
	}

	[Token(Token = "0x6001939")]
	[Address(RVA = "0x6162A0", Offset = "0x614CA0", VA = "0x1806162A0", Slot = "102")]
	public virtual void RemoveAllChoiceButtonsDelayed()
	{
	}

	[Token(Token = "0x600193A")]
	[Address(RVA = "0x616360", Offset = "0x614D60", VA = "0x180616360", Slot = "103")]
	public virtual void RemoveAllChoiceButtons()
	{
	}

	[Token(Token = "0x600193B")]
	[Address(RVA = "0x6165D0", Offset = "0x614FD0", VA = "0x1806165D0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x600193C")]
	[Address(RVA = "0x6167A0", Offset = "0x6151A0", VA = "0x1806167A0", Slot = "104")]
	protected virtual ChoiceHandlerButton LoadCustomButtonPrefab(string path)
	{
		return null;
	}

	[Token(Token = "0x600193D")]
	[Address(RVA = "0x616A10", Offset = "0x615410", VA = "0x180616A10", Slot = "105")]
	protected virtual bool ShouldFocusAddedButton(ChoiceHandlerButton _)
	{
		return default(bool);
	}

	[Token(Token = "0x600193E")]
	[Address(RVA = "0x616A90", Offset = "0x615490", VA = "0x180616A90", Slot = "106")]
	protected virtual void FocusAddedButton(ChoiceHandlerButton button)
	{
	}

	[Token(Token = "0x600193F")]
	[Address(RVA = "0x616D20", Offset = "0x615720", VA = "0x180616D20", Slot = "86")]
	[AsyncStateMachine(typeof(_003CHandleLocalizationChanged_003Ed__27))]
	public override UniTask HandleLocalizationChanged(LocaleChangedArgs _)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001940")]
	[Address(RVA = "0x616E40", Offset = "0x615840", VA = "0x180616E40", Slot = "87")]
	protected override void SerializeState(GameStateMap stateMap)
	{
	}

	[Token(Token = "0x6001941")]
	[Address(RVA = "0x617230", Offset = "0x615C30", VA = "0x180617230", Slot = "88")]
	[AsyncStateMachine(typeof(_003CDeserializeState_003Ed__29))]
	protected override UniTask DeserializeState(GameStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001942")]
	[Address(RVA = "0x36BA60", Offset = "0x36A460", VA = "0x18036BA60")]
	public ChoiceHandlerPanel()
	{
	}
}
