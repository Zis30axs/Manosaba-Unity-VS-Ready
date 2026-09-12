using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x200001A")]
public static class ActorManagerExtensions
{
	[StructLayout(3)]
	[Token(Token = "0x200001B")]
	[CompilerGenerated]
	private struct _003CGetOrAddActor_003Ed__0
{
		[Token(Token = "0x4000079")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400007A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IActor> _003C_003Et__builder;

		[Token(Token = "0x400007B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public IActorManager manager;

		[Token(Token = "0x400007C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string actorId;

		[Token(Token = "0x400007D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<IActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000068")]
		[Address(RVA = "0x44B930", Offset = "0x44A330", VA = "0x18044B930", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000069")]
		[Address(RVA = "0x44BC50", Offset = "0x44A650", VA = "0x18044BC50", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200001C")]
	[CompilerGenerated]
	private struct _003CGetOrAddActor_003Ed__1<TActor, TState, TMeta, TConfig> where TActor : IActor where TState : ActorState<TActor>, new() where TMeta : ActorMetadata where TConfig : ActorManagerConfiguration<TMeta>
{
		[Token(Token = "0x400007E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400007F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<TActor> _003C_003Et__builder;

		[Token(Token = "0x4000080")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public IActorManager<TActor, TState, TMeta, TConfig> manager;

		[Token(Token = "0x4000081")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string actorId;

		[Token(Token = "0x4000082")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600006A")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600006B")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x6000065")]
	[Address(RVA = "0x44B740", Offset = "0x44A140", VA = "0x18044B740")]
	[AsyncStateMachine(typeof(_003CGetOrAddActor_003Ed__0))]
	public static UniTask<IActor> GetOrAddActor(this IActorManager manager, string actorId)
	{
		return default(UniTask<IActor>);
	}

	[Token(Token = "0x6000066")]
	[AsyncStateMachine(typeof(_003CGetOrAddActor_003Ed__1<, , , >))]
	public static UniTask<TActor> GetOrAddActor<TActor, TState, TMeta, TConfig>(this IActorManager<TActor, TState, TMeta, TConfig> manager, string actorId) where TActor : IActor where TState : ActorState<TActor>, new() where TMeta : ActorMetadata where TConfig : ActorManagerConfiguration<TMeta>
	{
		return default(UniTask<TActor>);
	}

	[Token(Token = "0x6000067")]
	public static TMeta GetActorMetaOrDefault<TActor, TState, TMeta, TConfig>(this IActorManager<TActor, TState, TMeta, TConfig> manager, string actorId) where TActor : IActor where TState : ActorState<TActor>, new() where TMeta : ActorMetadata where TConfig : ActorManagerConfiguration<TMeta>
	{
		return null;
	}
}
