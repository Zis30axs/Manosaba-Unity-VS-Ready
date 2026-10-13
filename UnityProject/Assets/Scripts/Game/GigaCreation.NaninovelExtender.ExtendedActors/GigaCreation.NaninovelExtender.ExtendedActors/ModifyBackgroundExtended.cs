using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastEnumUtility;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x200003D")]
    [UsedImplicitly]
    [CommandAlias("back")]
    public class ModifyBackgroundExtended : ModifyBackground
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200003F")]
    	[CompilerGenerated]
    	private struct _003CApplyAppearanceModification_003Ed__18
    {
    		[Token(Token = "0x40000DA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000DB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000DC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ModifyBackgroundExtended _003C_003E4__this;

    		[Token(Token = "0x40000DD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40000DE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public IBackgroundActor actor;

    		[Token(Token = "0x40000DF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public Tween tween;

    		[Token(Token = "0x40000E0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<Texture2D>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40000E1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60000D9")]
    		[Address(RVA = "0xD21330", Offset = "0xD1FD30", VA = "0x180D21330", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000DA")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000040")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__16
    {
    		[Token(Token = "0x40000E2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000E3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000E4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ModifyBackgroundExtended _003C_003E4__this;

    		[Token(Token = "0x40000E5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40000E6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60000DB")]
    		[Address(RVA = "0xD21B00", Offset = "0xD20500", VA = "0x180D21B00", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000DC")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000041")]
    	[CompilerGenerated]
    	private struct _003CModify_003Ed__17
    {
    		[Token(Token = "0x40000E7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000E8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000E9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ModifyBackgroundExtended _003C_003E4__this;

    		[Token(Token = "0x40000EA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40000EB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private EasingType _003CeasingType_003E5__2;

    		[Token(Token = "0x40000EC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private PooledObject<List<UniTask>> _003C__003E5__3;

    		[Token(Token = "0x40000ED")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40000EE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask<IBackgroundActor>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60000DD")]
    		[Address(RVA = "0xD22230", Offset = "0xD20C30", VA = "0x180D22230", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000DE")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000042")]
    	[CompilerGenerated]
    	private struct _003CPreloadResources_003Ed__14
    {
    		[Token(Token = "0x40000EF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000F0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000F1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ModifyBackgroundExtended _003C_003E4__this;

    		[Token(Token = "0x40000F2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask<Texture2D>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40000F3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60000DF")]
    		[Address(RVA = "0xD23130", Offset = "0xD21B30", VA = "0x180D23130", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000E0")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40000D2")]
    	private const bool DefaultMatchScreenWidth = true;

    	[Token(Token = "0x40000D3")]
    	private const string StillId = "Stills";

    	[Token(Token = "0x40000D4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
    	[UsedImplicitly]
    	public StringParameter SubId;

    	[Token(Token = "0x40000D5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
    	[UsedImplicitly]
    	[ParameterAlias("match")]
    	public BooleanParameter MatchScreenWidth;

    	[Token(Token = "0x40000D6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
    	private ModifyActorUtility<IBackgroundActor, IMultipliableBackgroundManager> _actorUtility;

    	[Token(Token = "0x40000D7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	private Texture2D _preloadedDissolveTexture;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD204C0, 7 B
    	// `xor edx, edx; jmp AttemptScenePosition` -- the override is a forwarder to the private method below, which
    	// carries the whole "scene position, else pose position, else the base value" logic (ModifyOrthoActor's own
    	// AssignedPosition is the same forwarder to its private AttemptScenePosition).
    	// Annotated disassembly: work/title-path/disas/mbe_02_get_AssignedPosition.txt
    	[Token(Token = "0x17000014")]
    	protected override float?[] AssignedPosition
    	{
    		[Token(Token = "0x60000CA")]
    		[Address(RVA = "0xD204C0", Offset = "0xD1EEC0", VA = "0x180D204C0", Slot = "22")]
    		get
    		{
    			return AttemptScenePosition();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD204D0, 133 B
    	// `SubId` (+0xD0): null -> null, !HasValue -> null, else Value (Nullable<string>.get_Value): the body of
    	// StringParameter.op_Implicit(StringParameter), i.e. `=> SubId;` (same shape as SpawnExtended.AssignedSubId).
    	[Token(Token = "0x17000015")]
    	private string AssignedSubId
    	{
    		[Token(Token = "0x60000CB")]
    		[Address(RVA = "0xD204D0", Offset = "0xD1EED0", VA = "0x180D204D0")]
    		get
    		{
    			return SubId;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD20560, 175 B
    	// `MatchScreenWidth` (+0xD8): non-null and ICommandParameter.HasValue -> Nullable<bool>.get_Value, otherwise the
    	// constant `mov al, 1` (0x180D20602) = DefaultMatchScreenWidth.
    	[Token(Token = "0x17000016")]
    	private bool AssignedMatchScreenWidth
    	{
    		[Token(Token = "0x60000CC")]
    		[Address(RVA = "0xD20560", Offset = "0xD1EF60", VA = "0x180D20560")]
    		get
    		{
    			bool? matchScreenWidth = MatchScreenWidth;
    			return matchScreenWidth ?? DefaultMatchScreenWidth;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD20610, 239 B
    	// Lazy: `_actorUtility` (+0xE0) is created on first use as
    	// new ModifyActorUtility<IBackgroundActor, IMultipliableBackgroundManager>(ActorManager)  (vtable offset 0x2E8 =
    	// slot 27 = the ActorManager getter, il2cpp_codegen_object_new + the utility's .ctor 0x18086BAE0) and cached.
    	// Annotated disassembly: work/title-path/disas/mbe_05_get_ActorUtility.txt
    	[Token(Token = "0x17000017")]
    	private ModifyActorUtility<IBackgroundActor, IMultipliableBackgroundManager> ActorUtility
    	{
    		[Token(Token = "0x60000CD")]
    		[Address(RVA = "0xD20610", Offset = "0xD1F010", VA = "0x180D20610")]
    		get
    		{
    			return _actorUtility ??= new ModifyActorUtility<IBackgroundActor, IMultipliableBackgroundManager>(ActorManager);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD20700 (kickoff) / 0xD23130 (<PreloadResources>d__14.MoveNext, 1553 B)
    	// ModifyActor.PreloadResources with the resource work moved into the shared ModifyActorUtility (and the sub-id):
    	//   State -1  [+0x58] DissolveTexturePath assigned (ICommandParameter.HasValue) && !DynamicValue
    	//             (CommandParameter<string>.get_DynamicValue 0x180C30EE0) ->
    	//             _preloadedDissolveTexture = await ActorUtility.GetDissolveTexture(DissolveTexturePath, default ct)
    	//             (0x180D2330C: r8 = StringParameter.op_Implicit, r9 = 0)
    	//   vtable 0x318 = slot 30 = AllowPreload false, or AssignedId (slot 18) null/empty, or == "*"
    	//   (0x180D234E1, STR '*', reference-equal then SequenceEqual) -> done
    	//   0x180D23593  await ActorUtility.PreloadResources(AssignedId, AssignedSubId, AssignedAppearance (slot 20), this)
    	// Annotated disassembly: work/title-path/disas/mbe_06_PreloadResources.txt, mbe_07_PreloadResources_MoveNext.txt
    	[Token(Token = "0x60000CE")]
    	[Address(RVA = "0xD20700", Offset = "0xD1F100", VA = "0x180D20700", Slot = "32")]
    	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__14))]
    	public override UniTask PreloadResources()
    	{
    		return PreloadResourcesCore();
    	}

    	private async UniTask PreloadResourcesCore()
    	{
    		if (Assigned(DissolveTexturePath) && !DissolveTexturePath.DynamicValue)
    		{
    			_preloadedDissolveTexture = await ActorUtility.GetDissolveTexture(DissolveTexturePath);
    		}
    		if (!AllowPreload || string.IsNullOrEmpty(AssignedId) || AssignedId == "*")
    		{
    			return;
    		}
    		await ActorUtility.PreloadResources(AssignedId, AssignedSubId, AssignedAppearance, this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD20820, 319 B
    	//   0x180D20851  _preloadedDissolveTexture = null (+0xE8)
    	//   AllowPreload (slot 30) && AssignedId (slot 18) non-empty ->
    	//   0x180D20945  ActorUtility.ReleaseResources(AssignedId, AssignedAppearance (slot 20), this)
    	//                (unlike PreloadResources, no sub-id is passed: the utility's signature is (id, appearance, holder))
    	// Annotated disassembly: work/title-path/disas/mbe_08_ReleaseResources.txt
    	[Token(Token = "0x60000CF")]
    	[Address(RVA = "0xD20820", Offset = "0xD1F220", VA = "0x180D20820", Slot = "33")]
    	public override void ReleaseResources()
    	{
    		_preloadedDissolveTexture = null;
    		if (AllowPreload && !string.IsNullOrEmpty(AssignedId))
    		{
    			ActorUtility.ReleaseResources(AssignedId, AssignedAppearance, this);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD20960 (kickoff) / 0xD21B00 (<Execute>d__16.MoveNext, 1838 B)
    	// State -1:
    	//   0x180D21C65  StringUtils.EqualsFastIgnoreCase(AssignedId (slot 18), STR 'Stills') true ->
    	//     0x180D21C8E  unlockables = Engine.GetServiceOrErr<IUnlockableManager>()
    	//     0x180D21C9D  state = Engine.GetServiceOrErr<IStateManager>()
    	//     0x180D21CD7  unlockables.UnlockItem(AssignedAppearance (slot 20))   (IUnlockableManager slot 5 in dump.cs)
    	//     0x180D21CF4  await state.SaveGlobal()                            (IStateManager slot 32 in dump.cs)
    	//   0x180D21E48 and on: the package's ModifyActor.Execute state machine inlined -- `await base.Execute(token)`
    	//   (its own Start/get_Task at 0x180D21FCD/0x180D21FD9).
    	// A still is therefore unlocked for the gallery *by the command that shows it*.
    	// Annotated disassembly: work/title-path/disas/mbe_09_Execute.txt, mbe_10_Execute_MoveNext.txt
    	[Token(Token = "0x60000D0")]
    	[Address(RVA = "0xD20960", Offset = "0xD1F360", VA = "0x180D20960", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__16))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return ExecuteCore(token);
    	}

    	private async UniTask ExecuteCore(AsyncToken token)
    	{
            // The empty epilogue plate is auxiliary art, and must not create a global CG unlock record.
    		if (AssignedId.EqualsFastIgnoreCase(StillId) && AssignedAppearance != "EpilogueEmpty")
    		{
    			IUnlockableManager unlockableManager = Engine.GetServiceOrErr<IUnlockableManager>();
    			IStateManager stateManager = Engine.GetServiceOrErr<IStateManager>();
    			unlockableManager.UnlockItem(AssignedAppearance);
    			await stateManager.SaveGlobal();
    		}
    		await base.Execute(token);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD20AE0 (kickoff) / 0xD22230 (<Modify>d__17.MoveNext, 3+ KB)
    	// ModifyActor.Modify with the sub-id: only the actor resolution differs.
    	//   0x180D22474  ActorManager (slot 27) == null -> Err("Can't resolve actors manager."); return
    	//   0x180D224B2  AssignedId (slot 18) null/empty -> Err("Actor ID was not specified."); return
    	//   0x180D22533  easingType = ActorManager.Configuration.DefaultEasing   (IEngineService<BackgroundsConfiguration>
    	//                getter, then the field at +0x1C)
    	//   0x180D22597  EasingTypeName assigned && !FastEnum.TryParse<EasingType>((string)EasingTypeName, out easingType)
    	//                -> Warn(String.Format("Failed to parse `{0}` easing.", EasingTypeName))  (Command vtable slot 7)
    	//   AssignedId == "*" (0x180D225FA, STR '*'):
    	//     using var _ = ListPool<UniTask>.Rent(out var tasks)                  (CollectionPool.Rent 0x1808128D0)
    	//     ActorManager.Actors.Where(<>c.<Modify>b__17_0)  -- the lambda at 0xD212E0 is `actor => actor.Visible`
    	//       (IActor interface slot 3); each -> tasks.Add(ApplyModifications(actor, easingType, token))  (slot 35)
    	//     await UniTask.WhenAll(tasks)
    	//   otherwise:
    	//     0x180D2268F  actor = await ActorUtility.GetOrAddActor(AssignedId, AssignedSubId)
    	//     0x180D22941  token.ThrowIfCanceled()
    	//     await ApplyModifications(actor, easingType, token)                   (slot 35)
    	// Annotated disassembly: work/title-path/disas/mbe_11_Modify.txt, mbe_12_Modify_MoveNext.txt, mbe_16_Modify_lambda.txt
    	[Token(Token = "0x60000D1")]
    	[Address(RVA = "0xD20AE0", Offset = "0xD1F4E0", VA = "0x180D20AE0", Slot = "34")]
    	[AsyncStateMachine(typeof(_003CModify_003Ed__17))]
    	protected override UniTask Modify(AsyncToken token)
    	{
    		return ModifyCore(token);
    	}

    	private async UniTask ModifyCore(AsyncToken token)
    	{
    		if (ActorManager is null)
    		{
    			Err("Can't resolve actors manager.");
    			return;
    		}
    		if (string.IsNullOrEmpty(AssignedId))
    		{
    			Err("Actor ID was not specified.");
    			return;
    		}
    		EasingType easingType = ActorManager.Configuration.DefaultEasing;
    		if (Assigned(EasingTypeName) && !FastEnum.TryParse<EasingType>(EasingTypeName, out easingType))
    		{
    			Warn(string.Format("Failed to parse `{0}` easing.", EasingTypeName));
    		}
    		if (AssignedId == "*")
    		{
    			using var _ = ListPool<UniTask>.Rent(out List<UniTask> tasks);
    			foreach (IBackgroundActor visibleActor in ActorManager.Actors.Where(a => a.Visible))
    			{
    				tasks.Add(ApplyModifications(visibleActor, easingType, token));
    			}
    			await UniTask.WhenAll(tasks);
    		}
    		else
    		{
    			IBackgroundActor actor = await ActorUtility.GetOrAddActor(AssignedId, AssignedSubId);
    			token.ThrowIfCanceled();
    			await ApplyModifications(actor, easingType, token);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD20C70 (kickoff) / 0xD21330 (<ApplyAppearanceModification>d__18.MoveNext, 1994 B)
    	// ModifyActor.ApplyAppearanceModification with the transition / dissolve-texture work delegated to ActorUtility:
    	//   0x180D2149A  AssignedAppearance (slot 20) null/empty -> return
    	//   0x180D214EE  DissolveTexturePath [+0x58] assigned && !ObjectUtils.IsValid(_preloadedDissolveTexture) (0x180D214FD)
    	//     0x180D21546  _preloadedDissolveTexture = await ActorUtility.GetDissolveTexture(
    	//                   (string)DissolveTexturePath, token.CancellationToken [r9 = the first qword of the token])
    	//   0x180D2170B  transition = ActorUtility.BuildAppearanceTransition(AssignedTransition (slot 19),
    	//                TransitionParams [+0x50], _preloadedDissolveTexture [+0xE8])
    	//   0x180D21856  await actor.ChangeAppearance(AssignedAppearance, tween, transition, token)  (interface call; the
    	//                Transition is wrapped in Nullable<Transition>, 0x180D21794)
    	// Annotated disassembly: work/title-path/disas/mbe_13_ApplyAppearanceModification.txt, mbe_14_..._MoveNext.txt
    	[Token(Token = "0x60000D2")]
    	[Address(RVA = "0xD20C70", Offset = "0xD1F670", VA = "0x180D20C70", Slot = "36")]
    	[AsyncStateMachine(typeof(_003CApplyAppearanceModification_003Ed__18))]
    	protected override UniTask ApplyAppearanceModification(IBackgroundActor actor, Tween tween, AsyncToken token)
    	{
    		return ApplyAppearanceModificationCore(actor, tween, token);
    	}

    	private async UniTask ApplyAppearanceModificationCore(IBackgroundActor actor, Tween tween, AsyncToken token)
    	{
    		if (string.IsNullOrEmpty(AssignedAppearance))
    		{
    			return;
    		}
    		if (Assigned(DissolveTexturePath) && !ObjectUtils.IsValid(_preloadedDissolveTexture))
    		{
    			_preloadedDissolveTexture = await ActorUtility.GetDissolveTexture(DissolveTexturePath, token.CancellationToken);
    		}
    		Transition transition = ActorUtility.BuildAppearanceTransition(AssignedTransition, TransitionParams, _preloadedDissolveTexture);
    		await actor.ChangeAppearance(AssignedAppearance, tween, transition, token);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD20E80, 529 B
    	// Two conditions read at the top (+0xB0 = ScenePosition, ICommandParameter.HasValue thunk 0x1800022E0):
    	//   !Assigned(ScenePosition) && PosedPosition == null (0x180D20F1C, ModifyActor.get_PosedPosition)
    	//        -> 0x180D21087  tail call ModifyOrthoActor.AttemptScenePosition  == ModifyOrthoActor.AssignedPosition
    	//           (the package spells its AttemptScenePosition private, so `base.AssignedPosition` is the same call)
    	//   !Assigned(ScenePosition)  -> 0x180D21060  PosedPosition
    	//   Assigned(ScenePosition)   -> 0x180D21047  ActorUtility.EvaluateWorldPosition(ScenePosition,
    	//        AssignedMatchScreenWidth)   (r8b = the Nullable<bool>.get_Value or the constant 1)
    	// Annotated disassembly: work/title-path/disas/mbe_15_AttemptScenePosition.txt
    	[Token(Token = "0x60000D3")]
    	[Address(RVA = "0xD20E80", Offset = "0xD1F880", VA = "0x180D20E80")]
    	private float?[] AttemptScenePosition()
    	{
    		if (!Assigned(ScenePosition) && PosedPosition is null)
    		{
    			return base.AssignedPosition;
    		}
    		if (!Assigned(ScenePosition))
    		{
    			return PosedPosition;
    		}
    		return ActorUtility.EvaluateWorldPosition(ScenePosition, AssignedMatchScreenWidth);
    	}

    	[Token(Token = "0x60000D4")]
    	[Address(RVA = "0x4D8860", Offset = "0x4D7260", VA = "0x1804D8860")]
    	public ModifyBackgroundExtended()
    	{
    	}
    }
}
