using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel.UI
{
    [Token(Token = "0x200059C")]
    public class RevealableTextPrinterPanel : UITextPrinterPanel, ILocalizableUI
    {
    	[Serializable]
    	[Token(Token = "0x200059D")]
    	protected class CharsToSfx
    	{
    		[Token(Token = "0x4001594")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		[Tooltip("The characters for which to trigger the SFX. Leave empty to trigger on any character.")]
    		public string Characters;

    		[Token(Token = "0x4001595")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		[Tooltip("The name (local path) of the SFX to trigger for the specified characters.")]
    		[ResourcePopup("Audio")]
    		public string SfxName;

    		[Token(Token = "0x6001C14")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public CharsToSfx()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x200059E")]
    	protected class CharsToPlaylist
    	{
    		[Token(Token = "0x4001596")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		[Tooltip("The characters for which to trigger the command. Leave empty to trigger on any character.")]
    		public string Characters;

    		[Token(Token = "0x4001597")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		[Tooltip("The text of the script command to execute for the specified characters.")]
    		public string CommandText;

    		[Token(Token = "0x17000415")]
    		public ScriptPlaylist Playlist
    		{
    			[Token(Token = "0x6001C15")]
    			[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    			[CompilerGenerated]
    			get
    			{
    				return null;
    			}
    			[Token(Token = "0x6001C16")]
    			[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x6001C17")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public CharsToPlaylist()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x200059F")]
    	private class AuthorChangedEvent : UnityEvent<string>
    	{
    		[Token(Token = "0x6001C18")]
    		[Address(RVA = "0x49A850", Offset = "0x499250", VA = "0x18049A850")]
    		public AuthorChangedEvent()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005A3")]
    	[CompilerGenerated]
    	private struct _003C_003Ec__DisplayClass80_0
    	{
    		[Token(Token = "0x400159F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public char character;
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005A4")]
    	[CompilerGenerated]
    	private struct _003CExecuteCommandForChar_003Ed__80
    {
    		[Token(Token = "0x40015A0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40015A1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40015A2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public char character;

    		[Token(Token = "0x40015A3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public RevealableTextPrinterPanel _003C_003E4__this;

    		[Token(Token = "0x40015A4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private _003C_003Ec__DisplayClass80_0 _003C_003E8__1;

    		[Token(Token = "0x40015A5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x40015A6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private List<CharsToPlaylist>.Enumerator _003C_003E7__wrap1;

    		[Token(Token = "0x40015A7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001C1D")]
    		[Address(RVA = "0x49AC50", Offset = "0x499650", VA = "0x18049AC50", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001C1E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005A5")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__55
    {
    		[Token(Token = "0x40015A8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40015A9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40015AA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public RevealableTextPrinterPanel _003C_003E4__this;

    		[Token(Token = "0x40015AB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40015AC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private PooledObject<List<UniTask>> _003C__003E5__2;

    		[Token(Token = "0x40015AD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private YieldAwaitable.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6001C1F")]
    		[Address(RVA = "0x49B0F0", Offset = "0x499AF0", VA = "0x18049B0F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001C20")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005A6")]
    	[CompilerGenerated]
    	private struct _003CPlaceInputIndicatorOverText_003Ed__71
    {
    		[Token(Token = "0x40015AE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40015AF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40015B0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public RevealableTextPrinterPanel _003C_003E4__this;

    		[Token(Token = "0x40015B1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001C21")]
    		[Address(RVA = "0x49BF60", Offset = "0x49A960", VA = "0x18049BF60", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001C22")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005A7")]
    	[CompilerGenerated]
    	private struct _003CRevealMessages_003Ed__59
    {
    		[Token(Token = "0x40015B2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40015B3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40015B4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public RevealableTextPrinterPanel _003C_003E4__this;

    		[Token(Token = "0x40015B5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float delay;

    		[Token(Token = "0x40015B6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x40015B7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40015B8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<bool>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6001C23")]
    		[Address(RVA = "0x49C4D0", Offset = "0x49AED0", VA = "0x18049C4D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001C24")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005A8")]
    	[CompilerGenerated]
    	private struct _003CWaitForContinueRevealConfirmation_003Ed__60
    {
    		[Token(Token = "0x40015B9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40015BA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40015BB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public RevealableTextPrinterPanel _003C_003E4__this;

    		[Token(Token = "0x40015BC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40015BD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationToken _003Cnext_003E5__2;

    		[Token(Token = "0x40015BE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001C25")]
    		[Address(RVA = "0x49CAD0", Offset = "0x49B4D0", VA = "0x18049CAD0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001C26")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400157E")]
    	protected const string DefaultAppearanceName = "Default";

    	[Token(Token = "0x4001581")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x150")]
    	[Tooltip("Revealable text component. Expected to implement 'IRevealableText' interface.")]
    	[SerializeField]
    	private MonoBehaviour revealableText;

    	[Token(Token = "0x4001582")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x158")]
    	[Tooltip("Panel to display name of the currently printed text author (optional).")]
    	[SerializeField]
    	private AuthorNamePanel authorNamePanel;

    	[Token(Token = "0x4001583")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x160")]
    	[Tooltip("Image to display avatar of the currently printed text author (optional).")]
    	[SerializeField]
    	private AuthorImage authorAvatarImage;

    	[Token(Token = "0x4001584")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x168")]
    	[Tooltip("Object to use as an indicator when player is supposed to activate a 'Continue' input to progress further. Expected to implement 'IInputIndicator' interface.")]
    	[SerializeField]
    	private MonoBehaviour inputIndicator;

    	[Token(Token = "0x4001585")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x170")]
    	[Tooltip("Whether to automatically move input indicator so it appears after the last revealed text character.")]
    	[SerializeField]
    	private bool positionIndicatorOverText;

    	[Token(Token = "0x4001586")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x174")]
    	[Tooltip("Number of line breaks (<br> tags) to insert between adjacent messages when formatting the messages.")]
    	[SerializeField]
    	private int autoLineBreaks;

    	[Token(Token = "0x4001587")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x178")]
    	[Tooltip("Assigned canvas groups will represent printer appearances. Game object name of the canvas group represents the appearance name. Alpha of the group will be set to 1 when the appearance is activated and vice-versa.")]
    	[SerializeField]
    	private List<CanvasGroup> appearances;

    	[Token(Token = "0x4001588")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x180")]
    	[Tooltip("Allows binding an SFX to play when specific characters are revealed.")]
    	[SerializeField]
    	private List<CharsToSfx> charsSfx;

    	[Token(Token = "0x4001589")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x188")]
    	[Tooltip("Allows binding a script command to execute when specific characters are revealed.")]
    	[SerializeField]
    	private List<CharsToPlaylist> charsCommands;

    	[Token(Token = "0x400158A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x190")]
    	[Tooltip("Invoked when author (character ID) of the currently printed text is changed.")]
    	[SerializeField]
    	private AuthorChangedEvent onAuthorChanged;

    	[Token(Token = "0x400158B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x198")]
    	[Tooltip("Invoked when text reveal is started.")]
    	[SerializeField]
    	private UnityEvent onRevealStarted;

    	[Token(Token = "0x400158C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1A0")]
    	[Tooltip("Invoked when text reveal is finished.")]
    	[SerializeField]
    	private UnityEvent onRevealFinished;

    	[Token(Token = "0x400158D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1A8")]
    	private readonly StringBuilder builder;

    	[Token(Token = "0x400158E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1B0")]
    	private TextRevealer revealer;

    	[Token(Token = "0x400158F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1B8")]
    	private Color defaultMessageColor;

    	[Token(Token = "0x4001590")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1C8")]
    	private Color defaultNameColor;

    	[Token(Token = "0x4001591")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1D8")]
    	private IAudioManager audioManager;

    	[Token(Token = "0x4001592")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1E0")]
    	private IScriptPlayer scriptPlayer;

    	[Token(Token = "0x4001593")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1E8")]
    	private IInputSampler continueInput;

    	[Token(Token = "0x17000408")]
    	public virtual IRevealableText RevealableText
    	{
    		[Token(Token = "0x6001BE3")]
    		[Address(RVA = "0x4969F0", Offset = "0x4953F0", VA = "0x1804969F0", Slot = "116")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000409")]
    	public override float RevealProgress
    	{
    		[Token(Token = "0x6001BE4")]
    		[Address(RVA = "0x496A60", Offset = "0x495460", VA = "0x180496A60", Slot = "96")]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001BE5")]
    		[Address(RVA = "0x496AD0", Offset = "0x4954D0", VA = "0x180496AD0", Slot = "97")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700040A")]
    	public override string Appearance
    	{
    		[Token(Token = "0x6001BE6")]
    		[Address(RVA = "0x496AF0", Offset = "0x4954F0", VA = "0x180496AF0", Slot = "98")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001BE7")]
    		[Address(RVA = "0x496B10", Offset = "0x495510", VA = "0x180496B10", Slot = "99")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700040B")]
    	protected virtual MessageAuthor Author
    	{
    		[Token(Token = "0x6001BE8")]
    		[Address(RVA = "0x496B30", Offset = "0x495530", VA = "0x180496B30", Slot = "117")]
    		[CompilerGenerated]
    		get
    		{
    			return default(MessageAuthor);
    		}
    		[Token(Token = "0x6001BE9")]
    		[Address(RVA = "0x496B40", Offset = "0x495540", VA = "0x180496B40")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700040C")]
    	protected virtual CharacterMetadata AuthorMeta
    	{
    		[Token(Token = "0x6001BEA")]
    		[Address(RVA = "0x493380", Offset = "0x491D80", VA = "0x180493380", Slot = "118")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001BEB")]
    		[Address(RVA = "0x496BA0", Offset = "0x4955A0", VA = "0x180496BA0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700040D")]
    	protected virtual IInputIndicator InputIndicator
    	{
    		[Token(Token = "0x6001BEC")]
    		[Address(RVA = "0x496C00", Offset = "0x495600", VA = "0x180496C00", Slot = "119")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700040E")]
    	protected virtual AuthorNamePanel AuthorNamePanel
    	{
    		[Token(Token = "0x6001BED")]
    		[Address(RVA = "0x4933A0", Offset = "0x491DA0", VA = "0x1804933A0", Slot = "120")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700040F")]
    	protected virtual AuthorImage AuthorAvatarImage
    	{
    		[Token(Token = "0x6001BEE")]
    		[Address(RVA = "0x496C70", Offset = "0x495670", VA = "0x180496C70", Slot = "121")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000410")]
    	protected virtual bool PositionIndicatorOverText
    	{
    		[Token(Token = "0x6001BEF")]
    		[Address(RVA = "0x496C80", Offset = "0x495680", VA = "0x180496C80", Slot = "122")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000411")]
    	protected virtual List<CanvasGroup> Appearances
    	{
    		[Token(Token = "0x6001BF0")]
    		[Address(RVA = "0x496C90", Offset = "0x495690", VA = "0x180496C90", Slot = "123")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000412")]
    	protected virtual List<CharsToSfx> CharsSfx
    	{
    		[Token(Token = "0x6001BF1")]
    		[Address(RVA = "0x496CA0", Offset = "0x4956A0", VA = "0x180496CA0", Slot = "124")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000413")]
    	protected virtual List<CharsToPlaylist> CharsCommands
    	{
    		[Token(Token = "0x6001BF2")]
    		[Address(RVA = "0x366360", Offset = "0x364D60", VA = "0x180366360", Slot = "125")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000414")]
    	protected virtual int AutoLineBreaks
    	{
    		[Token(Token = "0x6001BF3")]
    		[Address(RVA = "0x496CB0", Offset = "0x4956B0", VA = "0x180496CB0", Slot = "126")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x6001BF4")]
    	[Address(RVA = "0x33FA90", Offset = "0x33E490", VA = "0x18033FA90", Slot = "83")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__55))]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001BF5")]
    	[Address(RVA = "0x496CC0", Offset = "0x4956C0", VA = "0x180496CC0", Slot = "107")]
    	public override void SetMessages(IReadOnlyList<PrintedMessage> messages)
    	{
    	}

    	[Token(Token = "0x6001BF6")]
    	[Address(RVA = "0x497250", Offset = "0x495C50", VA = "0x180497250", Slot = "108")]
    	public override void AddMessage(PrintedMessage message)
    	{
    	}

    	[Token(Token = "0x6001BF7")]
    	[Address(RVA = "0x4974C0", Offset = "0x495EC0", VA = "0x1804974C0", Slot = "109")]
    	public override void AppendText(LocalizableText text)
    	{
    	}

    	[Token(Token = "0x6001BF8")]
    	[Address(RVA = "0x497A60", Offset = "0x496460", VA = "0x180497A60", Slot = "111")]
    	[AsyncStateMachine(typeof(_003CRevealMessages_003Ed__59))]
    	public override UniTask RevealMessages(float delay, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001BF9")]
    	[Address(RVA = "0x497C10", Offset = "0x496610", VA = "0x180497C10", Slot = "127")]
    	[AsyncStateMachine(typeof(_003CWaitForContinueRevealConfirmation_003Ed__60))]
    	protected virtual UniTask WaitForContinueRevealConfirmation(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001BFA")]
    	[Address(RVA = "0x497DA0", Offset = "0x4967A0", VA = "0x180497DA0", Slot = "112")]
    	public override void SetWaitForInputIndicatorVisible(bool visible)
    	{
    	}

    	[Token(Token = "0x6001BFB")]
    	[Address(RVA = "0x497E70", Offset = "0x496870", VA = "0x180497E70", Slot = "85")]
    	public override void SetFontSize(int dropdownIndex)
    	{
    	}

    	[Token(Token = "0x6001BFC")]
    	[Address(RVA = "0x497EE0", Offset = "0x4968E0", VA = "0x180497EE0", Slot = "84")]
    	public override void SetFont(TMP_FontAsset font)
    	{
    	}

    	[Token(Token = "0x6001BFD")]
    	[Address(RVA = "0x497F50", Offset = "0x496950", VA = "0x180497F50", Slot = "86")]
    	public override UniTask HandleLocalizationChanged(LocaleChangedArgs _)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001BFE")]
    	[Address(RVA = "0x4980C0", Offset = "0x496AC0", VA = "0x1804980C0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001BFF")]
    	[Address(RVA = "0x498650", Offset = "0x497050", VA = "0x180498650", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x6001C00")]
    	[Address(RVA = "0x498710", Offset = "0x497110", VA = "0x180498710", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x6001C01")]
    	[Address(RVA = "0x4987F0", Offset = "0x4971F0", VA = "0x1804987F0", Slot = "8")]
    	protected override void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6001C02")]
    	[Address(RVA = "0x4989F0", Offset = "0x4973F0", VA = "0x1804989F0", Slot = "55")]
    	protected override void HandleVisibilityChanged(bool visible)
    	{
    	}

    	[Token(Token = "0x6001C03")]
    	[Address(RVA = "0x498B60", Offset = "0x497560", VA = "0x180498B60", Slot = "128")]
    	protected virtual void SetMessageAuthor(MessageAuthor author)
    	{
    	}

    	[Token(Token = "0x6001C04")]
    	[Address(RVA = "0x499250", Offset = "0x497C50", VA = "0x180499250", Slot = "129")]
    	[AsyncStateMachine(typeof(_003CPlaceInputIndicatorOverText_003Ed__71))]
    	protected virtual void PlaceInputIndicatorOverText()
    	{
    	}

    	[Token(Token = "0x6001C05")]
    	[Address(RVA = "0x499380", Offset = "0x497D80", VA = "0x180499380", Slot = "130")]
    	protected virtual string GetActiveAppearance()
    	{
    		return null;
    	}

    	[Token(Token = "0x6001C06")]
    	[Address(RVA = "0x4995F0", Offset = "0x497FF0", VA = "0x1804995F0", Slot = "131")]
    	protected virtual void SetActiveAppearance(string appearance)
    	{
    	}

    	[Token(Token = "0x6001C07")]
    	[Address(RVA = "0x499900", Offset = "0x498300", VA = "0x180499900", Slot = "132")]
    	protected virtual void SetRevealProgress(float value)
    	{
    	}

    	[Token(Token = "0x6001C08")]
    	[Address(RVA = "0x499980", Offset = "0x498380", VA = "0x180499980", Slot = "133")]
    	protected virtual void SetAuthorNameText(string text)
    	{
    	}

    	[Token(Token = "0x6001C09")]
    	[Address(RVA = "0x499B70", Offset = "0x498570", VA = "0x180499B70", Slot = "134")]
    	protected virtual void HandleAvatarChanged(CharacterAvatarChangedArgs args)
    	{
    	}

    	[Token(Token = "0x6001C0A")]
    	[Address(RVA = "0x499D00", Offset = "0x498700", VA = "0x180499D00", Slot = "135")]
    	protected virtual void HandleCharRevealed(char character, AsyncToken token)
    	{
    	}

    	[Token(Token = "0x6001C0B")]
    	[Address(RVA = "0x499E90", Offset = "0x498890", VA = "0x180499E90", Slot = "136")]
    	protected virtual void PlayAuthorSound()
    	{
    	}

    	[Token(Token = "0x6001C0C")]
    	[Address(RVA = "0x499FA0", Offset = "0x4989A0", VA = "0x180499FA0", Slot = "137")]
    	protected virtual void PlayRevealSfxForChar(char character)
    	{
    	}

    	[Token(Token = "0x6001C0D")]
    	[Address(RVA = "0x49A190", Offset = "0x498B90", VA = "0x18049A190", Slot = "138")]
    	[AsyncStateMachine(typeof(_003CExecuteCommandForChar_003Ed__80))]
    	protected virtual UniTask ExecuteCommandForChar(char character, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001C0E")]
    	[Address(RVA = "0x49A340", Offset = "0x498D40", VA = "0x18049A340")]
    	protected string FormatMessage(PrintedMessage message, bool adjacent)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001C0F")]
    	[Address(RVA = "0x49A410", Offset = "0x498E10", VA = "0x18049A410")]
    	public RevealableTextPrinterPanel()
    	{
    	}
    }
}
