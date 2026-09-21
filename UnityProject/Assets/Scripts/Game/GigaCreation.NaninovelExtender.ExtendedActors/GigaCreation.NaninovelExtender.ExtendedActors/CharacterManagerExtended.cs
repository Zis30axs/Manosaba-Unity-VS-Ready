using GigaCreation.NaninovelExtender.Common;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x200000E")]
    [UsedImplicitly]
    [InitializeAtRuntime(0, typeof(CharacterManager))]
    // Interface list as shipped (dump.cs TypeDefIndex 16396); the generated source had dropped all three.
    public class CharacterManagerExtended : CharacterManager, ICharacterManagerExtended, IMultipliableCharacterManager, IMultipliableActorManager<ICharacterActor>
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000011")]
    	[CompilerGenerated]
    	private struct _003CAddActor_003Ed__10
    {
    		[Token(Token = "0x4000031")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000032")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<ICharacterActor> _003C_003Et__builder;

    		[Token(Token = "0x4000033")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CharacterManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000034")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string actorId;

    		[Token(Token = "0x4000035")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string subId;

    		[Token(Token = "0x4000036")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<ICharacterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000033")]
    		[Address(RVA = "0xD15E40", Offset = "0xD14840", VA = "0x180D15E40", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000034")]
    		[Address(RVA = "0xD16490", Offset = "0xD14E90", VA = "0x180D16490", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000012")]
    	[CompilerGenerated]
    	private struct _003CConstructActor_003Ed__15
    {
    		[Token(Token = "0x4000037")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000038")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<ICharacterActor> _003C_003Et__builder;

    		[Token(Token = "0x4000039")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CharacterManagerExtended _003C_003E4__this;

    		[Token(Token = "0x400003A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string actorId;

    		[Token(Token = "0x400003B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string subId;

    		[Token(Token = "0x400003C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private CharacterMetadata _003Cmeta_003E5__2;

    		[Token(Token = "0x400003D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private ICharacterActor _003Cactor_003E5__3;

    		[Token(Token = "0x400003E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000035")]
    		[Address(RVA = "0xD164C0", Offset = "0xD14EC0", VA = "0x180D164C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000036")]
    		[Address(RVA = "0xD174A0", Offset = "0xD15EA0", VA = "0x180D174A0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000013")]
    	[CompilerGenerated]
    	private struct _003CGetOrAddActor_003Ed__13
    {
    		[Token(Token = "0x400003F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000040")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<ICharacterActor> _003C_003Et__builder;

    		[Token(Token = "0x4000041")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string actorId;

    		[Token(Token = "0x4000042")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CharacterManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000043")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<ICharacterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000037")]
    		[Address(RVA = "0xD174D0", Offset = "0xD15ED0", VA = "0x180D174D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000038")]
    		[Address(RVA = "0xD17930", Offset = "0xD16330", VA = "0x180D17930", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000014")]
    	[CompilerGenerated]
    	private struct _003CGetOrAddActor_003Ed__14
    {
    		[Token(Token = "0x4000044")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000045")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<ICharacterActor> _003C_003Et__builder;

    		[Token(Token = "0x4000046")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CharacterManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000047")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string actorId;

    		[Token(Token = "0x4000048")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string subId;

    		[Token(Token = "0x4000049")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<ICharacterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000039")]
    		[Address(RVA = "0xD17960", Offset = "0xD16360", VA = "0x180D17960", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600003A")]
    		[Address(RVA = "0xD17C70", Offset = "0xD16670", VA = "0x180D17C70", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000015")]
    	[CompilerGenerated]
    	private struct _003CLoadServiceState_003Ed__12
    {
    		[Token(Token = "0x400004A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400004B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400004C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GameStateMap stateMap;

    		[Token(Token = "0x400004D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CharacterManagerExtended _003C_003E4__this;

    		[Token(Token = "0x400004E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private IEnumerator<KeyValuePair<string, CharacterStateExtended>> _003C_003E7__wrap1;

    		[Token(Token = "0x400004F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CharacterStateExtended _003CextendedState_003E5__3;

    		[Token(Token = "0x4000050")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<ICharacterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000051")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600003B")]
    		[Address(RVA = "0xD17CA0", Offset = "0xD166A0", VA = "0x180D17CA0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600003C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400002A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	private readonly MultipliableActorManagerUtility<ICharacterActor> _utility;

    	[Token(Token = "0x400002B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	private readonly Dictionary<string, float> _baseScales;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x468E80, 8 B (folded)
    	// Get-only auto-property: the trivial `[CompilerGenerated]` read of the backing field
    	// `<Configuration>k__BackingField` at +0x80 (`mov rax,[rcx+0x80]; ret`, shared with dozens of
    	// unrelated trivial getters). `get;` instead of the stub `return null;` is required so the
    	// recovered constructor can assign the backing field (and so the IEngineService<TConfig>
    	// implementation stops handing out null).
    	[Token(Token = "0x17000001")]
    	public new CharactersConfigurationExtended Configuration
    	{
    		[Token(Token = "0x6000020")]
    		[Address(RVA = "0x468E80", Offset = "0x467880", VA = "0x180468E80", Slot = "72")]
    		[CompilerGenerated]
    		get;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x478210, 5 B
    	// A manual (non-auto) property: the getter is the trivial field read
    	// `mov rax,[rcx+0x78]; ret` of `_baseScales` (+0x78, dump.cs), which is why dump.cs lists no
    	// `<ScaleCoefficients>k__BackingField`. The stub `return null;` is replaced by that read; the
    	// dictionary itself is built by the recovered constructor (`configExtended.Options.ToDictionary(...)`).
    	[Token(Token = "0x17000002")]
    	public IReadOnlyDictionary<string, float> ScaleCoefficients
    	{
    		[Token(Token = "0x6000021")]
    		[Address(RVA = "0x478210", Offset = "0x476C10", VA = "0x180478210", Slot = "68")]
    		get
    		{
    			return _baseScales;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD14870, 1383 B
    	// ABI: rcx=__this, rdx=configExtended, r8=config, r9=cameraConfig, 5th (docs) = [rsp+0x70],
    	// 6th (printers) = [rsp+0x78] (after `push rdi/r14/r15` + `sub rsp,0x30`).
    	// 1. rdi=__this, r14=configExtended, rbp=config, rsi=cameraConfig.
    	// 2. +0x60 = new Naninovel.SerializableLiteralStringMap() -- this is a field initializer of the BASE
    	//    `CharacterManager.avatarPathByCharId` (+0x60, dump.cs), emitted before the base call because
    	//    the base constructor below is inlined.
    	// 3. `call 0x18089d280` -> OrthoActorManager<ICharacterActor, CharacterState, CharacterMetadata,
    	//    CharactersConfiguration>$$.ctor(config, cameraConfig) = the inlined `CharacterManager(config,
    	//    cameraConfig, docs, printers)` (dump.cs RVA 0x44DF20), whose own base call this is.
    	//    == base(config, cameraConfig, docs, printers).
    	// 4. the remainder of the inlined base body: +0x50 docs ([rsp+0x70]), +0x58 printers ([rsp+0x78]).
    	// 5. this class's own body, in this order:
    	//    +0x80 <Configuration>k__BackingField = configExtended (r14);
    	//    +0x70 _utility = new MultipliableActorManagerUtility<ICharacterActor>();
    	//    +0x78 _baseScales = Enumerable.ToDictionary<CharactersConfigurationExtended.Option, string, float>(
    	//      configExtended.Options (+0x18, loaded into rcx), keySelector: <>c.<.ctor>b__7_0
    	//      (returns string, RVA 0x326120, folded), elementSelector: <>c.<.ctor>b__7_1
    	//      (returns float, RVA 0xD15DA0, `movss xmm0,[rdx+0x18]`).
    	[Token(Token = "0x6000022")]
    	[Address(RVA = "0xD14870", Offset = "0xD13270", VA = "0x180D14870")]
    	public CharacterManagerExtended(CharactersConfigurationExtended configExtended, CharactersConfiguration config, CameraConfiguration cameraConfig, ITextManager docs, ITextPrinterManager printers)
    		: base(config, cameraConfig, docs, printers)
    	{
    		Configuration = configExtended;
    		_utility = new MultipliableActorManagerUtility<ICharacterActor>();
    		_baseScales = configExtended.Options.ToDictionary(option => option.Character, option => option.Scale);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD10F00, 133 B (folded with BackgroundManagerExtended.ActorExists: the same body)
    	// Inlined IMultipliableActor.BuildId(actorId, subId) (`subId` non-empty -> String.Concat(actorId, "-", subId)), then a tail
    	// call through vtable offset 0x348 = slot 33 = ActorManager.ActorExists(string).
    	// Annotated disassembly: work/title-path/disas/cme_01_ActorExists.txt
    	[Token(Token = "0x6000023")]
    	[Address(RVA = "0xD10F00", Offset = "0xD0F900", VA = "0x180D10F00", Slot = "69")]
    	public bool ActorExists(string actorId, string subId)
    	{
    		return ActorExists(IMultipliableActor.BuildId(actorId, subId));
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD10F90, 133 B (folded with BackgroundManagerExtended.GetActor)
    	// Same shape as ActorExists: BuildId, then vtable offset 0x378 = slot 36 = ActorManager.GetActor(string).
    	// Annotated disassembly: work/title-path/disas/cme_02_GetActor.txt
    	[Token(Token = "0x6000024")]
    	[Address(RVA = "0xD10F90", Offset = "0xD0F990", VA = "0x180D10F90", Slot = "70")]
    	public ICharacterActor GetActor(string actorId, string subId)
    	{
    		return GetActor(IMultipliableActor.BuildId(actorId, subId));
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD14DE0 (kickoff) / 0xD15E40 (<AddActor>d__10.MoveNext, 1610 B)
    	// The body lives in AddActorCore (XxxCore convention; the <AddActor>d__10 metadata stays).  The stub returned default(UniTask<T>),
    	// so every @char resolved to a null actor.  Instruction-for-instruction the twin of BackgroundManagerExtended.AddActor (the
    	// normalized call sequences differ only in the private construct method, ConstructActor here):
    	//   id = BuildId(actorId, subId); ActorExists(id) (slot 33) -> Engine.Warn("Actor '" + id + "' was requested to be added, but it
    	//   already exists.") and GetActor(actorId, subId); _utility.PendingAddActorTaskExists -> await
    	//   _utility.WaitForCompletionOfPendingAddActorAsync; else CreatePendingAddActorTask, actor = await ConstructActor(actorId, subId),
    	//   ManagedActors.Add(id, actor), _utility.SetResultAndRemovePendingAddActorTask(actorId, subId, actor)
    	// Annotated disassembly: work/title-path/disas/cme_03_AddActor, cme_04_AddActor_MoveNext.txt
    	[Token(Token = "0x6000025")]
    	[Address(RVA = "0xD14DE0", Offset = "0xD137E0", VA = "0x180D14DE0", Slot = "71")]
    	[AsyncStateMachine(typeof(_003CAddActor_003Ed__10))]
    	public UniTask<ICharacterActor> AddActor(string actorId, string subId)
    	{
    		return AddActorCore(actorId, subId);
    	}

    	private async UniTask<ICharacterActor> AddActorCore(string actorId, string subId)
    	{
    		string id = IMultipliableActor.BuildId(actorId, subId);
    		if (ActorExists(id))
    		{
    			Engine.Warn("Actor '" + IMultipliableActor.BuildId(actorId, subId) + "' was requested to be added, but it already exists.");
    			return GetActor(actorId, subId);
    		}
    		if (_utility.PendingAddActorTaskExists(actorId, subId))
    		{
    			return await _utility.WaitForCompletionOfPendingAddActorAsync(actorId, subId);
    		}
    		_utility.CreatePendingAddActorTask(actorId, subId);
    		ICharacterActor actor = await ConstructActor(actorId, subId);
    		ManagedActors.Add(IMultipliableActor.BuildId(actorId, subId), actor);
    		_utility.SetResultAndRemovePendingAddActorTask(actorId, subId, actor);
    		return actor;
    	}

    	[Token(Token = "0x6000026")]
    	[Address(RVA = "0xD15040", Offset = "0xD13A40", VA = "0x180D15040", Slot = "31")]
    	public override void SaveServiceState(GameStateMap stateMap)
    	{
    		base.SaveServiceState(stateMap);
    	}

    	[Token(Token = "0x6000027")]
    	[Address(RVA = "0xD15410", Offset = "0xD13E10", VA = "0x180D15410", Slot = "32")]
    	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__12))]
    	public override UniTask LoadServiceState(GameStateMap stateMap)
    	{
    		return base.LoadServiceState(stateMap);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD155A0 (kickoff) / 0xD174D0 (<GetOrAddActor>d__13.MoveNext, 1105 B)
    	// The id may carry the sub-id ("Hiro-Sub", the key AddActor registers under); the twin of BackgroundManagerExtended.GetOrAddActor:
    	//   0x180D175D3  parts = actorId.Split('-'); Length 1 -> 0x180D1762D await base.GetOrAddActor(actorId) (ActorManager.GetOrAddActor);
    	//   Length 2 -> 0x180D17769 await GetOrAddActor(parts[0], parts[1]); otherwise Debug.LogError(STR + actorId) (0x180D17893), null
    	// Annotated disassembly: work/title-path/disas/cme_05_GetOrAddActor_id, cme_06_GetOrAddActor_id_MoveNext.txt
    	[Token(Token = "0x6000028")]
    	[Address(RVA = "0xD155A0", Offset = "0xD13FA0", VA = "0x180D155A0", Slot = "37")]
    	[AsyncStateMachine(typeof(_003CGetOrAddActor_003Ed__13))]
    	public override UniTask<ICharacterActor> GetOrAddActor(string actorId)
    	{
    		return GetOrAddActorCore(actorId);
    	}

    	private async UniTask<ICharacterActor> GetOrAddActorCore(string actorId)
    	{
    		string[] parts = actorId.Split('-');
    		if (parts.Length == 1)
    		{
    			return await base.GetOrAddActor(actorId);
    		}
    		if (parts.Length == 2)
    		{
    			return await GetOrAddActor(parts[0], parts[1]);
    		}
    		UnityEngine.Debug.LogError("アクター ID に '-' が 2 つ以上含まれています：" + actorId);
    		return null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD15790 (kickoff) / 0xD17960 (<GetOrAddActor>d__14.MoveNext, 774 B)
    	// ActorExists(BuildId(actorId, subId)) -> GetActor(actorId, subId) as the result, otherwise await AddActor(actorId, subId)
    	// (normalized call sequence identical to BackgroundManagerExtended's).
    	// Annotated disassembly: work/title-path/disas/cme_07_GetOrAddActor_subId, cme_08_GetOrAddActor_subId_MoveNext.txt
    	[Token(Token = "0x6000029")]
    	[Address(RVA = "0xD15790", Offset = "0xD14190", VA = "0x180D15790")]
    	[AsyncStateMachine(typeof(_003CGetOrAddActor_003Ed__14))]
    	private UniTask<ICharacterActor> GetOrAddActor(string actorId, string subId)
    	{
    		return GetOrAddActorCore(actorId, subId);
    	}

    	private async UniTask<ICharacterActor> GetOrAddActorCore(string actorId, string subId)
    	{
    		return ActorExists(actorId, subId) ? GetActor(actorId, subId) : await AddActor(actorId, subId);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD159F0 (kickoff) / 0xD164C0 (<ConstructActor>d__15.MoveNext, 4049 B)
    	// Three package methods inlined with the sub-id threaded into the actor constructor (the twin of
    	// BackgroundManagerExtended.ConstructActorAsync, plus the try/catch of ActorManager.ConstructActor and the placement of
    	// CharacterManager.ConstructActor):
    	//   0x180D1665B  meta = base Configuration (+0x20, NOT the shadowing CharactersConfigurationExtended).GetMetadataOrDefault(actorId)
    	//   0x180D166D5  implType = GetImplementationType(meta) (vtable 0x3D8 = slot 42); 0x180D166EB loader = GetAppearanceLoader(actorId)
    	//   subId non-empty && typeof(IMultipliableActor).IsAssignableFrom(implType) (Type slot 0x2B8, 0x180D16750):
    	//     args = { actorId, subId, meta, loader } (loader != null) or { actorId, subId, meta }; otherwise { actorId, meta, loader } / { actorId, meta }
    	//   0x180D16856 / 0x180D1694E / 0x180D16A4D / 0x180D16B14  Activator.CreateInstance(implType, 0x214 = CreateInstance | Public | Instance,
    	//     null, args, null) cast to ICharacterActor; a failure is rethrown at 0x180D17461 as System.Exception(String.Concat(
    	//     "Failed to create instance of `", implType.FullName, "` actor. Make sure the implementation has a compatible constructor."), e)
    	//   0x180D16BA4  await actor.Initialize() (IActor interface slot 13, state 0)
    	//   0x180D16D8E  await new CharacterState().ApplyToActor(actor) (vtable 0x188 = slot 5, state 1)
    	//   0x180D16FBE  z = TopmostZPosition (vtable 0x408 = slot 45); SpecialSubId.TryGetModifiedPositionZ(z, subId, out z2) ? z2 :
    	//     TopmostZPosition again; actor.ChangePositionZ(z) (ActorExtensions 0x180448540)
    	//   0x180D17008  actor.Position = new Vector3(GlobalSceneOrigin.x, GlobalSceneOrigin.y, actor.Position.z) (GlobalSceneOrigin vtable 0x3E8 =
    	//     slot 43, read twice; IActor slots 5 get_Position / 6 set_Position)
    	//   0x180D1710F  meta.HighlightWhenSpeaking (+0xA4) -> ApplyPose(actor, meta.NotSpeakingPose (+0xB8), new Tween(0)) (vtable 0x568 =
    	//     slot 67; the Tween struct is {0f, default easing, Scale = true, Complete = true} = 0x101 at 0x180D17118)
    	// Annotated disassembly: work/title-path/disas/cme_09_ConstructActor, cme_10_ConstructActor_MoveNext.txt
    	[Token(Token = "0x600002A")]
    	[Address(RVA = "0xD159F0", Offset = "0xD143F0", VA = "0x180D159F0")]
    	[AsyncStateMachine(typeof(_003CConstructActor_003Ed__15))]
    	private UniTask<ICharacterActor> ConstructActor(string actorId, string subId)
    	{
    		return ConstructActorCore(actorId, subId);
    	}

    	private async UniTask<ICharacterActor> ConstructActorCore(string actorId, string subId)
    	{
    		CharacterMetadata meta = base.Configuration.GetMetadataOrDefault(actorId);
    		Type implType = GetImplementationType(meta);
    		IResourceLoader loader = GetAppearanceLoader(actorId);
    		object[] args;
    		if (!string.IsNullOrEmpty(subId) && typeof(IMultipliableActor).IsAssignableFrom(implType))
    		{
    			args = loader != null ? new object[] { actorId, subId, meta, loader } : new object[] { actorId, subId, meta };
    		}
    		else
    		{
    			args = loader != null ? new object[] { actorId, meta, loader } : new object[] { actorId, meta };
    		}
    		ICharacterActor actor;
    		try
    		{
    			actor = (ICharacterActor)Activator.CreateInstance(implType, BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance, null, args, null);
    		}
    		catch (Exception e)
    		{
    			throw new Exception("Failed to create instance of `" + implType.FullName + "` actor. Make sure the implementation has a compatible constructor.", e);
    		}
    		await actor.Initialize();
    		await new CharacterState().ApplyToActor(actor);
    		float z = SpecialSubId.TryGetModifiedPositionZ(TopmostZPosition, subId, out float modifiedZ) ? modifiedZ : TopmostZPosition;
    		actor.ChangePositionZ(z);
    		actor.Position = new UnityEngine.Vector3(GlobalSceneOrigin.x, GlobalSceneOrigin.y, actor.Position.z);
    		if (meta.HighlightWhenSpeaking)
    		{
    			ApplyPose(actor, meta.NotSpeakingPose, new Tween(0f));
    		}
    		return actor;
    	}
    }
}
