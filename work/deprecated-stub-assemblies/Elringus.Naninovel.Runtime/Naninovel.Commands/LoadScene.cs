using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006AF")]
    public class LoadScene : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006B0")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__2
    {
    		[Token(Token = "0x4001997")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001998")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001999")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public LoadScene _003C_003E4__this;

    		[Token(Token = "0x400199A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UnityAsyncExtensions.AsyncOperationAwaiter _003C_003Eu__1;

    		[Token(Token = "0x6002062")]
    		[Address(RVA = "0x4EA450", Offset = "0x4E8E50", VA = "0x1804EA450", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002063")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001995")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public StringParameter SceneName;

    	[Token(Token = "0x4001996")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterDefaultValue("false")]
    	public BooleanParameter Additive;

    	[Token(Token = "0x6002060")]
    	[Address(RVA = "0x4EA330", Offset = "0x4E8D30", VA = "0x1804EA330", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__2))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002061")]
    	[Address(RVA = "0x4D2580", Offset = "0x4D0F80", VA = "0x1804D2580")]
    	public LoadScene()
    	{
    	}
    }
}
