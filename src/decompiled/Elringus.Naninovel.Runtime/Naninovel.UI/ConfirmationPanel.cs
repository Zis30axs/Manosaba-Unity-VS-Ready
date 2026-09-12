using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naninovel.UI;

[Token(Token = "0x2000528")]
public class ConfirmationPanel : CustomUI, IConfirmationUI, IManagedUI, ILocalizableUI
{
	[Serializable]
	[Token(Token = "0x2000529")]
	private class OnMessageChangedEvent : UnityEvent<string>
	{
		[Token(Token = "0x60019B3")]
		[Address(RVA = "0x61DCB0", Offset = "0x61C6B0", VA = "0x18061DCB0")]
		public OnMessageChangedEvent()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200052A")]
	[CompilerGenerated]
	private struct _003CConfirm_003Ed__16
{
		[Token(Token = "0x40013FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001400")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

		[Token(Token = "0x4001401")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public ConfirmationPanel _003C_003E4__this;

		[Token(Token = "0x4001402")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string message;

		[Token(Token = "0x4001403")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60019B4")]
		[Address(RVA = "0x61DCF0", Offset = "0x61C6F0", VA = "0x18061DCF0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60019B5")]
		[Address(RVA = "0x61E1A0", Offset = "0x61CBA0", VA = "0x18061E1A0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200052B")]
	[CompilerGenerated]
	private struct _003CNotify_003Ed__17
{
		[Token(Token = "0x4001404")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001405")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001406")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ConfirmationPanel _003C_003E4__this;

		[Token(Token = "0x4001407")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string message;

		[Token(Token = "0x4001408")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60019B6")]
		[Address(RVA = "0x61E1D0", Offset = "0x61CBD0", VA = "0x18061E1D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60019B7")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40013FB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	[Tooltip("Used to agree on confirmation dialogue.")]
	[SerializeField]
	private Button confirmButton;

	[Token(Token = "0x40013FC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	[Tooltip("Used to cancel on confirmation dialogue.")]
	[SerializeField]
	private Button cancelButton;

	[Token(Token = "0x40013FD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	[Tooltip("Used to close notification dialogue.")]
	[SerializeField]
	private Button closeButton;

	[Token(Token = "0x40013FE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	[SerializeField]
	private OnMessageChangedEvent onMessageChanged;

	[Token(Token = "0x170003AF")]
	protected virtual Button ConfirmButton
	{
		[Token(Token = "0x60019A3")]
		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "98")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003B0")]
	protected virtual Button CancelButton
	{
		[Token(Token = "0x60019A4")]
		[Address(RVA = "0x49D2B0", Offset = "0x49BCB0", VA = "0x18049D2B0", Slot = "99")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003B1")]
	protected virtual Button CloseButton
	{
		[Token(Token = "0x60019A5")]
		[Address(RVA = "0x49D250", Offset = "0x49BC50", VA = "0x18049D250", Slot = "100")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003B2")]
	protected virtual bool? Confirmed
	{
		[Token(Token = "0x60019A6")]
		[Address(RVA = "0x61D0C0", Offset = "0x61BAC0", VA = "0x18061D0C0", Slot = "101")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60019A7")]
		[Address(RVA = "0x61D0D0", Offset = "0x61BAD0", VA = "0x18061D0D0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x60019A8")]
	[Address(RVA = "0x61D0E0", Offset = "0x61BAE0", VA = "0x18061D0E0", Slot = "83")]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60019A9")]
	[Address(RVA = "0x61D200", Offset = "0x61BC00", VA = "0x18061D200", Slot = "102")]
	[AsyncStateMachine(typeof(_003CConfirm_003Ed__16))]
	public virtual UniTask<bool> Confirm(string message)
	{
		return default(UniTask<bool>);
	}

	[Token(Token = "0x60019AA")]
	[Address(RVA = "0x61D3F0", Offset = "0x61BDF0", VA = "0x18061D3F0", Slot = "103")]
	[AsyncStateMachine(typeof(_003CNotify_003Ed__17))]
	public virtual UniTask Notify(string message)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60019AB")]
	[Address(RVA = "0x61D570", Offset = "0x61BF70", VA = "0x18061D570", Slot = "86")]
	public override UniTask HandleLocalizationChanged(LocaleChangedArgs _)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60019AC")]
	[Address(RVA = "0x61D640", Offset = "0x61C040", VA = "0x18061D640", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x60019AD")]
	[Address(RVA = "0x61D7C0", Offset = "0x61C1C0", VA = "0x18061D7C0", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x60019AE")]
	[Address(RVA = "0x61D940", Offset = "0x61C340", VA = "0x18061D940", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x60019AF")]
	[Address(RVA = "0x61DB50", Offset = "0x61C550", VA = "0x18061DB50", Slot = "104")]
	protected virtual void Confirm()
	{
	}

	[Token(Token = "0x60019B0")]
	[Address(RVA = "0x61DBD0", Offset = "0x61C5D0", VA = "0x18061DBD0", Slot = "105")]
	protected virtual void Cancel()
	{
	}

	[Token(Token = "0x60019B1")]
	[Address(RVA = "0x61DC50", Offset = "0x61C650", VA = "0x18061DC50", Slot = "106")]
	protected virtual void SetMessage(string value)
	{
	}

	[Token(Token = "0x60019B2")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public ConfirmationPanel()
	{
	}
}
