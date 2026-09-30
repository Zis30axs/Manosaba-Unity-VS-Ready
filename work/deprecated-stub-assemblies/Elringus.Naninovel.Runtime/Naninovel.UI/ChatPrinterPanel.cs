using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [Token(Token = "0x2000594")]
    public class ChatPrinterPanel : UITextPrinterPanel, ILocalizableUI
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000596")]
    	[CompilerGenerated]
    	private struct _003CEmbedChoiceHandler_003Ed__40
    {
    		[Token(Token = "0x4001562")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001563")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001564")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ChatPrinterPanel _003C_003E4__this;

    		[Token(Token = "0x4001565")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask<IChoiceHandlerActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001BCE")]
    		[Address(RVA = "0x494E50", Offset = "0x493850", VA = "0x180494E50", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001BCF")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000597")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__32
    {
    		[Token(Token = "0x4001566")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001567")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001568")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ChatPrinterPanel _003C_003E4__this;

    		[Token(Token = "0x4001569")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001BD0")]
    		[Address(RVA = "0x4954B0", Offset = "0x493EB0", VA = "0x1804954B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001BD1")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000598")]
    	[CompilerGenerated]
    	private struct _003CRevealMessages_003Ed__36
    {
    		[Token(Token = "0x400156A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400156B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400156C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ChatPrinterPanel _003C_003E4__this;

    		[Token(Token = "0x400156D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float delay;

    		[Token(Token = "0x400156E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x400156F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private ChatMessage _003Cmessage_003E5__2;

    		[Token(Token = "0x4001570")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private float _003CrevealDuration_003E5__3;

    		[Token(Token = "0x4001571")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
    		private float _003CrevealStartTime_003E5__4;

    		[Token(Token = "0x4001572")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private float _003CrevealFinishTime_003E5__5;

    		[Token(Token = "0x4001573")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001BD2")]
    		[Address(RVA = "0x495940", Offset = "0x494340", VA = "0x180495940", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001BD3")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000599")]
    	[CompilerGenerated]
    	private struct _003CScrollToBottom_003Ed__45
    {
    		[Token(Token = "0x4001574")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001575")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001576")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public ChatPrinterPanel _003C_003E4__this;

    		[Token(Token = "0x4001577")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001BD4")]
    		[Address(RVA = "0x4960F0", Offset = "0x494AF0", VA = "0x1804960F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001BD5")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001556")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x140")]
    	[SerializeField]
    	private ScrollRect scrollRect;

    	[Token(Token = "0x4001557")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x148")]
    	[SerializeField]
    	private RectTransform messagesContainer;

    	[Token(Token = "0x4001558")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x150")]
    	[SerializeField]
    	private ChatMessage messagePrototype;

    	[Token(Token = "0x4001559")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x158")]
    	[SerializeField]
    	private ScriptableUIBehaviour inputIndicator;

    	[Token(Token = "0x400155A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x160")]
    	[SerializeField]
    	private float revealDelayModifier;

    	[Token(Token = "0x400155B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x168")]
    	[Tooltip("Associated choice handler actor ID to embed inside the printer; implementation is expected to be MonoBehaviourActor-derived.")]
    	[SerializeField]
    	private string choiceHandlerId;

    	[Token(Token = "0x400155C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x170")]
    	[SerializeField]
    	private RectTransform choiceHandlerContainer;

    	[Token(Token = "0x400155D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x178")]
    	private readonly Stack<ChatMessage> chatMessages;

    	[Token(Token = "0x400155E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x180")]
    	private ICharacterManager characterManager;

    	[Token(Token = "0x400155F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x188")]
    	private IChoiceHandlerManager choiceManager;

    	[Token(Token = "0x4001560")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x190")]
    	private float revealProgress;

    	[Token(Token = "0x170003F8")]
    	public override float RevealProgress
    	{
    		[Token(Token = "0x6001BB3")]
    		[Address(RVA = "0x4932D0", Offset = "0x491CD0", VA = "0x1804932D0", Slot = "96")]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001BB4")]
    		[Address(RVA = "0x4932E0", Offset = "0x491CE0", VA = "0x1804932E0", Slot = "97")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x170003F9")]
    	public override string Appearance
    	{
    		[Token(Token = "0x6001BB5")]
    		[Address(RVA = "0x493300", Offset = "0x491D00", VA = "0x180493300", Slot = "98")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001BB6")]
    		[Address(RVA = "0x493310", Offset = "0x491D10", VA = "0x180493310", Slot = "99")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x170003FA")]
    	protected virtual ScrollRect ScrollRect
    	{
    		[Token(Token = "0x6001BB7")]
    		[Address(RVA = "0x493370", Offset = "0x491D70", VA = "0x180493370", Slot = "116")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003FB")]
    	protected virtual RectTransform MessagesContainer
    	{
    		[Token(Token = "0x6001BB8")]
    		[Address(RVA = "0x493380", Offset = "0x491D80", VA = "0x180493380", Slot = "117")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003FC")]
    	protected virtual ChatMessage MessagePrototype
    	{
    		[Token(Token = "0x6001BB9")]
    		[Address(RVA = "0x493390", Offset = "0x491D90", VA = "0x180493390", Slot = "118")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003FD")]
    	protected virtual ScriptableUIBehaviour InputIndicator
    	{
    		[Token(Token = "0x6001BBA")]
    		[Address(RVA = "0x4933A0", Offset = "0x491DA0", VA = "0x1804933A0", Slot = "119")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003FE")]
    	protected virtual float RevealDelayModifier
    	{
    		[Token(Token = "0x6001BBB")]
    		[Address(RVA = "0x4933B0", Offset = "0x491DB0", VA = "0x1804933B0", Slot = "120")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x170003FF")]
    	protected virtual string ChoiceHandlerId
    	{
    		[Token(Token = "0x6001BBC")]
    		[Address(RVA = "0x4933C0", Offset = "0x491DC0", VA = "0x1804933C0", Slot = "121")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000400")]
    	protected virtual RectTransform ChoiceHandlerContainer
    	{
    		[Token(Token = "0x6001BBD")]
    		[Address(RVA = "0x4933D0", Offset = "0x491DD0", VA = "0x1804933D0", Slot = "122")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6001BBE")]
    	[Address(RVA = "0x4933E0", Offset = "0x491DE0", VA = "0x1804933E0", Slot = "83")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__32))]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001BBF")]
    	[Address(RVA = "0x493500", Offset = "0x491F00", VA = "0x180493500", Slot = "107")]
    	public override void SetMessages(IReadOnlyList<PrintedMessage> messages)
    	{
    	}

    	[Token(Token = "0x6001BC0")]
    	[Address(RVA = "0x493770", Offset = "0x492170", VA = "0x180493770", Slot = "108")]
    	public override void AddMessage(PrintedMessage message)
    	{
    	}

    	[Token(Token = "0x6001BC1")]
    	[Address(RVA = "0x493840", Offset = "0x492240", VA = "0x180493840", Slot = "109")]
    	public override void AppendText(LocalizableText text)
    	{
    	}

    	[Token(Token = "0x6001BC2")]
    	[Address(RVA = "0x493D90", Offset = "0x492790", VA = "0x180493D90", Slot = "111")]
    	[AsyncStateMachine(typeof(_003CRevealMessages_003Ed__36))]
    	public override UniTask RevealMessages(float delay, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001BC3")]
    	[Address(RVA = "0x493F50", Offset = "0x492950", VA = "0x180493F50", Slot = "112")]
    	public override void SetWaitForInputIndicatorVisible(bool visible)
    	{
    	}

    	[Token(Token = "0x6001BC4")]
    	[Address(RVA = "0x493FA0", Offset = "0x4929A0", VA = "0x180493FA0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001BC5")]
    	[Address(RVA = "0x494240", Offset = "0x492C40", VA = "0x180494240", Slot = "8")]
    	protected override void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6001BC6")]
    	[Address(RVA = "0x4942E0", Offset = "0x492CE0", VA = "0x1804942E0", Slot = "123")]
    	[AsyncStateMachine(typeof(_003CEmbedChoiceHandler_003Ed__40))]
    	protected virtual UniTask EmbedChoiceHandler()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001BC7")]
    	[Address(RVA = "0x494400", Offset = "0x492E00", VA = "0x180494400", Slot = "124")]
    	protected virtual void HandleChoiceVisibilityChanged(bool visible)
    	{
    	}

    	[Token(Token = "0x6001BC8")]
    	[Address(RVA = "0x494470", Offset = "0x492E70", VA = "0x180494470", Slot = "125")]
    	protected virtual ChatMessage SpawnChatMessage(PrintedMessage message)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001BC9")]
    	[Address(RVA = "0x4949A0", Offset = "0x4933A0", VA = "0x1804949A0", Slot = "126")]
    	protected virtual void SetRevealProgress(float ratio)
    	{
    	}

    	[Token(Token = "0x6001BCA")]
    	[Address(RVA = "0x494A50", Offset = "0x493450", VA = "0x180494A50", Slot = "127")]
    	protected virtual void DestroyAllMessages()
    	{
    	}

    	[Token(Token = "0x6001BCB")]
    	[Address(RVA = "0x494BB0", Offset = "0x4935B0", VA = "0x180494BB0", Slot = "128")]
    	[AsyncStateMachine(typeof(_003CScrollToBottom_003Ed__45))]
    	protected virtual void ScrollToBottom()
    	{
    	}

    	[Token(Token = "0x6001BCC")]
    	[Address(RVA = "0x494CE0", Offset = "0x4936E0", VA = "0x180494CE0")]
    	public ChatPrinterPanel()
    	{
    	}
    }
}
