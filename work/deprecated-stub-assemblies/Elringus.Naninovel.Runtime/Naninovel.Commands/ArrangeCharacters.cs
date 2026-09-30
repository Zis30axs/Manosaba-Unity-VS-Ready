using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x200065E")]
    [CommandAlias("arrange")]
    public class ArrangeCharacters : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000663")]
    	[CompilerGenerated]
    	private struct _003CArrange_003Ed__7
    {
    		[Token(Token = "0x4001840")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001841")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001842")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ArrangeCharacters _003C_003E4__this;

    		[Token(Token = "0x4001843")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001844")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private PooledObject<List<UniTask>> _003C__003E5__2;

    		[Token(Token = "0x4001845")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001F6A")]
    		[Address(RVA = "0x4D40B0", Offset = "0x4D2AB0", VA = "0x1804D40B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001F6B")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001833")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[ActorContext("Characters", 0, null)]
    	public NamedDecimalListParameter CharacterPositions;

    	[Token(Token = "0x4001834")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("look")]
    	[ParameterDefaultValue("true")]
    	public BooleanParameter LookAtOrigin;

    	[Token(Token = "0x4001835")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[ParameterAlias("time")]
    	[ParameterDefaultValue("0.35")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x4001836")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x170004A1")]
    	protected ICharacterManager Characters
    	{
    		[Token(Token = "0x6001F5B")]
    		[Address(RVA = "0x4D3880", Offset = "0x4D2280", VA = "0x1804D3880")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6001F5C")]
    	[Address(RVA = "0x4D38E0", Offset = "0x4D22E0", VA = "0x1804D38E0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001F5D")]
    	[Address(RVA = "0x4D3990", Offset = "0x4D2390", VA = "0x1804D3990", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CArrange_003Ed__7))]
    	protected virtual UniTask Arrange(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001F5E")]
    	[Address(RVA = "0x325690", Offset = "0x324090", VA = "0x180325690")]
    	public ArrangeCharacters()
    	{
    	}
    }
}
