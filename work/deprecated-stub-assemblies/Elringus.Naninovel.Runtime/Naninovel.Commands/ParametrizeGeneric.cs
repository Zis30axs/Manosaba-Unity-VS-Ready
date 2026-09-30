using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006E6")]
    [CommandAlias("<")]
    public class ParametrizeGeneric : Command, Command.IPreloadable, Command.ILocalizable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006E7")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__8
    {
    		[Token(Token = "0x4001A5D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A5E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A5F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ParametrizeGeneric _003C_003E4__this;

    		[Token(Token = "0x4001A60")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask<System.IDisposable>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6002110")]
    		[Address(RVA = "0x4FC130", Offset = "0x4FAB30", VA = "0x1804FC130", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002111")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001A57")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("printer")]
    	[ActorContext("TextPrinters", -1, null)]
    	public StringParameter PrinterId;

    	[Token(Token = "0x4001A58")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("author")]
    	[ActorContext("Characters", -1, null)]
    	public StringParameter AuthorId;

    	[Token(Token = "0x4001A59")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[ParameterAlias("as")]
    	public LocalizableTextParameter AuthorLabel;

    	[Token(Token = "0x4001A5A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[ParameterAlias("speed")]
    	public DecimalParameter RevealSpeed;

    	[Token(Token = "0x4001A5B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[ParameterAlias("skip")]
    	public BooleanParameter SkipWaitingInput;

    	[Token(Token = "0x4001A5C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[ParameterAlias("join")]
    	public BooleanParameter Join;

    	[Token(Token = "0x600210C")]
    	[Address(RVA = "0x4FBE90", Offset = "0x4FA890", VA = "0x1804FBE90", Slot = "16")]
    	public UniTask PreloadResources()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600210D")]
    	[Address(RVA = "0x4FBF50", Offset = "0x4FA950", VA = "0x1804FBF50", Slot = "17")]
    	public void ReleaseResources()
    	{
    	}

    	[Token(Token = "0x600210E")]
    	[Address(RVA = "0x4FC010", Offset = "0x4FAA10", VA = "0x1804FC010", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__8))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600210F")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public ParametrizeGeneric()
    	{
    	}
    }
}
