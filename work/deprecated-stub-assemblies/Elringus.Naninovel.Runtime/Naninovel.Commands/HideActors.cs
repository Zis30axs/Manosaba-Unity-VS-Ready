using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x2000664")]
    [CommandAlias("hide")]
    public class HideActors : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000668")]
    	[CompilerGenerated]
    	private struct _003CHide_003Ed__5
    {
    		[Token(Token = "0x400184E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400184F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001850")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public HideActors _003C_003E4__this;

    		[Token(Token = "0x4001851")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001852")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private PooledObject<List<IActorManager>> _003C__003E5__2;

    		[Token(Token = "0x4001853")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private PooledObject<List<UniTask>> _003C___003E5__3;

    		[Token(Token = "0x4001854")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001F77")]
    		[Address(RVA = "0x4D6460", Offset = "0x4D4E60", VA = "0x1804D6460", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001F78")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000669")]
    	[CompilerGenerated]
    	private struct _003CHideInManager_003Ed__6
    {
    		[Token(Token = "0x4001855")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001856")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001857")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public IActorManager manager;

    		[Token(Token = "0x4001858")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string actorId;

    		[Token(Token = "0x4001859")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public HideActors _003C_003E4__this;

    		[Token(Token = "0x400185A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x400185B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001F79")]
    		[Address(RVA = "0x4D7010", Offset = "0x4D5A10", VA = "0x1804D7010", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001F7A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001846")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	[ActorContext("*", -1, null)]
    	public StringListParameter ActorIds;

    	[Token(Token = "0x4001847")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("time")]
    	[ParameterDefaultValue("0.35")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x4001848")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[ParameterDefaultValue("false")]
    	public BooleanParameter Lazy;

    	[Token(Token = "0x4001849")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x6001F6C")]
    	[Address(RVA = "0x4D5C00", Offset = "0x4D4600", VA = "0x1804D5C00", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001F6D")]
    	[Address(RVA = "0x4D5CB0", Offset = "0x4D46B0", VA = "0x1804D5CB0", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CHide_003Ed__5))]
    	protected virtual UniTask Hide(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001F6E")]
    	[Address(RVA = "0x4D5E50", Offset = "0x4D4850", VA = "0x1804D5E50", Slot = "17")]
    	[AsyncStateMachine(typeof(_003CHideInManager_003Ed__6))]
    	protected virtual UniTask HideInManager(string actorId, IActorManager manager, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001F6F")]
    	[Address(RVA = "0x4D6090", Offset = "0x4D4A90", VA = "0x1804D6090")]
    	public HideActors()
    	{
    	}
    }
}
