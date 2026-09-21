using FastEnumUtility;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000043")]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [CommandAlias("char")]
    public class ModifyCharacterExtended : ModifyCharacter
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000045")]
    	[CompilerGenerated]
    	private struct _003CApplyAppearanceModification_003Ed__16
    {
    		[Token(Token = "0x40000FB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000FC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000FD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ModifyCharacterExtended _003C_003E4__this;

    		[Token(Token = "0x40000FE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40000FF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public ICharacterActor actor;

    		[Token(Token = "0x4000100")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public Tween tween;

    		[Token(Token = "0x4000101")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<Texture2D>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000102")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60000EE")]
    		[Address(RVA = "0xD242A0", Offset = "0xD22CA0", VA = "0x180D242A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000EF")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000046")]
    	[CompilerGenerated]
    	private struct _003CModify_003Ed__15
    {
    		[Token(Token = "0x4000103")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000104")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000105")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ModifyCharacterExtended _003C_003E4__this;

    		[Token(Token = "0x4000106")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4000107")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private EasingType _003CeasingType_003E5__2;

    		[Token(Token = "0x4000108")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private PooledObject<List<UniTask>> _003C__003E5__3;

    		[Token(Token = "0x4000109")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400010A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask<ICharacterActor>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60000F0")]
    		[Address(RVA = "0xD24A70", Offset = "0xD23470", VA = "0x180D24A70", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000F1")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000047")]
    	[CompilerGenerated]
    	private struct _003CPreloadResources_003Ed__13
    {
    		[Token(Token = "0x400010B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400010C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400010D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ModifyCharacterExtended _003C_003E4__this;

    		[Token(Token = "0x400010E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask<Texture2D>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400010F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60000F2")]
    		[Address(RVA = "0xD25DF0", Offset = "0xD247F0", VA = "0x180D25DF0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000F3")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40000F4")]
    	private const bool DefaultMatchScreenWidth = true;

    	[Token(Token = "0x40000F5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	public StringParameter SubId;

    	[Token(Token = "0x40000F6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("match")]
    	public BooleanParameter MatchScreenWidth;

    	[Token(Token = "0x40000F7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
    	private ModifyActorUtility<ICharacterActor, IMultipliableCharacterManager> _actorUtility;

    	[Token(Token = "0x40000F8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
    	private Texture2D _preloadedDissolveTexture;

    	[Token(Token = "0x17000018")]
    	protected override float?[] AssignedPosition
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD23750, 7 B
    		// `xor edx, edx; jmp AttemptScenePosition` -- the override is a forwarder to the private method below (same shape as
    		// ModifyBackgroundExtended.AssignedPosition and ModifyOrthoActor's own AssignedPosition).
    		// Annotated disassembly: work/title-path/disas/mce_01_get_AssignedPosition.txt
    		[Token(Token = "0x60000E1")]
    		[Address(RVA = "0xD23750", Offset = "0xD22150", VA = "0x180D23750", Slot = "22")]
    		get
    		{
    			return AttemptScenePosition();
    		}
    	}

    	[Token(Token = "0x17000019")]
    	private string AssignedSubId
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD23760, 133 B
    		// `SubId` (+0xE8): null -> null, !HasValue -> null, else Nullable<string>.get_Value: the body of StringParameter.op_Implicit,
    		// i.e. `=> SubId;` (same shape as ModifyBackgroundExtended.AssignedSubId).
    		[Token(Token = "0x60000E2")]
    		[Address(RVA = "0xD23760", Offset = "0xD22160", VA = "0x180D23760")]
    		get
    		{
    			return SubId;
    		}
    	}

    	[Token(Token = "0x1700001A")]
    	private bool AssignedMatchScreenWidth
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD237F0, 175 B
    		// `MatchScreenWidth` (+0xF0): non-null and ICommandParameter.HasValue -> Nullable<bool>.get_Value, otherwise the constant
    		// `mov al, 1` (0x180D2388D) = DefaultMatchScreenWidth.
    		[Token(Token = "0x60000E3")]
    		[Address(RVA = "0xD237F0", Offset = "0xD221F0", VA = "0x180D237F0")]
    		get
    		{
    			bool? matchScreenWidth = MatchScreenWidth;
    			return matchScreenWidth ?? DefaultMatchScreenWidth;
    		}
    	}

    	[Token(Token = "0x1700001B")]
    	private ModifyActorUtility<ICharacterActor, IMultipliableCharacterManager> ActorUtility
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD238A0, 239 B
    		// Lazy: `_actorUtility` (+0xF8) is created on first use as
    		// new ModifyActorUtility<ICharacterActor, IMultipliableCharacterManager>(ActorManager) (vtable +0x2E8 = slot 27, the ActorManager
    		// getter) and cached; the twin of ModifyBackgroundExtended.ActorUtility.
    		// Annotated disassembly: work/title-path/disas/mce_04_get_ActorUtility.txt
    		[Token(Token = "0x60000E4")]
    		[Address(RVA = "0xD238A0", Offset = "0xD222A0", VA = "0x180D238A0")]
    		get
    		{
    			return _actorUtility ??= new ModifyActorUtility<ICharacterActor, IMultipliableCharacterManager>(ActorManager);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD23990 (kickoff) / 0xD25DF0 (<PreloadResources>d__13.MoveNext, 1553 B)
    	// The body lives in PreloadResourcesCore (the repo's XxxCore convention; the <PreloadResources>d__13 metadata stays).
    	// Identical to ModifyBackgroundExtended.PreloadResources (only the utility's type arguments differ):
    	//   State -1  DissolveTexturePath [+0x58] assigned && !DynamicValue (CommandParameter<string>.get_DynamicValue 0x180C30EE0) ->
    	//             _preloadedDissolveTexture (+0x100) = await ActorUtility.GetDissolveTexture(DissolveTexturePath, default ct) (0x180D25FCC)
    	//   vtable +0x318 = slot 30 AllowPreload false, AssignedId (slot 18, +0x258) null/empty, or == "*" (0x180D261A1) -> done
    	//   0x180D26253  await ActorUtility.PreloadResources(AssignedId, AssignedSubId, AssignedAppearance (slot 20, +0x278), this)
    	// Annotated disassembly: work/title-path/disas/mce_05_PreloadResources, mce_113_d__13_MoveNext.txt
    	[Token(Token = "0x60000E5")]
    	[Address(RVA = "0xD23990", Offset = "0xD22390", VA = "0x180D23990", Slot = "32")]
    	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__13))]
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD23AB0, 319 B
    	//   0x180D23AE1  _preloadedDissolveTexture = null (+0x100)
    	//   AllowPreload (slot 30) && AssignedId (slot 18) non-empty -> 0x180D23BD5 ActorUtility.ReleaseResources(AssignedId,
    	//   AssignedAppearance (slot 20), this) (no sub-id, as in ModifyBackgroundExtended)
    	// Annotated disassembly: work/title-path/disas/mce_06_ReleaseResources.txt
    	[Token(Token = "0x60000E6")]
    	[Address(RVA = "0xD23AB0", Offset = "0xD224B0", VA = "0x180D23AB0", Slot = "33")]
    	public override void ReleaseResources()
    	{
    		_preloadedDissolveTexture = null;
    		if (AllowPreload && !string.IsNullOrEmpty(AssignedId))
    		{
    			ActorUtility.ReleaseResources(AssignedId, AssignedAppearance, this);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD23BF0 (kickoff) / 0xD24A70 (<Modify>d__15.MoveNext, 4.6 KB)
    	// ModifyActor.Modify with the sub-id (the twin of ModifyBackgroundExtended.Modify) followed by ModifyCharacter's avatar-texture
    	// block -- ModifyCharacter.Modify is NOT called: its body is inlined, and AFTER the actor modification (the stock command
    	// assigns the avatar first and then awaits base.Modify).  The stub returned default(UniTask), so every @char was a no-op and
    	// 'Hiro' / 'Warden-Middle' / 'Ema' hides failed with "can't find any managers with '<id>' actor" (G4b round 20/21).
    	//   0x180D250F4  ActorManager (slot 27, +0x2E8) null -> Err("Can't resolve actors manager."); return
    	//   0x180D250D4  AssignedId (slot 18) null/empty -> Err("Actor ID was not specified."); return
    	//   0x180D24DA5  easingType = ActorManager.Configuration.DefaultEasing (+0x1C); EasingTypeName [+0x88] assigned and
    	//                FastEnum.TryParse fails -> Warn(String.Format("Failed to parse `{0}` easing.", EasingTypeName)) (vtable +0x1A8)
    	//   AssignedId == "*" (0x180D24E61):  using ListPool<UniTask>.Rent (0x1808128D0); ActorManager.Actors.Where(<>c.<Modify>b__15_0
    	//                = actor => actor.Visible, 0xD24250, IActor slot 3); each -> tasks.Add(ApplyModifications(actor, easingType, token))
    	//                (vtable +0x368 = slot 35); await UniTask.WhenAll(tasks) (state 0)
    	//   otherwise:   0x180D24EF6 await ActorUtility.GetOrAddActor(AssignedId, AssignedSubId) (state 1); token.ThrowIfCanceled
    	//                (0x180D25199); await ApplyModifications(actor, easingType, token) (state 2)
    	//   0x180D258FE  avatar block on AvatarTexturePath [+0xD8] (ICharacterManager slots 2 AvatarTextureExists, 3 RemoveAvatarTextureFor,
    	//                5 GetAvatarTexturePathFor, 6 SetAvatarTexturePathFor through the interface thunks 0x180003140 / 0x180008000):
    	//                assigned: Value equals "none" (StringUtils.EqualsFastIgnoreCase 0x18054F660) -> RemoveAvatarTextureFor(AssignedId),
    	//                else SetAvatarTexturePathFor(AssignedId, AvatarTexturePath); not assigned: AssignedId + "/" + AssignedAppearance
    	//                (String.Concat 0x180E267A0) when that avatar texture exists and is not yet the current one, else
    	//                AssignedId + "/Default" (0x180E265E0) under the same conditions
    	// Annotated disassembly: work/title-path/disas/mce_07_Modify, mce_115_d__15_MoveNext, mce_117_Modify_lambda.txt
    	[Token(Token = "0x60000E7")]
    	[Address(RVA = "0xD23BF0", Offset = "0xD225F0", VA = "0x180D23BF0", Slot = "34")]
    	[AsyncStateMachine(typeof(_003CModify_003Ed__15))]
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
    			foreach (ICharacterActor visibleActor in ActorManager.Actors.Where(a => a.Visible))
    			{
    				tasks.Add(ApplyModifications(visibleActor, easingType, token));
    			}
    			await UniTask.WhenAll(tasks);
    		}
    		else
    		{
    			ICharacterActor actor = await ActorUtility.GetOrAddActor(AssignedId, AssignedSubId);
    			token.ThrowIfCanceled();
    			await ApplyModifications(actor, easingType, token);
    		}
    		if (Assigned(AvatarTexturePath))
    		{
    			// 0x180D25958: the stock `AvatarTexturePath?.Value...` null test is in the shipped binary (a null parameter takes the Set branch)
    			if (AvatarTexturePath != null && AvatarTexturePath.Value.EqualsFastIgnoreCase("none"))
    			{
    				ActorManager.RemoveAvatarTextureFor(AssignedId);
    			}
    			else
    			{
    				ActorManager.SetAvatarTexturePathFor(AssignedId, AvatarTexturePath);
    			}
    		}
    		else
    		{
    			string avatarPath = AssignedId + "/" + AssignedAppearance;
    			if (ActorManager.AvatarTextureExists(avatarPath))
    			{
    				if (ActorManager.GetAvatarTexturePathFor(AssignedId) != avatarPath)
    				{
    					ActorManager.SetAvatarTexturePathFor(AssignedId, avatarPath);
    				}
    			}
    			else
    			{
    				string defaultAvatarPath = AssignedId + "/Default";
    				if (ActorManager.AvatarTextureExists(defaultAvatarPath) && ActorManager.GetAvatarTexturePathFor(AssignedId) != defaultAvatarPath)
    				{
    					ActorManager.SetAvatarTexturePathFor(AssignedId, defaultAvatarPath);
    				}
    			}
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD23D80 (kickoff) / 0xD242A0 (<ApplyAppearanceModification>d__16.MoveNext, 1994 B)
    	// The twin of ModifyBackgroundExtended.ApplyAppearanceModification (transition and dissolve texture delegated to ActorUtility):
    	//   0x180D24413  AssignedAppearance (slot 20) null/empty -> return
    	//   0x180D2446D  DissolveTexturePath [+0x58] assigned && !ObjectUtils.IsValid(_preloadedDissolveTexture) ->
    	//                _preloadedDissolveTexture = await ActorUtility.GetDissolveTexture((string)DissolveTexturePath, token.CancellationToken)
    	//   0x180D2467B  transition = ActorUtility.BuildAppearanceTransition(AssignedTransition (slot 19), TransitionParams [+0x50],
    	//                _preloadedDissolveTexture)
    	//   0x180D247C6  await actor.ChangeAppearance(AssignedAppearance, tween, transition, token) (IActor interface call; the
    	//                Transition is wrapped in Nullable<Transition>, 0x180D24704)
    	// Annotated disassembly: work/title-path/disas/mce_08_ApplyAppearanceModification, mce_116_d__16_MoveNext.txt
    	[Token(Token = "0x60000E8")]
    	[Address(RVA = "0xD23D80", Offset = "0xD22780", VA = "0x180D23D80", Slot = "36")]
    	[AsyncStateMachine(typeof(_003CApplyAppearanceModification_003Ed__16))]
    	protected override UniTask ApplyAppearanceModification(ICharacterActor actor, Tween tween, AsyncToken token)
    	{
    		return ApplyAppearanceModificationCore(actor, tween, token);
    	}

    	private async UniTask ApplyAppearanceModificationCore(ICharacterActor actor, Tween tween, AsyncToken token)
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD23F90, 529 B
    	// Two conditions read at the top (+0xB0 = ScenePosition, ICommandParameter.HasValue thunk 0x1800022E0):
    	//   !Assigned(ScenePosition) && PosedPosition == null (0x180D24031, ModifyActor.get_PosedPosition)
    	//        -> 0x180D24197  tail call ModifyOrthoActor.AttemptScenePosition == ModifyOrthoActor.AssignedPosition (`base.AssignedPosition`)
    	//   !Assigned(ScenePosition)  -> 0x180D24170  PosedPosition
    	//   Assigned(ScenePosition)   -> 0x180D24157  ActorUtility.EvaluateWorldPosition(ScenePosition, AssignedMatchScreenWidth)
    	// Annotated disassembly: work/title-path/disas/mce_09_AttemptScenePosition.txt
    	[Token(Token = "0x60000E9")]
    	[Address(RVA = "0xD23F90", Offset = "0xD22990", VA = "0x180D23F90")]
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

    	[Token(Token = "0x60000EA")]
    	[Address(RVA = "0x4D9600", Offset = "0x4D8000", VA = "0x1804D9600")]
    	public ModifyCharacterExtended()
    	{
    	}
    }
}
