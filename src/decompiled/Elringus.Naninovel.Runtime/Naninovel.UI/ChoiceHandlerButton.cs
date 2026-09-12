using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naninovel.UI;

[Token(Token = "0x200050A")]
[RequireComponent(typeof(Button))]
public class ChoiceHandlerButton : ScriptableButton
{
	[Serializable]
	[Token(Token = "0x200050B")]
	private class SummaryTextChangedEvent : UnityEvent<string>
	{
		[Token(Token = "0x600192E")]
		[Address(RVA = "0x6155E0", Offset = "0x613FE0", VA = "0x1806155E0")]
		public SummaryTextChangedEvent()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x200050C")]
	private class OnLockEvent : UnityEvent<bool>
	{
		[Token(Token = "0x600192F")]
		[Address(RVA = "0x615620", Offset = "0x614020", VA = "0x180615620")]
		public OnLockEvent()
		{
		}
	}

	[Token(Token = "0x400139C")]
	[FieldOffset(Offset = "0xF8")]
	[Tooltip("Invoked when the choice summary text is changed.")]
	[SerializeField]
	private SummaryTextChangedEvent onSummaryTextChanged;

	[Token(Token = "0x400139D")]
	[FieldOffset(Offset = "0x100")]
	[Tooltip("Invoked when lock status is changed: true when locked/disabled and vice-versa.")]
	[SerializeField]
	private OnLockEvent onLock;

	[Token(Token = "0x1700039A")]
	public ChoiceState ChoiceState
	{
		[Token(Token = "0x6001929")]
		[Address(RVA = "0x615320", Offset = "0x613D20", VA = "0x180615320")]
		[CompilerGenerated]
		get
		{
			return default(ChoiceState);
		}
		[Token(Token = "0x600192A")]
		[Address(RVA = "0x615370", Offset = "0x613D70", VA = "0x180615370")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x1400008B")]
	public event Action<string> OnSummaryTextChanged
	{
		[Token(Token = "0x6001925")]
		[Address(RVA = "0x614EA0", Offset = "0x6138A0", VA = "0x180614EA0")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001926")]
		[Address(RVA = "0x614FC0", Offset = "0x6139C0", VA = "0x180614FC0")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400008C")]
	public event Action<bool> OnLock
	{
		[Token(Token = "0x6001927")]
		[Address(RVA = "0x6150E0", Offset = "0x613AE0", VA = "0x1806150E0")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001928")]
		[Address(RVA = "0x615200", Offset = "0x613C00", VA = "0x180615200")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x600192B")]
	[Address(RVA = "0x615400", Offset = "0x613E00", VA = "0x180615400", Slot = "63")]
	public virtual void Initialize(ChoiceState choiceState)
	{
	}

	[Token(Token = "0x600192C")]
	[Address(RVA = "0x6155C0", Offset = "0x613FC0", VA = "0x1806155C0", Slot = "64")]
	public virtual void HandleLockChanged(bool locked)
	{
	}

	[Token(Token = "0x600192D")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public ChoiceHandlerButton()
	{
	}
}
