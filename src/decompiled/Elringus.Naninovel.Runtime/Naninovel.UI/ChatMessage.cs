using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naninovel.UI;

[Token(Token = "0x2000592")]
[RequireComponent(typeof(CanvasGroup))]
public class ChatMessage : ScriptableUIBehaviour
{
	[Serializable]
	[Token(Token = "0x2000593")]
	private class MessageTextChangedEvent : UnityEvent<string>
	{
		[Token(Token = "0x6001BB2")]
		[Address(RVA = "0x493290", Offset = "0x491C90", VA = "0x180493290")]
		public MessageTextChangedEvent()
		{
		}
	}

	[Token(Token = "0x400154E")]
	[FieldOffset(Offset = "0x88")]
	[SerializeField]
	private AuthorNamePanel actorNamePanel;

	[Token(Token = "0x400154F")]
	[FieldOffset(Offset = "0x90")]
	[SerializeField]
	private Image messageFrameImage;

	[Token(Token = "0x4001550")]
	[FieldOffset(Offset = "0x98")]
	[SerializeField]
	private RawImage avatarImage;

	[Token(Token = "0x4001551")]
	[FieldOffset(Offset = "0xA0")]
	[Tooltip("Invoked when the message text is changed.")]
	[SerializeField]
	private MessageTextChangedEvent onMessageTextChanged;

	[Token(Token = "0x4001552")]
	[FieldOffset(Offset = "0xA8")]
	[SerializeField]
	private UnityEvent onStartTyping;

	[Token(Token = "0x4001553")]
	[FieldOffset(Offset = "0xB0")]
	[SerializeField]
	private UnityEvent onStopTyping;

	[Token(Token = "0x4001554")]
	[FieldOffset(Offset = "0xB8")]
	private string messageText;

	[Token(Token = "0x170003EF")]
	public virtual string MessageText
	{
		[Token(Token = "0x6001B9E")]
		[Address(RVA = "0x492CE0", Offset = "0x4916E0", VA = "0x180492CE0", Slot = "59")]
		get
		{
			return null;
		}
		[Token(Token = "0x6001B9F")]
		[Address(RVA = "0x492CF0", Offset = "0x4916F0", VA = "0x180492CF0", Slot = "60")]
		set
		{
		}
	}

	[Token(Token = "0x170003F0")]
	public virtual Color MessageColor
	{
		[Token(Token = "0x6001BA0")]
		[Address(RVA = "0x492D10", Offset = "0x491710", VA = "0x180492D10", Slot = "61")]
		get
		{
			return default(Color);
		}
		[Token(Token = "0x6001BA1")]
		[Address(RVA = "0x492D60", Offset = "0x491760", VA = "0x180492D60", Slot = "62")]
		set
		{
		}
	}

	[Token(Token = "0x170003F1")]
	public virtual string ActorNameText
	{
		[Token(Token = "0x6001BA2")]
		[Address(RVA = "0x492DA0", Offset = "0x4917A0", VA = "0x180492DA0", Slot = "63")]
		get
		{
			return null;
		}
		[Token(Token = "0x6001BA3")]
		[Address(RVA = "0x492DD0", Offset = "0x4917D0", VA = "0x180492DD0", Slot = "64")]
		set
		{
		}
	}

	[Token(Token = "0x170003F2")]
	public virtual Color ActorNameTextColor
	{
		[Token(Token = "0x6001BA4")]
		[Address(RVA = "0x492E00", Offset = "0x491800", VA = "0x180492E00", Slot = "65")]
		get
		{
			return default(Color);
		}
		[Token(Token = "0x6001BA5")]
		[Address(RVA = "0x492E50", Offset = "0x491850", VA = "0x180492E50", Slot = "66")]
		set
		{
		}
	}

	[Token(Token = "0x170003F3")]
	public virtual Texture AvatarTexture
	{
		[Token(Token = "0x6001BA6")]
		[Address(RVA = "0x492E90", Offset = "0x491890", VA = "0x180492E90", Slot = "67")]
		get
		{
			return null;
		}
		[Token(Token = "0x6001BA7")]
		[Address(RVA = "0x492EC0", Offset = "0x4918C0", VA = "0x180492EC0", Slot = "68")]
		set
		{
		}
	}

	[Token(Token = "0x170003F4")]
	protected virtual AuthorNamePanel ActorNamePanel
	{
		[Token(Token = "0x6001BA8")]
		[Address(RVA = "0x46D4E0", Offset = "0x46BEE0", VA = "0x18046D4E0", Slot = "69")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003F5")]
	protected virtual Image MessageFrameImage
	{
		[Token(Token = "0x6001BA9")]
		[Address(RVA = "0x468E20", Offset = "0x467820", VA = "0x180468E20", Slot = "70")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003F6")]
	protected virtual RawImage AvatarImage
	{
		[Token(Token = "0x6001BAA")]
		[Address(RVA = "0x492EE0", Offset = "0x4918E0", VA = "0x180492EE0", Slot = "71")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003F7")]
	protected virtual bool Typing
	{
		[Token(Token = "0x6001BAB")]
		[Address(RVA = "0x492EF0", Offset = "0x4918F0", VA = "0x180492EF0", Slot = "72")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6001BAC")]
		[Address(RVA = "0x492F00", Offset = "0x491900", VA = "0x180492F00")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6001BAD")]
	[Address(RVA = "0x492F10", Offset = "0x491910", VA = "0x180492F10", Slot = "73")]
	public virtual void SetIsTyping(bool typing)
	{
	}

	[Token(Token = "0x6001BAE")]
	[Address(RVA = "0x492F80", Offset = "0x491980", VA = "0x180492F80", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001BAF")]
	[Address(RVA = "0x4930C0", Offset = "0x491AC0", VA = "0x1804930C0", Slot = "74")]
	protected virtual void SetMessageText(string text)
	{
	}

	[Token(Token = "0x6001BB0")]
	[Address(RVA = "0x493170", Offset = "0x491B70", VA = "0x180493170", Slot = "75")]
	protected virtual void SetAvatarTexture(Texture texture)
	{
	}

	[Token(Token = "0x6001BB1")]
	[Address(RVA = "0x38A020", Offset = "0x388A20", VA = "0x18038A020")]
	public ChatMessage()
	{
	}
}
