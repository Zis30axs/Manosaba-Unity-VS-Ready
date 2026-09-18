using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedSpawn
{
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2C2C0 (field initializer; 628 B total)
    	// `_spawnedMap` (+0x38) = new Dictionary<string, SpawnedObject>(): the constructor emits
    	// `il2cpp_codegen_object_new(Dictionary<string, SpawnedObject>_TypeInfo)` + that type's
    	// constructor and stores the result to +0x38 BEFORE the base constructor body (which
    	// initializes the base `spawnedMap` at +0x18 the same way) -> a C# field initializer.
    	[Token(Token = "0x4000016")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private readonly Dictionary<string, SpawnedObject> _spawnedMap = new Dictionary<string, SpawnedObject>();

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
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD2C270, 73 B
    		// `mov rcx,[rbx+0x38]` (_spawnedMap) -> tail call Dictionary<string,SpawnedObject>.get_Values.
    		// Annotated disassembly: work/spawn-nre/disas/02_get_Spawned.txt
    		[Token(Token = "0x6000015")]
    		[Address(RVA = "0xD2C270", Offset = "0xD2AC70", VA = "0x180D2C270", Slot = "18")]
    		get
    		{
    			return _spawnedMap.Values;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2C2C0, 628 B
    	// Not listed by the current compile gate log (its only diagnostic for this file is the
    	// CS0115 on `DestroySpawned`, which appears to suppress the base-call diagnostic for the
    	// class), but an implicit `base()` cannot bind against SpawnManager, so it is recovered and
    	// fixed the same way as the other six sites.
    	// ABI: rcx=__this, rdx=config, r8=providersManager.
    	// 1. field initializer (see `_spawnedMap` above): +0x38 = new Dictionary<string, SpawnedObject>().
    	// 2. inlined base constructor: +0x18 = new Dictionary<string, SpawnedObject>() (the base field
    	//    `spawnedMap`), then +0x10 <Configuration>k__BackingField = config (rdx) and +0x20 the
    	//    base field `providersManager` = providersManager (r8) == base(config, providersManager).
    	// 3. `mov [rdi+0x40], rbp` -> this._providersManager (+0x40) = providersManager.
    	// `_loader` (+0x48) and `_container` (+0x50) are NOT assigned here (set in InitializeService).
    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0xD2C2C0", Offset = "0xD2ACC0", VA = "0x180D2C2C0")]
    	public MultipliableSpawn(SpawnConfiguration config, IResourceProviderManager providersManager)
    		: base(config, providersManager)
    	{
    		_providersManager = providersManager;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2C540, 449 B
    	// Same shape as the package's SpawnManager.InitializeService, on this class' own fields:
    	//   0x180D2C5AE  virtual slot 17 on this          -> get_Configuration (SpawnManager Slot 17, dump.cs:173715)
    	//   0x180D2C5CD  mov rcx,[rax+0x18]               -> SpawnConfiguration.Loader (+0x18)
    	//   0x180D2C5E1  mov rdx,[rbx+0x40]               -> _providersManager
    	//   0x180D2C5EF  ResourceLoaderConfiguration$$CreateFor<GameObject> -> stored to [rbx+0x48] = _loader
    	//   0x180D2C66D  STR 'Spawn', Engine$$CreateObject(name, layer: null, parent: null, Array.Empty<Type>())
    	//                                                 -> stored to [rbx+0x50] = _container
    	//   0x180D2C6F4  xor eax,eax; ret                 -> default(UniTask) == UniTask.CompletedTask
    	// The base fields `loader`/`container` are never assigned by this override.
    	// Annotated disassembly: work/spawn-nre/disas/03_InitializeService.txt
    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0xD2C540", Offset = "0xD2AF40", VA = "0x180D2C540", Slot = "19")]
    	public override UniTask InitializeService()
    	{
    		_loader = Configuration.Loader.CreateFor<GameObject>(_providersManager);
    		_container = Engine.CreateObject("Spawn");
    		return UniTask.CompletedTask;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2C710, 80 B
    	//   0x180D2C71C  virtual slot 30 on this          -> DestroyAllSpawned()
    	//   0x180D2C72C  mov rcx,[rbx+0x48]; test; je     -> `_loader?.`
    	//   0x180D2C738  mov r8b,1; virtual slot 32       -> ReleaseAll(this, unload: true)
    	//   0x180D2C74E  mov rcx,[rbx+0x50]; jmp ObjectUtils$$DestroyOrImmediate -> _container (no null test; the helper tolerates null)
    	// Annotated disassembly: work/spawn-nre/disas/04_DestroyService.txt
    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0xD2C710", Offset = "0xD2B110", VA = "0x180D2C710", Slot = "21")]
    	public override void DestroyService()
    	{
    		DestroyAllSpawned();
    		_loader?.ReleaseAll(this);
    		ObjectUtils.DestroyOrImmediate(_container);
    	}

    	[Token(Token = "0x6000019")]
    	[Address(RVA = "0xD2C760", Offset = "0xD2B160", VA = "0x180D2C760", Slot = "24")]
    	[AsyncStateMachine(typeof(_003CHoldResources_003Ed__9))]
    	public override UniTask HoldResources(string path, object holder)
    	{
    		// The recovered body lives in HoldResourcesCore (see the project's async reconstruction
    		// convention in AddressableResourceProviderExtended / Overlay).
    		return HoldResourcesCore(path, holder);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2C760 (kickoff) / 0xD2E000 (<HoldResources>d__9.MoveNext, 620 B)
    	//   0x180D2E0A7  SpawnConfiguration$$ProcessInputPath(path (+0x18), out _ (rdx=&local, discarded))
    	//   0x180D2E0BC  mov rdx,[rdi+0x48]; test; je NRE -> _loader
    	//   0x180D2E0CC  virtual slot 37 on _loader       -> ResourceLoader<GameObject>.Load(resourcePath, holder (+0x28))
    	//   single await on UniTask<Resource<GameObject>>; the result is discarded (GetResult, then SetResult()).
    	// Identical to the package's SpawnManager.HoldResources on this class' own loader.
    	// Annotated disassembly: work/spawn-nre/disas/06_HoldResources_MoveNext.txt
    	private async UniTask HoldResourcesCore(string path, object holder)
    	{
    		var resourcePath = SpawnConfiguration.ProcessInputPath(path, out _);
    		await _loader.Load(resourcePath, holder);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2C950, 320 B
    	// Slot arithmetic on the ResourceLoader<GameObject> vtable ((off - 0x138) / 16, dump.cs:155753..):
    	//   0x180D2C99C  slot 35 IsLoaded(resourcePath)                 -> `if (!IsLoaded) return`
    	//   0x180D2C9CE  slot 31 Release(resourcePath, holder, r9b=0)  -> unload: false
    	//   0x180D2C9F7  slot 34 CountHolders(resourcePath); test eax; jne return
    	//   0x180D2CA14  virtual slot 28 on this IsSpawned(path); then slot 27 DestroySpawned(path)
    	//   0x180D2CA56  slot 31 Release(resourcePath, holder, r9b=1)  -> unload: true (the default)
    	// Identical to the package's SpawnManager.ReleaseResources on this class' own loader.
    	// Annotated disassembly: work/spawn-nre/disas/07_ReleaseResources.txt
    	[Token(Token = "0x600001A")]
    	[Address(RVA = "0xD2C950", Offset = "0xD2B350", VA = "0x180D2C950", Slot = "25")]
    	public override void ReleaseResources(string path, object holder)
    	{
    		var resourcePath = SpawnConfiguration.ProcessInputPath(path, out _);
    		if (!_loader.IsLoaded(resourcePath)) return;

    		_loader.Release(resourcePath, holder, false);
    		if (_loader.CountHolders(resourcePath) == 0)
    		{
    			if (IsSpawned(path))
    				DestroySpawned(path);
    			_loader.Release(resourcePath, holder);
    		}
    	}

    	[Token(Token = "0x600001B")]
    	[Address(RVA = "0xD2CA90", Offset = "0xD2B490", VA = "0x180D2CA90", Slot = "26")]
    	[AsyncStateMachine(typeof(_003CSpawn_003Ed__11))]
    	public override UniTask<SpawnedObject> Spawn(string path, [Optional] AsyncToken asyncToken)
    	{
    		return SpawnCore(path, asyncToken);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2CA90 (kickoff) / 0xD2F0F0 (<Spawn>d__11.MoveNext, 1713 B)
    	// State -1 (first run), in order:
    	//   0x180D2F278  virtual slot 28 on this: IsSpawned(path (+0x28)); true -> 0x180D2F731:
    	//                STR 'Object `' + path + STR "` is already spawned and can't be spawned again before it's destroyed."
    	//                -> new Naninovel.Error(msg) -> throw. (Back-tick quotes: an older Naninovel spelling
    	//                than the package's single quotes at 0x5D8A1D.)
    	//   0x180D2F2AC  SpawnConfiguration$$ProcessInputPath(path, out _)
    	//   0x180D2F2B1  mov rdx,[rsi+0x48] (_loader); slot 37 -> Load(resourcePath, holder: this (r9=rsi))
    	//                NOTE: plain Load, not LoadOrErr (the package base calls
    	//                ResourceLoaderExtensions$$LoadOrErr<object> at 0x5D864D).
    	//   await; 0x180D2F3B0  AsyncToken$$ThrowIfCanceled(&asyncToken (+0x30))
    	//   0x180D2F3C8  mov rcx,[r14+0x18] (Resource.Object); ObjectUtils$$IsValid; false -> 0x180D2F6B8:
    	//                a SECOND throw block with the very same two string literals (0x182E2FD10) as the
    	//                IsSpawned throw -> `if (!prefabResource.Valid) throw new Error("Object `{path}` is
    	//                already spawned ...")`. The original game code re-used the wrong message for the
    	//                "resource not found" case.
    	//                DEVIATION (message text only, semantics kept): the thrown message names the actual
    	//                failure, because the misleading April text would hide resource-registration bugs
    	//                during reconstruction. TODO(evidence): none needed -- the two blocks are separate in
    	//                the binary (0x6B8 vs 0x731), so this is a source-level duplicate, not folding.
    	//   0x180D2F3F5  Resource<object>$$CastObject -> prefabResource.Object (the typed getter)
    	//   0x180D2F40A  mov rcx,[rsi+0x50] (_container); test; je NRE; GameObject$$get_transform
    	//   0x180D2F421  xorps xmm6 -> the AsyncToken argument is an all-zero struct (default), NOT asyncToken;
    	//                the package base does the same at 0x5D8794.
    	//   0x180D2F46E  Engine$$Instantiate<GameObject>(prefab, name: path (r8=[rdi+0x28]), layer: null (r9=0),
    	//                parent: _container.transform, token: default)
    	//   await; 0x180D2F543  new SpawnedObject(path, gameObject) (SpawnedObject$$.ctor)
    	//   0x180D2F585  _spawnedMap.set_Item(path, spawned) (slot-less direct call, +0x38); SetResult(spawned)
    	// Annotated disassembly: work/spawn-nre/disas/08_Spawn_path.txt, 09_Spawn_path_MoveNext.txt
    	private async UniTask<SpawnedObject> SpawnCore(string path, AsyncToken asyncToken)
    	{
    		if (IsSpawned(path)) throw new Error($"Object `{path}` is already spawned and can't be spawned again before it's destroyed.");

    		var resourcePath = SpawnConfiguration.ProcessInputPath(path, out _);
    		var prefabResource = await _loader.Load(resourcePath, this);
    		asyncToken.ThrowIfCanceled();
    		if (!prefabResource.Valid)
    			throw new Error($"Failed to spawn `{path}`: spawn resource `{resourcePath}` is not registered with any resource provider of the spawn loader (see MultipliableSpawn.SpawnCore).");

    		var gameObject = await Engine.Instantiate(prefabResource.Object, path, parent: _container.transform);
    		return _spawnedMap[path] = new SpawnedObject(path, gameObject);
    	}

    	[Token(Token = "0x600001C")]
    	[Address(RVA = "0xD2CCE0", Offset = "0xD2B6E0", VA = "0x180D2CCE0", Slot = "31")]
    	[AsyncStateMachine(typeof(_003CSpawn_003Ed__12))]
    	public UniTask<SpawnedObject> Spawn(string path, string subId, [Optional] AsyncToken asyncToken)
    	{
    		return SpawnCore(path, subId, asyncToken);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2CCE0 (kickoff) / 0xD2F7E0 (<Spawn>d__12.MoveNext, 2257 B)
    	// State -1, in order:
    	//   0x180D2F99A  subId (+0x20) null or Length == 0 -> 0x180D2FB24: virtual slot 26 on this
    	//                Spawn(path (+0x30), asyncToken (+0x38)); await; SetResult(result)
    	//                == `return await Spawn(path, asyncToken)`.
    	//   0x180D2F9C1  String$$Concat(path, STR '-', subId) -> <multipliablePath>5__2 (+0x48)
    	//   0x180D2F9EA  direct call MultipliableSpawn$$IsSpawned(path, subId) (0xD2D1D0); true -> 0x180D2FFB9:
    	//                throw Error("Object `" + multipliablePath + "` is already spawned ...")
    	//   0x180D2FA02  ProcessInputPath(path, out _)  -- the *un-suffixed* path selects the prefab
    	//   0x180D2FA1B  _loader slot 37 Load(resourcePath, this); await; ThrowIfCanceled(&asyncToken)
    	//   0x180D2FCB3  ObjectUtils$$IsValid(Resource.Object); false -> 0x180D30046: the same duplicated
    	//                "already spawned" throw with multipliablePath (DEVIATION as in SpawnCore: message only)
    	//   0x180D2FD58  Engine$$Instantiate<GameObject>(prefab, name: multipliablePath (r8=[rdi+0x48]),
    	//                layer: null, parent: _container.transform, token: default)
    	//   0x180D2FE4B  new SpawnedObject(multipliablePath, gameObject); 0x180D2FE74 _spawnedMap[multipliablePath] = it
    	//   0x180D2FEEC  multipliablePath local cleared (hoisted local, end of scope); SetResult
    	// Annotated disassembly: work/spawn-nre/disas/10_Spawn_path_subId.txt, 11_Spawn_path_subId_MoveNext.txt
    	private async UniTask<SpawnedObject> SpawnCore(string path, string subId, AsyncToken asyncToken)
    	{
    		if (string.IsNullOrEmpty(subId))
    			return await Spawn(path, asyncToken);

    		var multipliablePath = path + "-" + subId;
    		if (IsSpawned(path, subId)) throw new Error($"Object `{multipliablePath}` is already spawned and can't be spawned again before it's destroyed.");

    		var resourcePath = SpawnConfiguration.ProcessInputPath(path, out _);
    		var prefabResource = await _loader.Load(resourcePath, this);
    		asyncToken.ThrowIfCanceled();
    		if (!prefabResource.Valid)
    			throw new Error($"Failed to spawn `{multipliablePath}`: spawn resource `{resourcePath}` is not registered with any resource provider of the spawn loader (see MultipliableSpawn.SpawnCore).");

    		var gameObject = await Engine.Instantiate(prefabResource.Object, multipliablePath, parent: _container.transform);
    		return _spawnedMap[multipliablePath] = new SpawnedObject(multipliablePath, gameObject);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2CF90, 172 B
    	// April signature: `public override void DestroySpawned(string path)` (Slot 27). The April
    	// base method Naninovel.SpawnManager.DestroySpawned(string) (RVA 0x5D66D0, also 172 B) is
    	// instruction-for-instruction identical to this override except for the map offset (base
    	// +0x18 `spawnedMap`, here +0x38 `_spawnedMap`), which recovers the body:
    	//   virtual IsSpawned(path)  (vtable [r8+0x2F8], MethodInfo* [r8+0x300])
    	//       -> if (!IsSpawned(path)) return;
    	//   virtual GetSpawned(path) (vtable [r8+0x308], MethodInfo* [r8+0x310])
    	//       -> var spawned = GetSpawned(path);
    	//   this._spawnedMap (+0x38).Remove(path)
    	//   Naninovel.ObjectUtils.DestroyOrImmediate(spawned.GameObject); the argument is read from
    	//   [spawned+0x18] = SpawnedObject.<GameObject>k__BackingField, and edx=0 is the nulled
    	//   MethodInfo* of a one-argument static call.
    	// DEVIATION April -> installed 2025-09-22 package: the package's base signature is
    	// `DestroySpawned(string path, bool dispose = true)`, so `override` cannot bind to the April
    	// one-parameter spelling; the extra optional parameter is added here. The flag is honoured
    	// (as in the package's own base body) because Naninovel.Commands.DestroySpawned (the
    	// `@despawn` command) calls `DestroySpawned(Path, false)` and destroys the game object
    	// itself only after awaiting the object's IAwaitable.AwaitDestroy; ignoring the flag would
    	// destroy the object before that await completes. Every April call site used the default, so
    	// for `dispose: true` this is byte-for-byte the April behaviour.
    	// TODO(evidence): whether any game-side caller ever needed the `dispose: false` path
    	// (impossible in April: the April base had no such parameter).
    	[Token(Token = "0x600001D")]
    	[Address(RVA = "0xD2CF90", Offset = "0xD2B990", VA = "0x180D2CF90", Slot = "27")]
    	public override void DestroySpawned(string path, bool dispose = true)
    	{
    		if (!IsSpawned(path))
    		{
    			return;
    		}
    		SpawnedObject spawned = GetSpawned(path);
    		_spawnedMap.Remove(path);
    		if (dispose)
    		{
    			ObjectUtils.DestroyOrImmediate(spawned.GameObject);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2D040, 272 B
    	//   0x180D2D08A  subId null or Length == 0 -> 0x180D2D120: tail call virtual slot 27 DestroySpawned(path)
    	//   0x180D2D0A9  direct IsSpawned(path, subId) (0xD2D1D0); false -> return
    	//   0x180D2D0C3  direct GetSpawned(path, subId) (0xD2D320)
    	//   0x180D2D0DF  String$$Concat(path, STR '-', subId); 0x180D2D0F6 _spawnedMap.Remove(that)
    	//   0x180D2D106  ObjectUtils$$DestroyOrImmediate(spawned.GameObject (+0x18))
    	// Annotated disassembly: work/spawn-nre/disas/13_DestroySpawned_path_subId.txt
    	[Token(Token = "0x600001E")]
    	[Address(RVA = "0xD2D040", Offset = "0xD2BA40", VA = "0x180D2D040", Slot = "34")]
    	public void DestroySpawned(string path, string subId)
    	{
    		if (string.IsNullOrEmpty(subId))
    		{
    			DestroySpawned(path);
    			return;
    		}
    		if (!IsSpawned(path, subId))
    		{
    			return;
    		}
    		SpawnedObject spawned = GetSpawned(path, subId);
    		_spawnedMap.Remove(path + "-" + subId);
    		ObjectUtils.DestroyOrImmediate(spawned.GameObject);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2D150, 112 B
    	//   0x180D2D181  path null or Length == 0 -> return false
    	//   0x180D2D18C  mov rcx,[rdi+0x38] (_spawnedMap); tail call Dictionary.ContainsKey(path)
    	// Same as the package's SpawnManager.IsSpawned on this class' own map.
    	// Annotated disassembly: work/spawn-nre/disas/14_IsSpawned_path.txt
    	[Token(Token = "0x600001F")]
    	[Address(RVA = "0xD2D150", Offset = "0xD2BB50", VA = "0x180D2D150", Slot = "28")]
    	public override bool IsSpawned(string path)
    	{
    		if (string.IsNullOrEmpty(path)) return false;
    		return _spawnedMap.ContainsKey(path);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2D1D0, 224 B
    	//   0x180D2D21A  subId null or Length == 0 -> 0x180D2D281: tail call virtual slot 28 IsSpawned(path)
    	//   0x180D2D225  path null or Length == 0  -> return false
    	//   0x180D2D244  String$$Concat(path, STR '-', subId); tail call _spawnedMap.ContainsKey(that)
    	// Annotated disassembly: work/spawn-nre/disas/15_IsSpawned_path_subId.txt
    	[Token(Token = "0x6000020")]
    	[Address(RVA = "0xD2D1D0", Offset = "0xD2BBD0", VA = "0x180D2D1D0", Slot = "32")]
    	public bool IsSpawned(string path, string subId)
    	{
    		if (string.IsNullOrEmpty(subId)) return IsSpawned(path);
    		if (string.IsNullOrEmpty(path)) return false;
    		return _spawnedMap.ContainsKey(path + "-" + subId);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2D2B0, 99 B
    	//   0x180D2D2E1  mov rcx,[rbx+0x38] (_spawnedMap); Dictionary.get_Item(path)
    	// Same as the package's SpawnManager.GetSpawned on this class' own map.
    	// Annotated disassembly: work/spawn-nre/disas/16_GetSpawned_path.txt
    	[Token(Token = "0x6000021")]
    	[Address(RVA = "0xD2D2B0", Offset = "0xD2BCB0", VA = "0x180D2D2B0", Slot = "29")]
    	public override SpawnedObject GetSpawned(string path)
    	{
    		return _spawnedMap[path];
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2D320, 208 B
    	//   0x180D2D36A  subId null or Length == 0 -> 0x180D2D3BF: tail call virtual slot 29 GetSpawned(path)
    	//   0x180D2D389  String$$Concat(path, STR '-', subId); _spawnedMap.get_Item(that)
    	// Annotated disassembly: work/spawn-nre/disas/17_GetSpawned_path_subId.txt
    	[Token(Token = "0x6000022")]
    	[Address(RVA = "0xD2D320", Offset = "0xD2BD20", VA = "0x180D2D320", Slot = "33")]
    	public SpawnedObject GetSpawned(string path, string subId)
    	{
    		if (string.IsNullOrEmpty(subId)) return GetSpawned(path);
    		return _spawnedMap[path + "-" + subId];
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2D3F0, 741 B
    	//   0x180D2D4B1  new SpawnManager.GameState (TYPE Naninovel.SpawnManager.GameState_TypeInfo)
    	//   0x180D2D4CD  _spawnedMap (+0x38).get_Values
    	//   0x180D2D4F4..0x180D2D5C9  cached static lambda `<>c.<>9__19_0` (Func<SpawnedObject, SpawnedObjectState>);
    	//                the METHODINFO alias line names the lambda body as folded with
    	//                Naninovel.SpawnManager.<>c.<SaveServiceState>b__14_0, i.e. `o => new SpawnedObjectState(o)`.
    	//   0x180D2D5D6  Enumerable.Select<SpawnedObject, SpawnedObjectState>(values, lambda)
    	//   0x180D2D62F  ToList (inlined as `new List<SpawnedObjectState>(IEnumerable)`)
    	//   0x180D2D640  mov [rsi+0x10],rdi  -> GameState.SpawnedObjects (+0x10)
    	//   0x180D2D6AB  tail call StateMap.SetState<SpawnManager.GameState>(state, instanceId: null)
    	// Same as the package's SpawnManager.SaveServiceState on this class' own map.
    	// Annotated disassembly: work/spawn-nre/disas/18_SaveServiceState.txt
    	[Token(Token = "0x6000023")]
    	[Address(RVA = "0xD2D3F0", Offset = "0xD2BDF0", VA = "0x180D2D3F0", Slot = "22")]
    	public override void SaveServiceState(GameStateMap stateMap)
    	{
    		var state = new GameState {
    			SpawnedObjects = _spawnedMap.Values
    				.Select(o => new SpawnedObjectState(o)).ToList()
    		};
    		stateMap.SetState(state);
    	}

    	[Token(Token = "0x6000024")]
    	[Address(RVA = "0xD2D6E0", Offset = "0xD2C0E0", VA = "0x180D2D6E0", Slot = "23")]
    	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__20))]
    	public override UniTask LoadServiceState(GameStateMap stateMap)
    	{
    		return LoadServiceStateCore(stateMap);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2D6E0 (kickoff) / 0xD2EB70 (<LoadServiceState>d__20.MoveNext, 1361 B)
    	//   0x180D2EC75  StateMap.GetState<SpawnManager.GameState>(instanceId: null)
    	//   0x180D2EC86  state?.SpawnedObjects?.Count (Nullable<int> ctor/HasValue/GetValueOrDefault) > 0
    	//                -> 0x180D2EF0F Start<LoadObjectsAsync>d__21 with state; await it (the only await)
    	//   0x180D2F054  else: _spawnedMap (+0x38) Count (count(+0x20) - freeCount(+0x28)) > 0
    	//                -> virtual slot 30 DestroyAllSpawned()
    	// Same as the package's SpawnManager.LoadServiceState, with the local function hoisted to
    	// LoadObjectsAsync (dump.cs:735291 names it as a private method).
    	// Annotated disassembly: work/spawn-nre/disas/20_LoadServiceState_MoveNext.txt
    	private async UniTask LoadServiceStateCore(GameStateMap stateMap)
    	{
    		var state = stateMap.GetState<GameState>();
    		if (state?.SpawnedObjects?.Count > 0) await LoadObjectsAsync(state);
    		else if (_spawnedMap.Count > 0) DestroyAllSpawned();
    	}

    	[Token(Token = "0x6000025")]
    	[Address(RVA = "0xD2D860", Offset = "0xD2C260", VA = "0x180D2D860")]
    	[AsyncStateMachine(typeof(_003CLoadObjectsAsync_003Ed__21))]
    	private UniTask LoadObjectsAsync(GameState state)
    	{
    		return LoadObjectsAsyncCore(state);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2D860 (kickoff) / 0xD2E270 (<LoadObjectsAsync>d__21.MoveNext, 2299 B)
    	//   0x180D2E47C  new List<UniTask>(capacity: state.SpawnedObjects.Count) -- a plain List, not ListPool
    	//   0x180D2E4A3  _spawnedMap.get_Values; 0x180D2E4FC new List<SpawnedObject>(values) == .ToList()  -> toDestroy
    	//   0x180D2E5B0  foreach objState in state.SpawnedObjects (List<SpawnedObjectState>.Enumerator):
    	//     0x180D2E604  virtual slot 28 IsSpawned(objState.Path (+0x0 of the struct copy))
    	//     true  -> 0x180D2E67E direct UpdateObject(objState, toDestroy) (0xD2D9E0)
    	//     false -> 0x180D2E65A direct SpawnObjectAsync(objState) (0xD2DC30); 0x180D2E66E tasks.Add (ICollection<UniTask>.Add thunk 0x80050)
    	//   0x180D2E6B2  foreach obj in toDestroy (IEnumerable<SpawnedObject>): virtual slot 27 DestroySpawned(obj.Path (+0x10))
    	//   0x180D2E8AF  UniTask$$WhenAll(tasks); await
    	// Annotated disassembly: work/spawn-nre/disas/22_LoadObjectsAsync_MoveNext.txt
    	private async UniTask LoadObjectsAsyncCore(GameState state)
    	{
    		var tasks = new List<UniTask>(state.SpawnedObjects.Count);
    		var toDestroy = _spawnedMap.Values.ToList();
    		foreach (var objState in state.SpawnedObjects)
    			if (IsSpawned(objState.Path)) UpdateObject(objState, toDestroy);
    			else tasks.Add(SpawnObjectAsync(objState));
    		foreach (var obj in toDestroy)
    			DestroySpawned(obj.Path);
    		await UniTask.WhenAll(tasks);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2D9E0, 592 B
    	//   0x180D2DAAC  objState.Path.Split('-') (String$$SplitInternal with a 1-char ReadOnlySpan '-', count int.MaxValue, options None)
    	//   0x180D2DAC0  parts.Length == 1 -> 0x180D2DAD7 virtual slot 29 GetSpawned(parts[0])
    	//   0x180D2DAE9  parts.Length == 2 -> 0x180D2DB11 direct GetSpawned(parts[0], parts[1]) (0xD2D320)
    	//   else          -> 0x180D2DBDF Debug.LogError(STR "スポーンパスに '-' が 2 つ以上含まれています：" + objState.Path); return
    	//   0x180D2DB6F  ICollection<SpawnedObject>.Remove(spawned) on toDestroy (interface slot 6)
    	//   0x180D2DB7B  SpawnedObjectState$$ApplyTo(&objState, spawned)
    	//   0x180D2DB9D  spawned.AwaitSpawn(default).Forget()  (UniTaskExtensions$$ForgetCore)
    	// The JP literal (0x2E2FD48, UTF-8 in stringliteral.json) reads: 「スポーンパスに '-' が 2 つ以上含まれています：」
    	// ("the spawn path contains two or more '-':").
    	// Annotated disassembly: work/spawn-nre/disas/23_UpdateObject.txt
    	[Token(Token = "0x6000026")]
    	[Address(RVA = "0xD2D9E0", Offset = "0xD2C3E0", VA = "0x180D2D9E0")]
    	private void UpdateObject(SpawnedObjectState objState, IList<SpawnedObject> toDestroy)
    	{
    		string[] parts = objState.Path.Split('-');
    		SpawnedObject spawned;
    		if (parts.Length == 1)
    		{
    			spawned = GetSpawned(parts[0]);
    		}
    		else if (parts.Length == 2)
    		{
    			spawned = GetSpawned(parts[0], parts[1]);
    		}
    		else
    		{
    			UnityEngine.Debug.LogError("スポーンパスに '-' が 2 つ以上含まれています：" + objState.Path);
    			return;
    		}
    		toDestroy.Remove(spawned);
    		objState.ApplyTo(spawned);
    		spawned.AwaitSpawn().Forget();
    	}

    	[Token(Token = "0x6000027")]
    	[Address(RVA = "0xD2DC30", Offset = "0xD2C630", VA = "0x180D2DC30")]
    	[AsyncStateMachine(typeof(_003CSpawnObjectAsync_003Ed__23))]
    	private UniTask SpawnObjectAsync(SpawnedObjectState objState)
    	{
    		return SpawnObjectAsyncCore(objState);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2DC30 (kickoff) / 0xD300F0 (<SpawnObjectAsync>d__23.MoveNext, 1148 B)
    	//   0x180D301E2  objState.Path (+0x18 of the state machine = the struct's first field).Split('-') (String$$Split(char, int, options))
    	//   Length == 1 -> 0x180D3023E virtual slot 26 Spawn(parts[0], token: default (zeroed 16 B at [rsp+0x50]))
    	//   Length == 2 -> 0x180D30384 direct Spawn(parts[0], parts[1], token: default) (0xD2CCE0)
    	//   else        -> 0x180D304C8 Debug.LogError(same JP literal + objState.Path); return
    	//   await; 0x180D30453 SpawnedObjectState$$ApplyTo(&objState, spawned); 0x180D30475 spawned.AwaitSpawn().Forget()
    	// Annotated disassembly: work/spawn-nre/disas/25_SpawnObjectAsync_MoveNext.txt
    	private async UniTask SpawnObjectAsyncCore(SpawnedObjectState objState)
    	{
    		string[] parts = objState.Path.Split('-');
    		SpawnedObject spawned;
    		if (parts.Length == 1)
    		{
    			spawned = await Spawn(parts[0]);
    		}
    		else if (parts.Length == 2)
    		{
    			spawned = await Spawn(parts[0], parts[1]);
    		}
    		else
    		{
    			UnityEngine.Debug.LogError("スポーンパスに '-' が 2 つ以上含まれています：" + objState.Path);
    			return;
    		}
    		objState.ApplyTo(spawned);
    		spawned.AwaitSpawn().Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2DDF0, 360 B
    	//   0x180D2DE87  _spawnedMap (+0x38).get_Values; ValueCollection.Enumerator loop:
    	//   0x180D2DF13    ObjectUtils$$DestroyOrImmediate(spawnedObj.GameObject (+0x18))
    	//   0x180D2DF3B  _spawnedMap.Clear()
    	// Same as the package's SpawnManager.DestroyAllSpawned on this class' own map.
    	// Annotated disassembly: work/spawn-nre/disas/26_DestroyAllSpawned.txt
    	[Token(Token = "0x6000028")]
    	[Address(RVA = "0xD2DDF0", Offset = "0xD2C7F0", VA = "0x180D2DDF0", Slot = "30")]
    	protected override void DestroyAllSpawned()
    	{
    		foreach (var spawnedObj in _spawnedMap.Values)
    			ObjectUtils.DestroyOrImmediate(spawnedObj.GameObject);
    		_spawnedMap.Clear();
    	}
    }
}
