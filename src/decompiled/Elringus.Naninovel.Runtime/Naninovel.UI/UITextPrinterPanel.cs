using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x20005AA")]
[RequireComponent(typeof(CanvasGroup))]
public abstract class UITextPrinterPanel : CustomUI, IManagedUI
{
	[StructLayout(3)]
	[Token(Token = "0x20005AC")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__32
{
		[Token(Token = "0x40015D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40015D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40015D2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UITextPrinterPanel _003C_003E4__this;

		[Token(Token = "0x40015D3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001C46")]
		[Address(RVA = "0x49E760", Offset = "0x49D160", VA = "0x18049E760", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001C47")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40015C7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	[Tooltip("Transform used for printer position, scale and rotation external manipulations.")]
	[SerializeField]
	private RectTransform content;

	[Token(Token = "0x40015C8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	[Tooltip("Objects that should trigger continue input when interacted with. Make sure the objects are a raycast target and not blocked by other raycast targets.")]
	[SerializeField]
	private List<GameObject> continueInputTriggers;

	[Token(Token = "0x40015C9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	[Tooltip("Formatting templates to apply by default (before any templates assigned via `@format` command) for the messages printed by the printer actor.\n\n%TEXT% is replaced with the message text, %AUTHOR% — with the author name.\n\nThe templates are applied in order and filtered by the author: `+` applies for any authored message, `-` — for un-authored messages and `*` for all messages, authored or not.")]
	[SerializeField]
	private List<MessageTemplate> defaultTemplates;

	[Token(Token = "0x40015CA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	[Tooltip("Event invoked when tint color of the printer actor is changed.")]
	[SerializeField]
	private ColorUnityEvent onTintChanged;

	[Token(Token = "0x40015CB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
	private IInputSampler continueInput;

	[Token(Token = "0x40015CC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x120")]
	private IScriptPlayer scriptPlayer;

	[Token(Token = "0x40015CD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x128")]
	private Color tintColor;

	[Token(Token = "0x17000416")]
	public virtual RectTransform Content
	{
		[Token(Token = "0x6001C2A")]
		[Address(RVA = "0x49D250", Offset = "0x49BC50", VA = "0x18049D250", Slot = "95")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000417")]
	public abstract float RevealProgress
	{
		[Token(Token = "0x6001C2B")]
		get;
		[Token(Token = "0x6001C2C")]
		set;
	}

	[Token(Token = "0x17000418")]
	public abstract string Appearance
	{
		[Token(Token = "0x6001C2D")]
		get;
		[Token(Token = "0x6001C2E")]
		set;
	}

	[Token(Token = "0x17000419")]
	public virtual Color TintColor
	{
		[Token(Token = "0x6001C2F")]
		[Address(RVA = "0x49D260", Offset = "0x49BC60", VA = "0x18049D260", Slot = "100")]
		get
		{
			return default(Color);
		}
		[Token(Token = "0x6001C30")]
		[Address(RVA = "0x49D270", Offset = "0x49BC70", VA = "0x18049D270", Slot = "101")]
		set
		{
		}
	}

	[Token(Token = "0x1700041A")]
	public virtual IReadOnlyCollection<GameObject> ContinueInputTriggers
	{
		[Token(Token = "0x6001C31")]
		[Address(RVA = "0x37A040", Offset = "0x378A40", VA = "0x18037A040", Slot = "102")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700041B")]
	public virtual IReadOnlyList<MessageTemplate> DefaultTemplates
	{
		[Token(Token = "0x6001C32")]
		[Address(RVA = "0x37A0B0", Offset = "0x378AB0", VA = "0x18037A0B0", Slot = "103")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700041C")]
	protected virtual ICharacterManager CharacterManager
	{
		[Token(Token = "0x6001C33")]
		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0", Slot = "104")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6001C34")]
		[Address(RVA = "0x33E0D0", Offset = "0x33CAD0", VA = "0x18033E0D0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x1700041D")]
	protected virtual List<PrintedMessage> Messages
	{
		[Token(Token = "0x6001C35")]
		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "105")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700041E")]
	protected virtual List<MessageTemplate> Templates
	{
		[Token(Token = "0x6001C36")]
		[Address(RVA = "0x49D2B0", Offset = "0x49BCB0", VA = "0x18049D2B0", Slot = "106")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001C37")]
	[Address(RVA = "0x49D2C0", Offset = "0x49BCC0", VA = "0x18049D2C0", Slot = "83")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__32))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001C38")]
	[Address(RVA = "0x49D3E0", Offset = "0x49BDE0", VA = "0x18049D3E0", Slot = "68")]
	private UniTask Naninovel_002EUI_002EIManagedUI_002EChangeVisibility(bool visible, float? duration, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001C39")]
	public abstract void SetMessages(IReadOnlyList<PrintedMessage> messages);

	[Token(Token = "0x6001C3A")]
	public abstract void AddMessage(PrintedMessage message);

	[Token(Token = "0x6001C3B")]
	public abstract void AppendText(LocalizableText text);

	[Token(Token = "0x6001C3C")]
	[Address(RVA = "0x49D490", Offset = "0x49BE90", VA = "0x18049D490", Slot = "110")]
	public virtual void SetTemplates(IReadOnlyList<MessageTemplate> templates)
	{
	}

	[Token(Token = "0x6001C3D")]
	public abstract UniTask RevealMessages(float delay, AsyncToken token);

	[Token(Token = "0x6001C3E")]
	public abstract void SetWaitForInputIndicatorVisible(bool visible);

	[Token(Token = "0x6001C3F")]
	[Address(RVA = "0x49D500", Offset = "0x49BF00", VA = "0x18049D500", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001C40")]
	[Address(RVA = "0x49D7A0", Offset = "0x49C1A0", VA = "0x18049D7A0", Slot = "8")]
	protected override void OnDestroy()
	{
	}

	[Token(Token = "0x6001C41")]
	[Address(RVA = "0x49DAC0", Offset = "0x49C4C0", VA = "0x18049DAC0", Slot = "113")]
	protected virtual void SetTintColor(Color color)
	{
	}

	[Token(Token = "0x6001C42")]
	[Address(RVA = "0x49DB30", Offset = "0x49C530", VA = "0x18049DB30", Slot = "114")]
	protected virtual string FormatMessage(PrintedMessage message)
	{
		return null;
	}

	[Token(Token = "0x6001C43")]
	[Address(RVA = "0x49E3F0", Offset = "0x49CDF0", VA = "0x18049E3F0")]
	protected UITextPrinterPanel()
	{
	}
}
