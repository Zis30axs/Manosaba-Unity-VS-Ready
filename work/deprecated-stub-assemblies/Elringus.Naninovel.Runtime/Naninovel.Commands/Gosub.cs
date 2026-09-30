using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006C4")]
    [Branch(BranchTraits.Endpoint | BranchTraits.Return, null, null)]
    public class Gosub : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006C5")]
    	[CompilerGenerated]
    	private struct _003CReset_003Ed__11
    {
    		[Token(Token = "0x40019D7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40019D8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40019D9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Gosub _003C_003E4__this;

    		[Token(Token = "0x40019DA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60020A1")]
    		[Address(RVA = "0x4F09F0", Offset = "0x4EF3F0", VA = "0x1804F09F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60020A2")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40019D5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	[EndpointContext(null)]
    	public NamedStringParameter Path;

    	[Token(Token = "0x40019D6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("reset")]
    	public StringListParameter ResetState;

    	[Token(Token = "0x170004C6")]
    	protected IScriptPlayer Player
    	{
    		[Token(Token = "0x6002098")]
    		[Address(RVA = "0x4EFBC0", Offset = "0x4EE5C0", VA = "0x1804EFBC0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170004C7")]
    	protected IScriptManager Scripts
    	{
    		[Token(Token = "0x6002099")]
    		[Address(RVA = "0x4EFC20", Offset = "0x4EE620", VA = "0x1804EFC20")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x600209A")]
    	[Address(RVA = "0x4EFC80", Offset = "0x4EE680", VA = "0x1804EFC80", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600209B")]
    	[Address(RVA = "0x4EFDB0", Offset = "0x4EE7B0", VA = "0x1804EFDB0", Slot = "16")]
    	protected virtual void PushReturnSpot()
    	{
    	}

    	[Token(Token = "0x600209C")]
    	[Address(RVA = "0x4F0040", Offset = "0x4EEA40", VA = "0x1804F0040", Slot = "17")]
    	protected virtual bool TryGetScriptPathAndLabel(out string scriptPath, [MaybeNull] out string label)
    	{
    		scriptPath = default;
    		label = default;
    		scriptPath = default;
    		label = default;
    		return default(bool);
    	}

    	[Token(Token = "0x600209D")]
    	[Address(RVA = "0x4F0410", Offset = "0x4EEE10", VA = "0x1804F0410", Slot = "18")]
    	protected virtual bool ShouldNavigatePlayedScript(string scriptPath)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600209E")]
    	[Address(RVA = "0x4F0640", Offset = "0x4EF040", VA = "0x1804F0640", Slot = "19")]
    	protected virtual void NavigatePlayedScript([MaybeNull] string label)
    	{
    	}

    	[Token(Token = "0x600209F")]
    	[Address(RVA = "0x4F08D0", Offset = "0x4EF2D0", VA = "0x1804F08D0", Slot = "20")]
    	[AsyncStateMachine(typeof(_003CReset_003Ed__11))]
    	protected virtual UniTask Reset()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60020A0")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public Gosub()
    	{
    	}
    }
}
