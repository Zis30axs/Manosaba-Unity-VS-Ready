using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006EA")]
    [CommandAlias("print")]
    public class PrintText : PrinterCommand, Command.IPreloadable, Command.ILocalizable
    {
    	[Token(Token = "0x20006EC")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass31_0
    	{
    		[Token(Token = "0x4001A73")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public PrintText _003C_003E4__this;

    		[Token(Token = "0x4001A74")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ITextPrinterActor printer;

    		[Token(Token = "0x600213D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public _003C_003Ec__DisplayClass31_0()
    		{
    		}

    		[Token(Token = "0x600213E")]
    		[Address(RVA = "0x323A50", Offset = "0x322450", VA = "0x180323A50")]
    		internal UniTask _003CExecute_003Eb__0(AsyncToken token)
    		{
    			return default(UniTask);
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006EE")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__31
    {
    		[Token(Token = "0x4001A77")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A78")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A79")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PrintText _003C_003E4__this;

    		[Token(Token = "0x4001A7A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001A7B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private _003C_003Ec__DisplayClass31_0 _003C_003E8__1;

    		[Token(Token = "0x4001A7C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<ITextPrinterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001A7D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600213F")]
    		[Address(RVA = "0x4FF740", Offset = "0x4FE140", VA = "0x1804FF740", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002140")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006EF")]
    	[CompilerGenerated]
    	private struct _003CPlayAutoVoice_003Ed__40
    {
    		[Token(Token = "0x4001A7E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A7F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A80")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PrintText _003C_003E4__this;

    		[Token(Token = "0x4001A81")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ITextPrinterActor printer;

    		[Token(Token = "0x4001A82")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x4001A83")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<bool>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001A84")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6002141")]
    		[Address(RVA = "0x4FFDD0", Offset = "0x4FE7D0", VA = "0x1804FFDD0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002142")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006F0")]
    	[CompilerGenerated]
    	private struct _003CPreloadResources_003Ed__29
    {
    		[Token(Token = "0x4001A85")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A86")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A87")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PrintText _003C_003E4__this;

    		[Token(Token = "0x4001A88")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001A89")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask<Resource>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6002143")]
    		[Address(RVA = "0x500610", Offset = "0x4FF010", VA = "0x180500610", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002144")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006F1")]
    	[CompilerGenerated]
    	private struct _003CPrint_003Ed__32
    {
    		[Token(Token = "0x4001A8A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A8B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A8C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PrintText _003C_003E4__this;

    		[Token(Token = "0x4001A8D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ITextPrinterActor printer;

    		[Token(Token = "0x4001A8E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x4001A8F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private TextPrinterMetadata _003Cmeta_003E5__2;

    		[Token(Token = "0x4001A90")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private LocalizableText _003CprintedText_003E5__3;

    		[Token(Token = "0x4001A91")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private IDisposable _003C__003E5__4;

    		[Token(Token = "0x4001A92")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001A93")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UniTask<IDisposable>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x4001A94")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    		private int _003Ci_003E5__5;

    		[Token(Token = "0x4001A95")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    		private YieldAwaitable.Awaiter _003C_003Eu__3;

    		[Token(Token = "0x6002145")]
    		[Address(RVA = "0x501030", Offset = "0x4FFA30", VA = "0x180501030", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002146")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006F2")]
    	[CompilerGenerated]
    	private struct _003CShowPrinter_003Ed__36
    {
    		[Token(Token = "0x4001A96")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A97")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A98")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PrintText _003C_003E4__this;

    		[Token(Token = "0x4001A99")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public TextPrinterMetadata meta;

    		[Token(Token = "0x4001A9A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public ITextPrinterActor printer;

    		[Token(Token = "0x4001A9B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x4001A9C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6002147")]
    		[Address(RVA = "0x5026F0", Offset = "0x5010F0", VA = "0x1805026F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002148")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006F3")]
    	[CompilerGenerated]
    	private struct _003CWaitAutoPlayDelay_003Ed__47
    {
    		[Token(Token = "0x4001A9D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A9E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A9F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PrintText _003C_003E4__this;

    		[Token(Token = "0x4001AA0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public LocalizableText text;

    		[Token(Token = "0x4001AA1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x4001AA2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private float _003CwaitUntilTime_003E5__2;

    		[Token(Token = "0x4001AA3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6002149")]
    		[Address(RVA = "0x502B10", Offset = "0x501510", VA = "0x180502B10", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600214A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006F4")]
    	[CompilerGenerated]
    	private struct _003CWaitAutoVoice_003Ed__46
    {
    		[Token(Token = "0x4001AA4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001AA5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001AA6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PrintText _003C_003E4__this;

    		[Token(Token = "0x4001AA7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001AA8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600214B")]
    		[Address(RVA = "0x503140", Offset = "0x501B40", VA = "0x180503140", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600214C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006F5")]
    	[CompilerGenerated]
    	private struct _003CWaitInput_003Ed__44
    {
    		[Token(Token = "0x4001AA9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001AAA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001AAB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PrintText _003C_003E4__this;

    		[Token(Token = "0x4001AAC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public LocalizableText text;

    		[Token(Token = "0x4001AAD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x4001AAE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600214D")]
    		[Address(RVA = "0x5033D0", Offset = "0x501DD0", VA = "0x1805033D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600214E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001A66")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public LocalizableTextParameter Text;

    	[Token(Token = "0x4001A67")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("printer")]
    	[ActorContext("TextPrinters", -1, null)]
    	public StringParameter PrinterId;

    	[Token(Token = "0x4001A68")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[ParameterAlias("author")]
    	[ActorContext("Characters", -1, null)]
    	public StringParameter AuthorId;

    	[Token(Token = "0x4001A69")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[ParameterAlias("as")]
    	public LocalizableTextParameter AuthorLabel;

    	[Token(Token = "0x4001A6A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[ParameterAlias("speed")]
    	[ParameterDefaultValue("1")]
    	public DecimalParameter RevealSpeed;

    	[Token(Token = "0x4001A6B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[ParameterAlias("reset")]
    	public BooleanParameter ResetPrinter;

    	[Token(Token = "0x4001A6C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	[ParameterAlias("default")]
    	public BooleanParameter DefaultPrinter;

    	[Token(Token = "0x4001A6D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	[ParameterAlias("waitInput")]
    	public BooleanParameter WaitForInput;

    	[Token(Token = "0x4001A6E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	public BooleanParameter Append;

    	[Token(Token = "0x4001A6F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	[ParameterAlias("fadeTime")]
    	public DecimalParameter ChangeVisibilityDuration;

    	[Token(Token = "0x4001A70")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x170004DC")]
    	protected override string AssignedPrinterId
    	{
    		[Token(Token = "0x600211D")]
    		[Address(RVA = "0x4F89A0", Offset = "0x4F73A0", VA = "0x1804F89A0", Slot = "16")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170004DD")]
    	protected override string AssignedAuthorId
    	{
    		[Token(Token = "0x600211E")]
    		[Address(RVA = "0x4F8A30", Offset = "0x4F7430", VA = "0x1804F8A30", Slot = "17")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170004DE")]
    	protected virtual float AssignedRevealSpeed
    	{
    		[Token(Token = "0x600211F")]
    		[Address(RVA = "0x4FCC80", Offset = "0x4FB680", VA = "0x1804FCC80", Slot = "23")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x170004DF")]
    	protected virtual string AutoVoicePath
    	{
    		[Token(Token = "0x6002120")]
    		[Address(RVA = "0x46D4E0", Offset = "0x46BEE0", VA = "0x18046D4E0", Slot = "24")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6002121")]
    		[Address(RVA = "0x46D4F0", Offset = "0x46BEF0", VA = "0x18046D4F0", Slot = "25")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x170004E0")]
    	protected IAudioManager AudioManager
    	{
    		[Token(Token = "0x6002122")]
    		[Address(RVA = "0x4FCCD0", Offset = "0x4FB6D0", VA = "0x1804FCCD0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170004E1")]
    	protected IScriptPlayer ScriptPlayer
    	{
    		[Token(Token = "0x6002123")]
    		[Address(RVA = "0x4FCD30", Offset = "0x4FB730", VA = "0x1804FCD30")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170004E2")]
    	protected ITextLocalizer TextLocalizer
    	{
    		[Token(Token = "0x6002124")]
    		[Address(RVA = "0x4FCD90", Offset = "0x4FB790", VA = "0x1804FCD90")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170004E3")]
    	protected CharacterMetadata AuthorMeta
    	{
    		[Token(Token = "0x6002125")]
    		[Address(RVA = "0x4FCDF0", Offset = "0x4FB7F0", VA = "0x1804FCDF0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6002126")]
    	[Address(RVA = "0x4FCEE0", Offset = "0x4FB8E0", VA = "0x1804FCEE0", Slot = "18")]
    	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__29))]
    	public override UniTask PreloadResources()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002127")]
    	[Address(RVA = "0x4FD000", Offset = "0x4FBA00", VA = "0x1804FD000", Slot = "19")]
    	public override void ReleaseResources()
    	{
    	}

    	[Token(Token = "0x6002128")]
    	[Address(RVA = "0x4FD2A0", Offset = "0x4FBCA0", VA = "0x1804FD2A0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__31))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002129")]
    	[Address(RVA = "0x4FD430", Offset = "0x4FBE30", VA = "0x1804FD430", Slot = "26")]
    	[AsyncStateMachine(typeof(_003CPrint_003Ed__32))]
    	protected virtual UniTask Print(ITextPrinterActor printer, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600212A")]
    	[Address(RVA = "0x4FD640", Offset = "0x4FC040", VA = "0x1804FD640", Slot = "27")]
    	protected virtual bool ShouldResetText(TextPrinterMetadata meta)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600212B")]
    	[Address(RVA = "0x4FD770", Offset = "0x4FC170", VA = "0x1804FD770", Slot = "28")]
    	protected virtual void ResetText(ITextPrinterActor printer)
    	{
    	}

    	[Token(Token = "0x600212C")]
    	[Address(RVA = "0x4FD800", Offset = "0x4FC200", VA = "0x1804FD800", Slot = "29")]
    	[CanBeNull]
    	protected virtual string BuildAutoVoicePath()
    	{
    		return null;
    	}

    	[Token(Token = "0x600212D")]
    	[Address(RVA = "0x4FD810", Offset = "0x4FC210", VA = "0x1804FD810", Slot = "30")]
    	[AsyncStateMachine(typeof(_003CShowPrinter_003Ed__36))]
    	protected virtual UniTask ShowPrinter(ITextPrinterActor printer, TextPrinterMetadata meta, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600212E")]
    	[Address(RVA = "0x4FDA50", Offset = "0x4FC450", VA = "0x1804FDA50", Slot = "31")]
    	protected virtual bool ShouldSetDefaultPrinter(TextPrinterMetadata meta)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600212F")]
    	[Address(RVA = "0x4FDB80", Offset = "0x4FC580", VA = "0x1804FDB80", Slot = "32")]
    	protected virtual void SetDefaultPrinter(ITextPrinterActor defaultPrinter, AsyncToken token)
    	{
    	}

    	[Token(Token = "0x6002130")]
    	[Address(RVA = "0x4FE180", Offset = "0x4FCB80", VA = "0x1804FE180", Slot = "33")]
    	protected virtual bool ShouldPlayAutoVoice()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6002131")]
    	[Address(RVA = "0x4FE2B0", Offset = "0x4FCCB0", VA = "0x1804FE2B0", Slot = "34")]
    	[AsyncStateMachine(typeof(_003CPlayAutoVoice_003Ed__40))]
    	protected virtual UniTask PlayAutoVoice(ITextPrinterActor printer, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002132")]
    	[Address(RVA = "0x4FE4A0", Offset = "0x4FCEA0", VA = "0x1804FE4A0", Slot = "35")]
    	protected virtual UniTask Print(LocalizableText text, ITextPrinterActor printer, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002133")]
    	[Address(RVA = "0x4FE8A0", Offset = "0x4FD2A0", VA = "0x1804FE8A0", Slot = "36")]
    	protected virtual bool ShouldWaitForInput(TextPrinterMetadata meta, AsyncToken token)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6002134")]
    	[Address(RVA = "0x4FE9A0", Offset = "0x4FD3A0", VA = "0x1804FE9A0", Slot = "37")]
    	protected virtual bool ShouldAllowRollbackWhenInputNotAwaited(AsyncToken token)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6002135")]
    	[Address(RVA = "0x4FEA60", Offset = "0x4FD460", VA = "0x1804FEA60", Slot = "38")]
    	[AsyncStateMachine(typeof(_003CWaitInput_003Ed__44))]
    	protected virtual UniTask WaitInput(LocalizableText text, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002136")]
    	[Address(RVA = "0x4FEC50", Offset = "0x4FD650", VA = "0x1804FEC50", Slot = "39")]
    	protected virtual void AddBacklog(LocalizableText text, TextPrinterMetadata meta)
    	{
    	}

    	[Token(Token = "0x6002137")]
    	[Address(RVA = "0x4FEF70", Offset = "0x4FD970", VA = "0x1804FEF70", Slot = "40")]
    	[AsyncStateMachine(typeof(_003CWaitAutoVoice_003Ed__46))]
    	protected virtual UniTask WaitAutoVoice(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002138")]
    	[Address(RVA = "0x4FF100", Offset = "0x4FDB00", VA = "0x1804FF100", Slot = "41")]
    	[AsyncStateMachine(typeof(_003CWaitAutoPlayDelay_003Ed__47))]
    	protected virtual UniTask WaitAutoPlayDelay(LocalizableText text, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002139")]
    	[Address(RVA = "0x4FF2F0", Offset = "0x4FDCF0", VA = "0x1804FF2F0", Slot = "42")]
    	protected virtual bool IsPlayingAutoVoice()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600213A")]
    	[Address(RVA = "0x4FF430", Offset = "0x4FDE30", VA = "0x1804FF430")]
    	public PrintText()
    	{
    	}
    }
}
