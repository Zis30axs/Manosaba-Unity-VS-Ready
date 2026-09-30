using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x2000708")]
    [CommandAlias("despawnAll")]
    public class DestroyAllSpawned : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000709")]
    	[CompilerGenerated]
    	private struct _003CDestroyAll_003Ed__4
    {
    		[Token(Token = "0x4001AE2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001AE3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001AE4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DestroyAllSpawned _003C_003E4__this;

    		[Token(Token = "0x4001AE5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001AE6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private PooledObject<List<UniTask>> _003C__003E5__2;

    		[Token(Token = "0x4001AE7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private PooledObject<List<SpawnedObject>> _003C___003E5__3;

    		[Token(Token = "0x4001AE8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6002183")]
    		[Address(RVA = "0x507E80", Offset = "0x506880", VA = "0x180507E80", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002184")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001AE1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x170004E6")]
    	protected virtual ISpawnManager SpawnManager
    	{
    		[Token(Token = "0x600217F")]
    		[Address(RVA = "0x507BD0", Offset = "0x5065D0", VA = "0x180507BD0", Slot = "16")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6002180")]
    	[Address(RVA = "0x507C30", Offset = "0x506630", VA = "0x180507C30", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002181")]
    	[Address(RVA = "0x507CE0", Offset = "0x5066E0", VA = "0x180507CE0", Slot = "17")]
    	[AsyncStateMachine(typeof(_003CDestroyAll_003Ed__4))]
    	protected virtual UniTask DestroyAll(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002182")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public DestroyAllSpawned()
    	{
    	}
    }
}
