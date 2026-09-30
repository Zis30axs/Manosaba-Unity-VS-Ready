using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006A9")]
    public class HideUI : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006AA")]
    	[CompilerGenerated]
    	private struct _003CHide_003Ed__5
    {
    		[Token(Token = "0x4001986")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001987")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001988")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public HideUI _003C_003E4__this;

    		[Token(Token = "0x4001989")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private PooledObject<List<UniTask>> _003C__003E5__2;

    		[Token(Token = "0x400198A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6002054")]
    		[Address(RVA = "0x4E8A90", Offset = "0x4E7490", VA = "0x1804E8A90", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002055")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001982")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[ResourceContext("UI", -1, null)]
    	public StringListParameter UINames;

    	[Token(Token = "0x4001983")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterDefaultValue("false")]
    	public BooleanParameter AllowToggle;

    	[Token(Token = "0x4001984")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[ParameterAlias("time")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x4001985")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x6002051")]
    	[Address(RVA = "0x4E88C0", Offset = "0x4E72C0", VA = "0x1804E88C0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002052")]
    	[Address(RVA = "0x4E8970", Offset = "0x4E7370", VA = "0x1804E8970", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CHide_003Ed__5))]
    	protected virtual UniTask Hide(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002053")]
    	[Address(RVA = "0x4D2580", Offset = "0x4D0F80", VA = "0x1804D2580")]
    	public HideUI()
    	{
    	}
    }
}
