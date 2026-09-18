using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000005")]
    [UsedImplicitly]
    [InitializeAtRuntime(0, typeof(BackgroundManager))]
    public class BackgroundManagerExtended : BackgroundManager
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000007")]
    	[CompilerGenerated]
    	private struct _003CAddActor_003Ed__4
    {
    		[Token(Token = "0x4000008")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000009")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<IBackgroundActor> _003C_003Et__builder;

    		[Token(Token = "0x400000A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public BackgroundManagerExtended _003C_003E4__this;

    		[Token(Token = "0x400000B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string actorId;

    		[Token(Token = "0x400000C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string subId;

    		[Token(Token = "0x400000D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<IBackgroundActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000013")]
    		[Address(RVA = "0xD11F80", Offset = "0xD10980", VA = "0x180D11F80", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000014")]
    		[Address(RVA = "0xD125D0", Offset = "0xD10FD0", VA = "0x180D125D0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000008")]
    	[CompilerGenerated]
    	private struct _003CConstructActorAsync_003Ed__9
    {
    		[Token(Token = "0x400000E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400000F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<IBackgroundActor> _003C_003Et__builder;

    		[Token(Token = "0x4000010")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public BackgroundManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000011")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string actorId;

    		[Token(Token = "0x4000012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string subId;

    		[Token(Token = "0x4000013")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private IBackgroundActor _003Cactor_003E5__2;

    		[Token(Token = "0x4000014")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000015")]
    		[Address(RVA = "0xD12600", Offset = "0xD11000", VA = "0x180D12600", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000016")]
    		[Address(RVA = "0xD13370", Offset = "0xD11D70", VA = "0x180D13370", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000009")]
    	[CompilerGenerated]
    	private struct _003CGetOrAddActor_003Ed__7
    {
    		[Token(Token = "0x4000015")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000016")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<IBackgroundActor> _003C_003Et__builder;

    		[Token(Token = "0x4000017")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string actorId;

    		[Token(Token = "0x4000018")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public BackgroundManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000019")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<IBackgroundActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000017")]
    		[Address(RVA = "0xD133A0", Offset = "0xD11DA0", VA = "0x180D133A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000018")]
    		[Address(RVA = "0xD13800", Offset = "0xD12200", VA = "0x180D13800", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000A")]
    	[CompilerGenerated]
    	private struct _003CGetOrAddActor_003Ed__8
    {
    		[Token(Token = "0x400001A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400001B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<IBackgroundActor> _003C_003Et__builder;

    		[Token(Token = "0x400001C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public BackgroundManagerExtended _003C_003E4__this;

    		[Token(Token = "0x400001D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string actorId;

    		[Token(Token = "0x400001E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string subId;

    		[Token(Token = "0x400001F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<IBackgroundActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000019")]
    		[Address(RVA = "0xD13830", Offset = "0xD12230", VA = "0x180D13830", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600001A")]
    		[Address(RVA = "0xD13B40", Offset = "0xD12540", VA = "0x180D13B40", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000B")]
    	[CompilerGenerated]
    	private struct _003CLoadServiceState_003Ed__6
    {
    		[Token(Token = "0x4000020")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000021")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000022")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GameStateMap stateMap;

    		[Token(Token = "0x4000023")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public BackgroundManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000024")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private IEnumerator<KeyValuePair<string, BackgroundStateExtended>> _003C_003E7__wrap1;

    		[Token(Token = "0x4000025")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private BackgroundStateExtended _003CextendedState_003E5__3;

    		[Token(Token = "0x4000026")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<IBackgroundActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000027")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600001B")]
    		[Address(RVA = "0xD13B70", Offset = "0xD12570", VA = "0x180D13B70", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600001C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000006")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private readonly MultipliableActorManagerUtility<IBackgroundActor> _utility;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD10DF0, 257 B
    	// ABI: rcx=__this, rdx=config, r8=cameraConfig (+ MethodInfo*). rdi=__this, rsi=config, rbx=cameraConfig.
    	// 1. `call 0x18089d280` -> Naninovel.OrthoActorManager<IBackgroundActor, BackgroundState,
    	//    BackgroundMetadata, BackgroundsConfiguration>$$.ctor with rdx=rsi=config, r8=rbx=cameraConfig, r9=MethodInfo*.
    	//    That call is the inlined body of `Naninovel.BackgroundManager..ctor(config, cameraConfig)`
    	//    (dump.cs RVA 0x44C530, whose only statement is this base call) == base(config, cameraConfig).
    	// 2. `_utility` (+0x48) = new MultipliableActorManagerUtility<IBackgroundActor>()
    	//    (il2cpp_codegen_object_new + the utility type's .ctor). The store happens AFTER the base call, so it
    	//    is a constructor-body statement: for comparison, the base `CharacterManager.avatarPathByCharId`
    	//    (+0x60) is initialised BEFORE the base call in the sibling class, which is the field-initializer shape.
    	//    TODO(evidence): a field-initializer spelling would be behaviourally identical here (MSVC LTCG could in
    	//    principle sink it past the base call); the emitted order is reproduced as written.
    	[Token(Token = "0x6000007")]
    	[Address(RVA = "0xD10DF0", Offset = "0xD0F7F0", VA = "0x180D10DF0")]
    	public BackgroundManagerExtended(BackgroundsConfiguration config, CameraConfiguration cameraConfig)
    		: base(config, cameraConfig)
    	{
    		_utility = new MultipliableActorManagerUtility<IBackgroundActor>();
    	}

    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0xD10F00", Offset = "0xD0F900", VA = "0x180D10F00", Slot = "48")]
    	public bool ActorExists(string actorId, string subId)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0xD10F90", Offset = "0xD0F990", VA = "0x180D10F90", Slot = "49")]
    	public IBackgroundActor GetActor(string actorId, string subId)
    	{
    		return null;
    	}

    	[Token(Token = "0x600000A")]
    	[Address(RVA = "0xD11020", Offset = "0xD0FA20", VA = "0x180D11020", Slot = "50")]
    	[AsyncStateMachine(typeof(_003CAddActor_003Ed__4))]
    	public UniTask<IBackgroundActor> AddActor(string actorId, string subId)
    	{
    		return default(UniTask<IBackgroundActor>);
    	}

    	[Token(Token = "0x600000B")]
    	[Address(RVA = "0xD11280", Offset = "0xD0FC80", VA = "0x180D11280", Slot = "31")]
    	public override void SaveServiceState(GameStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0xD11650", Offset = "0xD10050", VA = "0x180D11650", Slot = "32")]
    	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__6))]
    	public override UniTask LoadServiceState(GameStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0xD117E0", Offset = "0xD101E0", VA = "0x180D117E0", Slot = "37")]
    	[AsyncStateMachine(typeof(_003CGetOrAddActor_003Ed__7))]
    	public override UniTask<IBackgroundActor> GetOrAddActor(string actorId)
    	{
    		return default(UniTask<IBackgroundActor>);
    	}

    	[Token(Token = "0x600000E")]
    	[Address(RVA = "0xD119D0", Offset = "0xD103D0", VA = "0x180D119D0")]
    	[AsyncStateMachine(typeof(_003CGetOrAddActor_003Ed__8))]
    	private UniTask<IBackgroundActor> GetOrAddActor(string actorId, string subId)
    	{
    		return default(UniTask<IBackgroundActor>);
    	}

    	[Token(Token = "0x600000F")]
    	[Address(RVA = "0xD11C30", Offset = "0xD10630", VA = "0x180D11C30")]
    	[AsyncStateMachine(typeof(_003CConstructActorAsync_003Ed__9))]
    	private UniTask<IBackgroundActor> ConstructActorAsync(string actorId, string subId)
    	{
    		return default(UniTask<IBackgroundActor>);
    	}
    }
}
