using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000462")]
[InitializeAtRuntime(2147483647, null)]
public class SpawnManager : IStatefulService<GameStateMap>, IEngineService, ISpawnManager, IEngineService<SpawnConfiguration>
{
	[Serializable]
	[Token(Token = "0x2000463")]
	public class GameState
	{
		[Token(Token = "0x4001059")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public List<SpawnedObjectState> SpawnedObjects;

		[Token(Token = "0x60015B8")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public GameState()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000469")]
	[CompilerGenerated]
	private struct _003CHoldResources_003Ed__16
{
		[Token(Token = "0x4001069")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400106A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400106B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public string path;

		[Token(Token = "0x400106C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public SpawnManager _003C_003E4__this;

		[Token(Token = "0x400106D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public object holder;

		[Token(Token = "0x400106E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<Resource<GameObject>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60015C4")]
		[Address(RVA = "0x5D7B80", Offset = "0x5D6580", VA = "0x1805D7B80", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60015C5")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200046A")]
	[CompilerGenerated]
	private struct _003CLoadServiceState_003Ed__15
{
		[Token(Token = "0x400106F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001070")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001071")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnManager _003C_003E4__this;

		[Token(Token = "0x4001072")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public GameStateMap stateMap;

		[Token(Token = "0x4001073")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60015C6")]
		[Address(RVA = "0x5D7DF0", Offset = "0x5D67F0", VA = "0x1805D7DF0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60015C7")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200046B")]
	[CompilerGenerated]
	private struct _003CSpawn_003Ed__18
{
		[Token(Token = "0x4001074")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001075")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<SpawnedObject> _003C_003Et__builder;

		[Token(Token = "0x4001076")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public SpawnManager _003C_003E4__this;

		[Token(Token = "0x4001077")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string path;

		[Token(Token = "0x4001078")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x4001079")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<Resource<GameObject>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400107A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask<GameObject>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60015C8")]
		[Address(RVA = "0x5D8460", Offset = "0x5D6E60", VA = "0x1805D8460", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60015C9")]
		[Address(RVA = "0x5D8A80", Offset = "0x5D7480", VA = "0x1805D8A80", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001055")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private readonly Dictionary<string, SpawnedObject> spawnedMap;

	[Token(Token = "0x4001056")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly IResourceProviderManager providersManager;

	[Token(Token = "0x4001057")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private ResourceLoader<GameObject> loader;

	[Token(Token = "0x4001058")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private GameObject container;

	[Token(Token = "0x17000306")]
	public virtual SpawnConfiguration Configuration
	{
		[Token(Token = "0x60015A9")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "17")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000307")]
	public virtual IReadOnlyCollection<SpawnedObject> Spawned
	{
		[Token(Token = "0x60015AA")]
		[Address(RVA = "0x5D58D0", Offset = "0x5D42D0", VA = "0x1805D58D0", Slot = "18")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60015AB")]
	[Address(RVA = "0x5D5920", Offset = "0x5D4320", VA = "0x1805D5920")]
	public SpawnManager(SpawnConfiguration config = default, IResourceProviderManager providersManager = default)
	{
	}

	[Token(Token = "0x60015AC")]
	[Address(RVA = "0x5D5AA0", Offset = "0x5D44A0", VA = "0x1805D5AA0", Slot = "19")]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60015AD")]
	[Address(RVA = "0x5D5C70", Offset = "0x5D4670", VA = "0x1805D5C70", Slot = "20")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x60015AE")]
	[Address(RVA = "0x5D5C90", Offset = "0x5D4690", VA = "0x1805D5C90", Slot = "21")]
	public virtual void DestroyService()
	{
	}

	[Token(Token = "0x60015AF")]
	[Address(RVA = "0x5D5CE0", Offset = "0x5D46E0", VA = "0x1805D5CE0", Slot = "22")]
	public virtual void SaveServiceState(GameStateMap stateMap)
	{
	}

	[Token(Token = "0x60015B0")]
	[Address(RVA = "0x5D5FD0", Offset = "0x5D49D0", VA = "0x1805D5FD0", Slot = "23")]
	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__15))]
	public virtual UniTask LoadServiceState(GameStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60015B1")]
	[Address(RVA = "0x5D6150", Offset = "0x5D4B50", VA = "0x1805D6150", Slot = "24")]
	[AsyncStateMachine(typeof(_003CHoldResources_003Ed__16))]
	public virtual UniTask HoldResources(string path, object holder)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60015B2")]
	[Address(RVA = "0x5D6340", Offset = "0x5D4D40", VA = "0x1805D6340", Slot = "25")]
	public virtual void ReleaseResources(string path, object holder)
	{
	}

	[Token(Token = "0x60015B3")]
	[Address(RVA = "0x5D6480", Offset = "0x5D4E80", VA = "0x1805D6480", Slot = "26")]
	[AsyncStateMachine(typeof(_003CSpawn_003Ed__18))]
	public virtual UniTask<SpawnedObject> Spawn(string path, [Optional] AsyncToken token)
	{
		return default(UniTask<SpawnedObject>);
	}

	[Token(Token = "0x60015B4")]
	[Address(RVA = "0x5D66D0", Offset = "0x5D50D0", VA = "0x1805D66D0", Slot = "27")]
	public virtual void DestroySpawned(string path)
	{
	}

	[Token(Token = "0x60015B5")]
	[Address(RVA = "0x5D6780", Offset = "0x5D5180", VA = "0x1805D6780", Slot = "28")]
	public virtual bool IsSpawned(string path)
	{
		return default(bool);
	}

	[Token(Token = "0x60015B6")]
	[Address(RVA = "0x5D6800", Offset = "0x5D5200", VA = "0x1805D6800", Slot = "29")]
	public virtual SpawnedObject GetSpawned(string path)
	{
		return null;
	}

	[Token(Token = "0x60015B7")]
	[Address(RVA = "0x5D6870", Offset = "0x5D5270", VA = "0x1805D6870", Slot = "30")]
	protected virtual void DestroyAllSpawned()
	{
	}
}
