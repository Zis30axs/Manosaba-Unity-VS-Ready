#nullable enable annotations
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastEnumUtility;
using GigaCreation.NaninovelExtender.Cameras;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using Naninovel.FX;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x200004B")]
    public class AnimateExtended : Animate
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200004E")]
    	[CompilerGenerated]
    	private struct _003CAnimateKey_003Ed__7
    {
    		[Token(Token = "0x400011F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000120")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000121")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AnimateExtended _003C_003E4__this;

    		[Token(Token = "0x4000122")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public int keyIndex;

    		[Token(Token = "0x4000123")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public IActor actor;

    		[Token(Token = "0x4000124")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x4000125")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600010F")]
    		[Address(RVA = "0xD28B70", Offset = "0xD27570", VA = "0x180D28B70", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000110")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	// The three field initialisers are the first statements of the constructor (0xD28540): List<string>..ctor stored
    	// to +0xB0, List<string>..ctor to +0xB8, List<UniTask>..ctor to +0xC0, before the base constructor call.
    	[Token(Token = "0x4000116")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
    	private readonly List<string> _easingTypeNameForPosX = new List<string>();

    	[Token(Token = "0x4000117")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	private readonly List<string> _easingTypeNameForPosY = new List<string>();

    	[Token(Token = "0x4000118")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
    	private readonly IList<UniTask> _tasks = new List<UniTask>();

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD27530, 544 B
    	// The spawn parameter list is the stock 13 entries followed by one per AnimateActorExtended.AdditionalParam:
    	//   0x180D275CA  n = FastEnum.GetNames<AdditionalParam>().Count (5);   split = parameters.Count - n
    	//   0x180D27601  stock      = parameters.Take(split).ToList()
    	//   0x180D27670  additional = parameters.Skip(split).ToList()
    	//   0x180D276D7  base.SetSpawnParameters(stock, asap)                 (Animate.SetSpawnParameters, 0x1804B1A30)
    	//   0x180D27702  tail call ParseAdditionalParameters(additional)
    	// Annotated disassembly: work/title-path/disas/ae_01_SetSpawnParameters.txt
    	[Token(Token = "0x60000FF")]
    	[Address(RVA = "0xD27530", Offset = "0xD25F30", VA = "0x180D27530", Slot = "26")]
    	public override void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    		int split = parameters.Count - FastEnum.GetNames<AnimateActorExtended.AdditionalParam>().Count;
    		List<string> stockParameters = parameters.Take(split).ToList();
    		List<string> additionalParameters = parameters.Skip(split).ToList();
    		base.SetSpawnParameters(stockParameters, asap);
    		ParseAdditionalParameters(additionalParameters);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD27760, 1920 B
    	//   additional = parameters.Select((value, index) => (value, index))                    (<>c.b__4_0, 0xD287D0)
    	//                          .ToDictionary(t => (AdditionalParam)t.index, t => t.value)   (b__4_1 0x56B0D0 = Item2,
    	//                                                                                       b__4_2 0x3570C0 = Item1)
    	//   [MatchScreenWidth] TryParseParameter(additional[0], out keys) -> keys.ElementAtOrDefault(0) non-null and
    	//        bool.TryParse(span) -> matchScreenWidth (bool?); otherwise it stays null
    	//   0x180D27CFC  screenRect = CameraConfigurationUtility.EvaluateSceneRect(matchScreenWidth)   (closure field)
    	//   [ScenePositionX] TryParseParameter -> AssignKeys(keys, PositionX (vtable +0x1E8), b__3)
    	//        b__3 (0xD288A0, closure):  key => key.AsInvariantFloat().GetValueOrDefault() / 100f * screenRect.width  + screenRect.x
    	//   [ScenePositionY] -> AssignKeys(keys, PositionY (+0x1F8), b__4 0xD28A00)
    	//        b__4:                      key => key.AsInvariantFloat().GetValueOrDefault() / 100f * screenRect.height + screenRect.y
    	//   [EasingTypeNameForPosX] -> AssignKeys(keys, _easingTypeNameForPosX)  (0x180D27E6F, no parser)
    	//   [EasingTypeNameForPosY] -> AssignKeys(keys, _easingTypeNameForPosY)  (0x180D27EBE)
    	//   (the dictionary is read through IReadOnlyDictionary's indexer, sub_36F0)
    	// Annotated disassembly: work/title-path/disas/ae_02_ParseAdditionalParameters.txt, ae_08..ae_13
    	[Token(Token = "0x6000100")]
    	[Address(RVA = "0xD27760", Offset = "0xD26160", VA = "0x180D27760")]
    	private void ParseAdditionalParameters(IEnumerable<string> parameters)
    	{
    		IReadOnlyDictionary<AnimateActorExtended.AdditionalParam, string> additional = parameters
    			.Select((value, index) => (value, index))
    			.ToDictionary(tuple => (AnimateActorExtended.AdditionalParam)tuple.index, tuple => tuple.value);

    		bool? matchScreenWidth = null;
    		if (TryParseParameter(additional[AnimateActorExtended.AdditionalParam.MatchScreenWidth], out string[] matchScreenWidthKeys))
    		{
    			string first = matchScreenWidthKeys.ElementAtOrDefault(0);
    			if (first != null && bool.TryParse(first, out bool parsed))
    			{
    				matchScreenWidth = parsed;
    			}
    		}
    		Rect screenRect = CameraConfigurationUtility.EvaluateSceneRect(matchScreenWidth);

    		if (TryParseParameter(additional[AnimateActorExtended.AdditionalParam.ScenePositionX], out string[] positionXKeys))
    		{
    			AssignKeys(positionXKeys, PositionX, key => key.AsInvariantFloat().GetValueOrDefault() / 100f * screenRect.width + screenRect.x);
    		}
    		if (TryParseParameter(additional[AnimateActorExtended.AdditionalParam.ScenePositionY], out string[] positionYKeys))
    		{
    			AssignKeys(positionYKeys, PositionY, key => key.AsInvariantFloat().GetValueOrDefault() / 100f * screenRect.height + screenRect.y);
    		}
    		if (TryParseParameter(additional[AnimateActorExtended.AdditionalParam.EasingTypeNameForPosX], out string[] easingXKeys))
    		{
    			AssignKeys(easingXKeys, _easingTypeNameForPosX);
    		}
    		if (TryParseParameter(additional[AnimateActorExtended.AdditionalParam.EasingTypeNameForPosY], out string[] easingYKeys))
    		{
    			AssignKeys(easingYKeys, _easingTypeNameForPosY);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD27EF0, 615 B
    	//   keys = parameter?.Split(AnimateActor.KeyDelimiters)      (String.SplitInternal 0x180E28F40, count = int.MaxValue)
    	//   keys null or empty -> false (0x180D28145);  else keys.Any(<>c.<TryParseParameter>b__5_0)  (0xD28840: `s => s != string.Empty`)
    	// Annotated disassembly: work/title-path/disas/ae_03_TryParseParameter.txt, ae_08_lambda_TryParse_b5_0.txt
    	[Token(Token = "0x6000101")]
    	[Address(RVA = "0xD27EF0", Offset = "0xD268F0", VA = "0x180D27EF0")]
    	private static bool TryParseParameter(string parameter, out string[] keys)
    	{
    		keys = parameter?.Split(AnimateActor.KeyDelimiters);
    		return keys != null && keys.Length != 0 && keys.Any(s => s != string.Empty);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x656020 (shared generic instantiation, 942 B)
    	// The package's local function Animate.ParseParameter.AssignKeys, hoisted to a method with the key list passed in:
    	//   0x18065616A  KeyCount (vtable 0x268 = slot 19);  Enumerable.Repeat(default(T), KeyCount)   (0x1806561A9)
    	//   0x1806561DF  parameter.InsertRange(parameter.Count, defaults)            (== AddRange)
    	//   loop i = 0 .. keys.Count-1 (IReadOnlyCollection<string>.Count each iteration):
    	//     keys[i] (IReadOnlyList<string>) non-null and non-empty ->
    	//       parameter[i] = parseKey is null ? (T)(object)keys[i] : parseKey(keys[i])   (List<T>.set_Item 0x1808582E0)
    	// Annotated disassembly: work/title-path/disas/ae_14_AssignKeys.txt
    	[Token(Token = "0x6000102")]
    	private void AssignKeys<T>(IReadOnlyList<string> keys, List<T> parameter, [Optional] Func<string, T> parseKey)
    	{
    		IEnumerable<T> defaultKeys = Enumerable.Repeat<T>(default, KeyCount);
    		parameter.AddRange(defaultKeys);
    		for (int keyIdx = 0; keyIdx < keys.Count; keyIdx++)
    		{
    			if (!string.IsNullOrEmpty(keys[keyIdx]))
    			{
    				parameter[keyIdx] = parseKey is null ? (T)(object)keys[keyIdx] : parseKey(keys[keyIdx]);
    			}
    		}
    	}

    	// RECONSTRUCTED | Confidence: B | RVA: 0xD28160 (kickoff) / 0xD28B70 (<AnimateKey>d__7.MoveNext, 7278 B)
    	// The package's Animate.AnimateKey with three differences read off the state machine:
    	//   * it works on its own `_tasks` list (+0xC0), not on the base `tasks`;
    	//   * position keys are applied per coordinate when the actor implements IPositionChangeableForEachCoordinateActor
    	//     (IsInst 0x180D29743; interface calls ChangePositionX/Y/ZAsync, thunk 0x180080660 with ecx = 0/1/2), X and Y
    	//     using their own easing (`ParseEasing(_easingTypeNameForPos{X,Y}, keyIndex) ?? easingType`, 0x180D29C45 /
    	//     0x180D29DE1) and Z the key's easing (0x180D29F7F); any other actor gets the package's single ChangePosition
    	//     with the tween (0x180D29AE5);
    	//   * the easing name is parsed by ParseEasing below (FastEnum, warning through Debug.LogWarning) and the tint
    	//     warning is Debug.LogWarning(String.Format(..., TintColor, actor.Id)) (0x180D2A567); it does NOT end with
    	//     token.ThrowIfCanceled(gameObject): after `await UniTask.WhenAll(_tasks)` (0x180D2A4E5) the state machine
    	//     goes straight to SetResult (0x180D2A6D3).
    	// State machine order (0x180D28E00 ff.): _tasks.Clear() (ICollection<UniTask> slot 3); Duration (vtable slot 18) index
    	// valid or return; duration = Duration[key] ?? 0; easing = ParseEasing(EasingTypeName (slot 17), key) ?? DefaultEasing
    	// (slot 25); tween = new Tween(duration, easing) (complete = true); Appearance/Transition; Visibility; Position;
    	// RotationZ (ActorExtensions.ChangeRotationZ 0x180447BE0); Scale (`?? Vector3.one`, sub_46CC90); TintColor.
    	// Annotated disassembly: work/title-path/disas/ae_04_AnimateKey.txt, ae_05_AnimateKey_MoveNext.txt
    	[Token(Token = "0x6000103")]
    	[Address(RVA = "0xD28160", Offset = "0xD26B60", VA = "0x180D28160", Slot = "28")]
    	[AsyncStateMachine(typeof(_003CAnimateKey_003Ed__7))]
    	protected override UniTask AnimateKey(IActor actor, int keyIndex, AsyncToken asyncToken)
    	{
    		return AnimateKeyCore(actor, keyIndex, asyncToken);
    	}

    	private async UniTask AnimateKeyCore(IActor actor, int keyIndex, AsyncToken asyncToken)
    	{
    		_tasks.Clear();
    		if (!Duration.IsIndexValid(keyIndex))
    		{
    			return;
    		}
    		float duration = Duration[keyIndex].GetValueOrDefault();
    		EasingType easingType = ParseEasing(EasingTypeName, keyIndex) ?? DefaultEasing;
    		EasingType? easingTypeX = ParseEasing(_easingTypeNameForPosX, keyIndex);
    		EasingType? easingTypeY = ParseEasing(_easingTypeNameForPosY, keyIndex);
    		Tween tween = new Tween(duration, easingType);

    		if (Appearance.ElementAtOrDefault(keyIndex) != null)
    		{
    			string transitionName = !string.IsNullOrEmpty(Transition.ElementAtOrDefault(keyIndex)) ? Transition[keyIndex] : TransitionUtils.DefaultTransition;
    			_tasks.Add(actor.ChangeAppearance(Appearance[keyIndex], tween, new Transition(transitionName), asyncToken));
    		}
    		if (Visibility.ElementAtOrDefault(keyIndex).HasValue)
    		{
    			_tasks.Add(actor.ChangeVisibility(Visibility[keyIndex].GetValueOrDefault(), tween, asyncToken));
    		}
    		if (PositionX.ElementAtOrDefault(keyIndex).HasValue || PositionY.ElementAtOrDefault(keyIndex).HasValue || PositionZ.ElementAtOrDefault(keyIndex).HasValue)
    		{
    			if (actor is IPositionChangeableForEachCoordinateActor coordinateActor)
    			{
    				if (PositionX.ElementAtOrDefault(keyIndex).HasValue)
    				{
    					_tasks.Add(coordinateActor.ChangePositionXAsync(PositionX.ElementAtOrDefault(keyIndex) ?? actor.Position.x, duration, easingTypeX ?? easingType, asyncToken));
    				}
    				if (PositionY.ElementAtOrDefault(keyIndex).HasValue)
    				{
    					_tasks.Add(coordinateActor.ChangePositionYAsync(PositionY.ElementAtOrDefault(keyIndex) ?? actor.Position.y, duration, easingTypeY ?? easingType, asyncToken));
    				}
    				if (PositionZ.ElementAtOrDefault(keyIndex).HasValue)
    				{
    					_tasks.Add(coordinateActor.ChangePositionZAsync(PositionZ.ElementAtOrDefault(keyIndex) ?? actor.Position.z, duration, easingType, asyncToken));
    				}
    			}
    			else
    			{
    				_tasks.Add(actor.ChangePosition(new Vector3(
    					PositionX.ElementAtOrDefault(keyIndex) ?? actor.Position.x,
    					PositionY.ElementAtOrDefault(keyIndex) ?? actor.Position.y,
    					PositionZ.ElementAtOrDefault(keyIndex) ?? actor.Position.z), tween, asyncToken));
    			}
    		}
    		if (RotationZ.ElementAtOrDefault(keyIndex).HasValue)
    		{
    			_tasks.Add(actor.ChangeRotationZ(RotationZ[keyIndex].GetValueOrDefault(), tween, asyncToken));
    		}
    		if (Scale.ElementAtOrDefault(keyIndex).HasValue)
    		{
    			_tasks.Add(actor.ChangeScale(Scale[keyIndex] ?? Vector3.one, tween, asyncToken));
    		}
    		if (TintColor.ElementAtOrDefault(keyIndex) != null)
    		{
    			if (ColorUtility.TryParseHtmlString(TintColor[keyIndex], out Color color))
    			{
    				_tasks.Add(actor.ChangeTintColor(color, tween, asyncToken));
    			}
    			else
    			{
    				UnityEngine.Debug.LogWarning(string.Format("Failed to parse `{0}` color to apply tint animation for `{1}` actor. See the API docs for supported color formats.", TintColor, actor.Id));
    			}
    		}
    		await UniTask.WhenAll(_tasks);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD28360, 449 B
    	//   easingTypeName.ElementAtOrDefault(keyIndex) == null -> null                     (0x180D28406)
    	//   FastEnum.TryParse<EasingType>(easingTypeName[keyIndex], ignoreCase: true, out e) -> e   (dl = 1, 0x180D28465)
    	//   otherwise Debug.LogWarning(String.Format("Failed to parse `{0}` easing.", easingTypeName))  (the whole list is the
    	//   format argument, ParamsArray(rsi) at 0x180D284D7) and null
    	// Annotated disassembly: work/title-path/disas/ae_06_ParseEasing.txt
    	[Token(Token = "0x6000104")]
    	[Address(RVA = "0xD28360", Offset = "0xD26D60", VA = "0x180D28360")]
    	private static EasingType? ParseEasing(IReadOnlyList<string> easingTypeName, int keyIndex)
    	{
    		if (easingTypeName.ElementAtOrDefault(keyIndex) == null)
    		{
    			return null;
    		}
    		if (FastEnum.TryParse<EasingType>(easingTypeName[keyIndex], true, out EasingType easingType))
    		{
    			return easingType;
    		}
    		UnityEngine.Debug.LogWarning(string.Format("Failed to parse `{0}` easing.", easingTypeName));
    		return null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD28540, 0x1E8 B: the three field initialisers above, then Animate..ctor().
    	[Token(Token = "0x6000105")]
    	[Address(RVA = "0xD28540", Offset = "0xD26F40", VA = "0x180D28540")]
    	public AnimateExtended()
    	{
    	}
    }
}
