using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x2000022")]
public static class MultipliableActorManagerExtensions
{
	[StructLayout(3)]
	[Token(Token = "0x2000023")]
	[CompilerGenerated]
	private struct _003CGetOrAddActor_003Ed__0
{
		[Token(Token = "0x400005A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400005B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IActor> _003C_003Et__builder;

		[Token(Token = "0x400005C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public IMultipliableActorManager<IActor> manager;

		[Token(Token = "0x400005D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string actorId;

		[Token(Token = "0x400005E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string subId;

		[Token(Token = "0x400005F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<IActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000059")]
		[Address(RVA = "0xD18DD0", Offset = "0xD177D0", VA = "0x180D18DD0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600005A")]
		[Address(RVA = "0xD19100", Offset = "0xD17B00", VA = "0x180D19100", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000024")]
	[CompilerGenerated]
	private struct _003CGetOrAddActor_003Ed__1<TActor> where TActor : IActor
{
		[Token(Token = "0x4000060")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000061")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<TActor> _003C_003Et__builder;

		[Token(Token = "0x4000062")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public IMultipliableActorManager<TActor> manager;

		[Token(Token = "0x4000063")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string actorId;

		[Token(Token = "0x4000064")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string subId;

		[Token(Token = "0x4000065")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600005B")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600005C")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x6000057")]
	[Address(RVA = "0xD18B70", Offset = "0xD17570", VA = "0x180D18B70")]
	[AsyncStateMachine(typeof(_003CGetOrAddActor_003Ed__0))]
	public static UniTask<IActor> GetOrAddActor(this IMultipliableActorManager<IActor> manager, string actorId, string subId)
	{
		return default(UniTask<IActor>);
	}

	[Token(Token = "0x6000058")]
	[AsyncStateMachine(typeof(_003CGetOrAddActor_003Ed__1<>))]
	public static UniTask<TActor> GetOrAddActor<TActor>(this IMultipliableActorManager<TActor> manager, string actorId, string subId) where TActor : IActor
	{
		return default(UniTask<TActor>);
	}
}
