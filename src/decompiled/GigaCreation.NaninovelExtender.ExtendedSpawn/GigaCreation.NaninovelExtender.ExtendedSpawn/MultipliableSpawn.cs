using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedSpawn;

[Token(Token = "0x2000009")]
[UsedImplicitly]
[InitializeAtRuntime(0, typeof(SpawnManager))]
public class MultipliableSpawn : SpawnManager, IMultipliableSpawnManager, ISpawnManager, IEngineService<SpawnConfiguration>, IEngineService
{
	[StructLayout(3)]
	[Token(Token = "0x200000B")]
	[CompilerGenerated]
	private struct _003CHoldResources_003Ed__9
{
		[Token(Token = "0x400001C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400001D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400001E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public string path;

		[Token(Token = "0x400001F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public MultipliableSpawn _003C_003E4__this;

		[Token(Token = "0x4000020")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public object holder;

		[Token(Token = "0x4000021")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<Resource<GameObject>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600002C")]
		[Address(RVA = "0xD2E000", Offset = "0xD2CA00", VA = "0x180D2E000", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600002D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200000C")]
	[CompilerGenerated]
	private struct _003CLoadObjectsAsync_003Ed__21
{
		[Token(Token = "0x4000022")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000023")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000024")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GameState state;

		[Token(Token = "0x4000025")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public MultipliableSpawn _003C_003E4__this;

		[Token(Token = "0x4000026")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600002E")]
		[Address(RVA = "0xD2E270", Offset = "0xD2CC70", VA = "0x180D2E270", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600002F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200000D")]
	[CompilerGenerated]
	private struct _003CLoadServiceState_003Ed__20
{
		[Token(Token = "0x4000027")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000028")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000029")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GameStateMap stateMap;

		[Token(Token = "0x400002A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public MultipliableSpawn _003C_003E4__this;

		[Token(Token = "0x400002B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000030")]
		[Address(RVA = "0xD2EB70", Offset = "0xD2D570", VA = "0x180D2EB70", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000031")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200000E")]
	[CompilerGenerated]
	private struct _003CSpawn_003Ed__11
{
		[Token(Token = "0x400002C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400002D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<SpawnedObject> _003C_003Et__builder;

		[Token(Token = "0x400002E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public MultipliableSpawn _003C_003E4__this;

		[Token(Token = "0x400002F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string path;

		[Token(Token = "0x4000030")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken asyncToken;

		[Token(Token = "0x4000031")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<Resource<GameObject>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000032")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask<GameObject>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000032")]
		[Address(RVA = "0xD2F0F0", Offset = "0xD2DAF0", VA = "0x180D2F0F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000033")]
		[Address(RVA = "0xD2F7B0", Offset = "0xD2E1B0", VA = "0x180D2F7B0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200000F")]
	[CompilerGenerated]
	private struct _003CSpawn_003Ed__12
{
		[Token(Token = "0x4000033")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000034")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<SpawnedObject> _003C_003Et__builder;

		[Token(Token = "0x4000035")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string subId;

		[Token(Token = "0x4000036")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public MultipliableSpawn _003C_003E4__this;

		[Token(Token = "0x4000037")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string path;

		[Token(Token = "0x4000038")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public AsyncToken asyncToken;

		[Token(Token = "0x4000039")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private string _003CmultipliablePath_003E5__2;

		[Token(Token = "0x400003A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask<SpawnedObject>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400003B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
		private UniTask<Resource<GameObject>>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x400003C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
		private UniTask<GameObject>.Awaiter _003C_003Eu__3;

		[Token(Token = "0x6000034")]
		[Address(RVA = "0xD2F7E0", Offset = "0xD2E1E0", VA = "0x180D2F7E0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000035")]
		[Address(RVA = "0xD300C0", Offset = "0xD2EAC0", VA = "0x180D300C0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000010")]
	[CompilerGenerated]
	private struct _003CSpawnObjectAsync_003Ed__23
{
		[Token(Token = "0x400003D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400003E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400003F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnedObjectState objState;

		[Token(Token = "0x4000040")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		public MultipliableSpawn _003C_003E4__this;

		[Token(Token = "0x4000041")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private UniTask<SpawnedObject>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000036")]
		[Address(RVA = "0xD300F0", Offset = "0xD2EAF0", VA = "0x180D300F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000037")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000016")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly Dictionary<string, SpawnedObject> _spawnedMap;

	[Token(Token = "0x4000017")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private readonly IResourceProviderManager _providersManager;

	[Token(Token = "0x4000018")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private ResourceLoader<GameObject> _loader;

	[Token(Token = "0x4000019")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private GameObject _container;

	[Token(Token = "0x17000004")]
	public override IReadOnlyCollection<SpawnedObject> Spawned
	{
		[Token(Token = "0x6000015")]
		[Address(RVA = "0xD2C270", Offset = "0xD2AC70", VA = "0x180D2C270", Slot = "18")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000016")]
	[Address(RVA = "0xD2C2C0", Offset = "0xD2ACC0", VA = "0x180D2C2C0")]
	public MultipliableSpawn(SpawnConfiguration config, IResourceProviderManager providersManager)
	{
	}

	[Token(Token = "0x6000017")]
	[Address(RVA = "0xD2C540", Offset = "0xD2AF40", VA = "0x180D2C540", Slot = "19")]
	public override UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000018")]
	[Address(RVA = "0xD2C710", Offset = "0xD2B110", VA = "0x180D2C710", Slot = "21")]
	public override void DestroyService()
	{
	}

	[Token(Token = "0x6000019")]
	[Address(RVA = "0xD2C760", Offset = "0xD2B160", VA = "0x180D2C760", Slot = "24")]
	[AsyncStateMachine(typeof(_003CHoldResources_003Ed__9))]
	public override UniTask HoldResources(string path, object holder)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600001A")]
	[Address(RVA = "0xD2C950", Offset = "0xD2B350", VA = "0x180D2C950", Slot = "25")]
	public override void ReleaseResources(string path, object holder)
	{
	}

	[Token(Token = "0x600001B")]
	[Address(RVA = "0xD2CA90", Offset = "0xD2B490", VA = "0x180D2CA90", Slot = "26")]
	[AsyncStateMachine(typeof(_003CSpawn_003Ed__11))]
	public override UniTask<SpawnedObject> Spawn(string path, [Optional] AsyncToken asyncToken)
	{
		return default(UniTask<SpawnedObject>);
	}

	[Token(Token = "0x600001C")]
	[Address(RVA = "0xD2CCE0", Offset = "0xD2B6E0", VA = "0x180D2CCE0", Slot = "31")]
	[AsyncStateMachine(typeof(_003CSpawn_003Ed__12))]
	public UniTask<SpawnedObject> Spawn(string path, string subId, [Optional] AsyncToken asyncToken)
	{
		return default(UniTask<SpawnedObject>);
	}

	[Token(Token = "0x600001D")]
	[Address(RVA = "0xD2CF90", Offset = "0xD2B990", VA = "0x180D2CF90", Slot = "27")]
	public override void DestroySpawned(string path)
	{
	}

	[Token(Token = "0x600001E")]
	[Address(RVA = "0xD2D040", Offset = "0xD2BA40", VA = "0x180D2D040", Slot = "34")]
	public void DestroySpawned(string path, string subId)
	{
	}

	[Token(Token = "0x600001F")]
	[Address(RVA = "0xD2D150", Offset = "0xD2BB50", VA = "0x180D2D150", Slot = "28")]
	public override bool IsSpawned(string path)
	{
		return default(bool);
	}

	[Token(Token = "0x6000020")]
	[Address(RVA = "0xD2D1D0", Offset = "0xD2BBD0", VA = "0x180D2D1D0", Slot = "32")]
	public bool IsSpawned(string path, string subId)
	{
		return default(bool);
	}

	[Token(Token = "0x6000021")]
	[Address(RVA = "0xD2D2B0", Offset = "0xD2BCB0", VA = "0x180D2D2B0", Slot = "29")]
	public override SpawnedObject GetSpawned(string path)
	{
		return null;
	}

	[Token(Token = "0x6000022")]
	[Address(RVA = "0xD2D320", Offset = "0xD2BD20", VA = "0x180D2D320", Slot = "33")]
	public SpawnedObject GetSpawned(string path, string subId)
	{
		return null;
	}

	[Token(Token = "0x6000023")]
	[Address(RVA = "0xD2D3F0", Offset = "0xD2BDF0", VA = "0x180D2D3F0", Slot = "22")]
	public override void SaveServiceState(GameStateMap stateMap)
	{
	}

	[Token(Token = "0x6000024")]
	[Address(RVA = "0xD2D6E0", Offset = "0xD2C0E0", VA = "0x180D2D6E0", Slot = "23")]
	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__20))]
	public override UniTask LoadServiceState(GameStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000025")]
	[Address(RVA = "0xD2D860", Offset = "0xD2C260", VA = "0x180D2D860")]
	[AsyncStateMachine(typeof(_003CLoadObjectsAsync_003Ed__21))]
	private UniTask LoadObjectsAsync(GameState state)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000026")]
	[Address(RVA = "0xD2D9E0", Offset = "0xD2C3E0", VA = "0x180D2D9E0")]
	private void UpdateObject(SpawnedObjectState objState, IList<SpawnedObject> toDestroy)
	{
	}

	[Token(Token = "0x6000027")]
	[Address(RVA = "0xD2DC30", Offset = "0xD2C630", VA = "0x180D2DC30")]
	[AsyncStateMachine(typeof(_003CSpawnObjectAsync_003Ed__23))]
	private UniTask SpawnObjectAsync(SpawnedObjectState objState)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000028")]
	[Address(RVA = "0xD2DDF0", Offset = "0xD2C7F0", VA = "0x180D2DDF0", Slot = "30")]
	protected override void DestroyAllSpawned()
	{
	}
}
