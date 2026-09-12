using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000061")]
public abstract class OrthoActorManager<TActor, TState, TMeta, TConfig> : ActorManager<TActor, TState, TMeta, TConfig> where TActor : IActor where TState : ActorState<TActor>, new() where TMeta : OrthoActorMetadata where TConfig : OrthoActorManagerConfiguration<TMeta>
{
	[StructLayout(3)]
	[Token(Token = "0x2000062")]
	[CompilerGenerated]
	private struct _003CConstructActor_003Ed__11
{
		[Token(Token = "0x4000147")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000148")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<TActor> _003C_003Et__builder;

		[Token(Token = "0x4000149")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public OrthoActorManager<TActor, TState, TMeta, TConfig> _003C_003E4__this;

		[Token(Token = "0x400014A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string actorId;

		[Token(Token = "0x400014B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60001A9")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60001AA")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x17000043")]
	protected virtual Vector2 GlobalSceneOrigin
	{
		[Token(Token = "0x60001A1")]
		get
		{
			return default(Vector2);
		}
	}

	[Token(Token = "0x17000044")]
	protected virtual float ZOffset
	{
		[Token(Token = "0x60001A2")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000045")]
	protected virtual float TopmostZPosition
	{
		[Token(Token = "0x60001A3")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000046")]
	protected virtual CameraConfiguration CameraConfiguration
	{
		[Token(Token = "0x60001A4")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60001A5")]
	protected OrthoActorManager(TConfig config = default, CameraConfiguration cameraConfig = default)
	{
	}

	[Token(Token = "0x60001A6")]
	protected virtual void MoveActorToBottom(TActor actor)
	{
	}

	[Token(Token = "0x60001A7")]
	[AsyncStateMachine(typeof(OrthoActorManager<, , , >._003CConstructActor_003Ed__11))]
	protected override UniTask<TActor> ConstructActor(string actorId)
	{
		return default(UniTask<TActor>);
	}
}
