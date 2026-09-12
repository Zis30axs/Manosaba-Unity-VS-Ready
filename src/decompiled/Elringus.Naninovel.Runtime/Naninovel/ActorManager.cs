using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x200000F")]
public abstract class ActorManager<TActor, TState, TMeta, TConfig> where TActor : IActor where TState : ActorState<TActor>, new() where TMeta : ActorMetadata where TConfig : ActorManagerConfiguration<TMeta>
{
	[Serializable]
	[Token(Token = "0x2000010")]
	public class GameState : ISerializationCallbackReceiver
	{
		[Token(Token = "0x400004E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private SerializableLiteralStringMap actorsJsonMap;

		[Token(Token = "0x17000006")]
		public Dictionary<string, TState> ActorsMap
		{
			[Token(Token = "0x6000045")]
			[CompilerGenerated]
			get
			{
				return null;
			}
			[Token(Token = "0x6000046")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x6000047")]
		public virtual void OnBeforeSerialize()
		{
		}

		[Token(Token = "0x6000048")]
		public virtual void OnAfterDeserialize()
		{
		}

		[Token(Token = "0x6000049")]
		public GameState()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000012")]
	[CompilerGenerated]
	private struct _003CAddActor_003Ed__27
{
		[Token(Token = "0x4000051")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000052")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<TActor> _003C_003Et__builder;

		[Token(Token = "0x4000053")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ActorManager<TActor, TState, TMeta, TConfig> _003C_003E4__this;

		[Token(Token = "0x4000054")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string actorId;

		[Token(Token = "0x4000055")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600004D")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600004E")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000013")]
	[CompilerGenerated]
	private struct _003CAddActor_003Ed__29
{
		[Token(Token = "0x4000056")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000057")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<TActor> _003C_003Et__builder;

		[Token(Token = "0x4000058")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string actorId;

		[Token(Token = "0x4000059")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TState state;

		[Token(Token = "0x400005A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ActorManager<TActor, TState, TMeta, TConfig> _003C_003E4__this;

		[Token(Token = "0x400005B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private TActor _003Cactor_003E5__2;

		[Token(Token = "0x400005C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400005D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600004F")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000050")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000014")]
	[CompilerGenerated]
	private struct _003CConstructActor_003Ed__38
{
		[Token(Token = "0x400005E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400005F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<TActor> _003C_003Et__builder;

		[Token(Token = "0x4000060")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ActorManager<TActor, TState, TMeta, TConfig> _003C_003E4__this;

		[Token(Token = "0x4000061")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string actorId;

		[Token(Token = "0x4000062")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private TActor _003Cactor_003E5__2;

		[Token(Token = "0x4000063")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000051")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000052")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000015")]
	[CompilerGenerated]
	private struct _003CGetOrAddActor_003Ed__32
{
		[Token(Token = "0x4000064")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000065")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<TActor> _003C_003Et__builder;

		[Token(Token = "0x4000066")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ActorManager<TActor, TState, TMeta, TConfig> _003C_003E4__this;

		[Token(Token = "0x4000067")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string actorId;

		[Token(Token = "0x4000068")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000053")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000054")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000016")]
	[CompilerGenerated]
	private struct _003CLoadServiceState_003Ed__25
{
		[Token(Token = "0x4000069")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400006A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400006B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public GameStateMap stateMap;

		[Token(Token = "0x400006C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ActorManager<TActor, TState, TMeta, TConfig> _003C_003E4__this;

		[Token(Token = "0x400006D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Dictionary<string, TState>.Enumerator _003C_003E7__wrap1;

		[Token(Token = "0x400006E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private KeyValuePair<string, TState> _003Ckv_003E5__3;

		[Token(Token = "0x400006F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000070")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000055")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000056")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000017")]
	[CompilerGenerated]
	private struct _003CNaninovel_002DIActorManager_002DAddActor_003Ed__28
{
		[Token(Token = "0x4000071")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000072")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<IActor> _003C_003Et__builder;

		[Token(Token = "0x4000073")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ActorManager<TActor, TState, TMeta, TConfig> _003C_003E4__this;

		[Token(Token = "0x4000074")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string actorId;

		[Token(Token = "0x4000075")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000057")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000058")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000049")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	protected readonly Dictionary<string, TActor> ManagedActors;

	[Token(Token = "0x400004A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	protected readonly Dictionary<string, IResourceLoader> AppearanceLoaders;

	[Token(Token = "0x400004B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static IReadOnlyDictionary<string, Type> implNameToType;

	[Token(Token = "0x400004C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly Dictionary<string, UniTaskCompletionSource<TActor>> pendingAddActorTasks;

	[Token(Token = "0x17000002")]
	public TConfig Configuration
	{
		[Token(Token = "0x600002D")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000003")]
	public ActorManagerConfiguration ActorManagerConfiguration
	{
		[Token(Token = "0x600002E")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000004")]
	private IReadOnlyCollection<TActor> Naninovel_002EIActorManager_003CTActor_002CTState_002CTMeta_002CTConfig_003E_002EActors
	{
		[Token(Token = "0x600002F")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000005")]
	private IEnumerable<IActor> Naninovel_002EIActorManager_002EActors
	{
		[Token(Token = "0x6000030")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x14000001")]
	public event Action<string> OnActorAdded
	{
		[Token(Token = "0x6000029")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x600002A")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x14000002")]
	public event Action<string> OnActorRemoved
	{
		[Token(Token = "0x600002B")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x600002C")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6000031")]
	protected ActorManager(TConfig config = default)
	{
	}

	[Token(Token = "0x6000032")]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000033")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x6000034")]
	public virtual void DestroyService()
	{
	}

	[Token(Token = "0x6000035")]
	public virtual void SaveServiceState(GameStateMap stateMap)
	{
	}

	[Token(Token = "0x6000036")]
	[AsyncStateMachine(typeof(ActorManager<, , , >._003CLoadServiceState_003Ed__25))]
	public virtual UniTask LoadServiceState(GameStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000037")]
	public virtual bool ActorExists(string actorId)
	{
		return default(bool);
	}

	[Token(Token = "0x6000038")]
	[AsyncStateMachine(typeof(ActorManager<, , , >._003CAddActor_003Ed__27))]
	public virtual UniTask<TActor> AddActor(string actorId)
	{
		return default(UniTask<TActor>);
	}

	[Token(Token = "0x6000039")]
	[AsyncStateMachine(typeof(ActorManager<, , , >._003CNaninovel_002DIActorManager_002DAddActor_003Ed__28))]
	private UniTask<IActor> Naninovel_002EIActorManager_002EAddActor(string actorId)
	{
		return default(UniTask<IActor>);
	}

	[Token(Token = "0x600003A")]
	[AsyncStateMachine(typeof(ActorManager<, , , >._003CAddActor_003Ed__29))]
	public virtual UniTask<TActor> AddActor(string actorId, TState state)
	{
		return default(UniTask<TActor>);
	}

	[Token(Token = "0x600003B")]
	public virtual TActor GetActor(string actorId)
	{
		return default(TActor);
	}

	[Token(Token = "0x600003C")]
	private IActor Naninovel_002EIActorManager_002EGetActor(string actorId)
	{
		return null;
	}

	[Token(Token = "0x600003D")]
	[AsyncStateMachine(typeof(ActorManager<, , , >._003CGetOrAddActor_003Ed__32))]
	public virtual UniTask<TActor> GetOrAddActor(string actorId)
	{
		return default(UniTask<TActor>);
	}

	[Token(Token = "0x600003E")]
	public virtual void RemoveActor(string actorId)
	{
	}

	[Token(Token = "0x600003F")]
	public virtual void RemoveAllActors()
	{
	}

	[Token(Token = "0x6000040")]
	private ActorState Naninovel_002EIActorManager_002EGetActorState(string actorId)
	{
		return null;
	}

	[Token(Token = "0x6000041")]
	public virtual TState GetActorState(string actorId)
	{
		return null;
	}

	[Token(Token = "0x6000042")]
	public IResourceLoader GetAppearanceLoader(string actorId)
	{
		return null;
	}

	[Token(Token = "0x6000043")]
	[AsyncStateMachine(typeof(ActorManager<, , , >._003CConstructActor_003Ed__38))]
	protected virtual UniTask<TActor> ConstructActor(string actorId)
	{
		return default(UniTask<TActor>);
	}

	[Token(Token = "0x6000044")]
	protected virtual Type GetImplementationType(TMeta meta)
	{
		return null;
	}
}
