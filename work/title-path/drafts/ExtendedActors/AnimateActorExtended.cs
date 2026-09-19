using System.Collections.Generic;
using System.Linq;
using FastEnumUtility;
using GigaCreation.NaninovelExtender.Cameras;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000031")]
    [UsedImplicitly]
    [CommandAlias("animate")]
    public class AnimateActorExtended : AnimateActor
    {
    	[Token(Token = "0x2000032")]
    	public enum AdditionalParam
    	{
    		[Token(Token = "0x40000A8")]
    		MatchScreenWidth,
    		[Token(Token = "0x40000A9")]
    		ScenePositionX,
    		[Token(Token = "0x40000AA")]
    		ScenePositionY,
    		[Token(Token = "0x40000AB")]
    		EasingTypeNameForPosX,
    		[Token(Token = "0x40000AC")]
    		EasingTypeNameForPosY
    	}

    	[Token(Token = "0x40000A2")]
    	[FieldOffset(Offset = "0xA0")]
    	[UsedImplicitly]
    	[ParameterAlias("easingPosX")]
    	public StringParameter EasingTypeNameForPosX;

    	[Token(Token = "0x40000A3")]
    	[FieldOffset(Offset = "0xA8")]
    	[UsedImplicitly]
    	[ParameterAlias("easingPosY")]
    	public StringParameter EasingTypeNameForPosY;

    	[Token(Token = "0x40000A4")]
    	[FieldOffset(Offset = "0xB0")]
    	[UsedImplicitly]
    	public BooleanParameter Relative;

    	[Token(Token = "0x40000A5")]
    	[FieldOffset(Offset = "0xB8")]
    	[UsedImplicitly]
    	[ParameterAlias("multi")]
    	public DecimalParameter Multiplier;

    	[Token(Token = "0x40000A6")]
    	[FieldOffset(Offset = "0xC0")]
    	[UsedImplicitly]
    	[ParameterAlias("match")]
    	public BooleanParameter MatchScreenWidth;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1E940, 2686 B
    	// The 13 stock parameters plus one entry per AdditionalParam (5), concatenated:
    	//   0x180D1EAE3  baseParameters = AnimateActor.GetParametersForActor(actorId)   (0x1804D1BE0, string[13])
    	//   0x180D1EBF5  manager = Engine.FindService<IActorManager, string>(actorId, <>c.<GetParametersForActor>b__6_0)
    	//                (the lambda at 0xD1F8A0 is `(m, id) => m.ActorExists(id)`, IActorManager slot 6);
    	//                null -> 0x180D1F31A Engine.Warn("Can't find a manager with `" + actorId + "` actor.") and the
    	//                stock parameters are returned as they are
    	//   0x180D1EC15  actor = manager.GetActor(actorId)                       (IActorManager slot 7)
    	//   0x180D1EC1D  additional = new string[FastEnum.GetNames<AdditionalParam>().Count]   (= 5)
    	//   [MatchScreenWidth] the parameter, else `true` for a background actor / `false` for a character actor (isinst
    	//        IBackgroundActor 0x180D1EC65, ICharacterActor 0x180D1EC84), else nothing; as bool? -> "True"/"False"/null
    	//        (STR 'True', 'False' picked by cmovne at 0x180D1EE17)
    	//   Relative (the bool? tested at 0x180D1EE55; byte 0 of a bool? is its value) ->
    	//     0x180D1EFF6  position = actor.Position                               (IActor slot 5)
    	//     0x180D1F00F  rect = CameraConfigurationUtility.EvaluateSceneRect(matchScreenWidth)
    	//     actorX = (position.x - rect.x) / rect.width,  actorY = (position.y - rect.y) / rect.height
    	//     multiplier = Multiplier assigned ? Multiplier.Value : 1f  (data_2AE2820 = 1.0f)
    	//     [ScenePositionX] = ModifyKeysAsRelative(ScenePositionX, multiplier, actorX)   0x180D1F0D4
    	//     [ScenePositionY] = ModifyKeysAsRelative(ScenePositionY, multiplier, actorY)   0x180D1F0EE
    	//   otherwise [ScenePositionX] / [ScenePositionY] = the parameters' values, or null when not assigned
    	//   [EasingTypeNameForPosX] / [EasingTypeNameForPosY] = the parameters' values, or null
    	//   0x180D1F2B7  return baseParameters.Concat(additional).ToList()          (Enumerable.Concat, ToList)
    	// Annotated disassembly: work/title-path/disas/aae_01_GetParametersForActor.txt, aae_04_...closure.txt
    	[Token(Token = "0x60000A9")]
    	[Address(RVA = "0xD1E940", Offset = "0xD1D340", VA = "0x180D1E940", Slot = "17")]
    	protected override IReadOnlyList<string> GetParametersForActor(string actorId)
    	{
    		IReadOnlyList<string> baseParameters = base.GetParametersForActor(actorId);
    		IActorManager manager = Engine.FindService<IActorManager, string>(actorId, (m, id) => m.ActorExists(id));
    		if (manager is null)
    		{
    			Engine.Warn("Can't find a manager with `" + actorId + "` actor.");
    			return baseParameters;
    		}
    		IActor actor = manager.GetActor(actorId);
    		string[] additional = new string[FastEnum.GetNames<AdditionalParam>().Count];

    		BooleanParameter matchScreenWidthParameter = MatchScreenWidth
    			?? (actor is IBackgroundActor ? (BooleanParameter)true : actor is ICharacterActor ? (BooleanParameter)false : null);
    		bool? matchScreenWidth = matchScreenWidthParameter;
    		additional[(int)AdditionalParam.MatchScreenWidth] = matchScreenWidth.HasValue ? matchScreenWidth.Value.ToString() : null;

    		string scenePositionX;
    		string scenePositionY;
    		bool? relative = Relative;
    		if (relative.GetValueOrDefault())
    		{
    			Vector3 position = actor.Position;
    			Rect sceneRect = CameraConfigurationUtility.EvaluateSceneRect(matchScreenWidth);
    			float actorScreenPositionX = (position.x - sceneRect.x) / sceneRect.width;
    			float actorScreenPositionY = (position.y - sceneRect.y) / sceneRect.height;
    			float multiplier = Assigned(Multiplier) ? Multiplier.Value : 1f;
    			scenePositionX = ModifyKeysAsRelative(ScenePositionX, multiplier, actorScreenPositionX);
    			scenePositionY = ModifyKeysAsRelative(ScenePositionY, multiplier, actorScreenPositionY);
    		}
    		else
    		{
    			scenePositionX = Assigned(ScenePositionX) ? ScenePositionX.Value : null;
    			scenePositionY = Assigned(ScenePositionY) ? ScenePositionY.Value : null;
    		}
    		additional[(int)AdditionalParam.ScenePositionX] = scenePositionX;
    		additional[(int)AdditionalParam.ScenePositionY] = scenePositionY;
    		additional[(int)AdditionalParam.EasingTypeNameForPosX] = Assigned(EasingTypeNameForPosX) ? EasingTypeNameForPosX.Value : null;
    		additional[(int)AdditionalParam.EasingTypeNameForPosY] = Assigned(EasingTypeNameForPosY) ? EasingTypeNameForPosY.Value : null;
    		return baseParameters.Concat(additional).ToList();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1F3C0, 921 B
    	// Rewrites a comma/pipe separated key list so every key becomes relative to the actor's current screen position:
    	//   not assigned                                           -> null                        (0x180D1F74A)
    	//   keys = parameter.Value.Split(AnimateActor.KeyDelimiters)   (String.SplitInternal 0x180E28F40, no limit)
    	//   keys null/empty                                        -> null
    	//   keys.All(s => s == string.Empty)  (<>c.<ModifyKeysAsRelative>b__7_0 at 0x4B4440, folded with Animate's
    	//                                       own ParseParameter lambda)                 -> null
    	//   string.Join(KeyDelimiters[0], keys.Select(<>c__DisplayClass7_0.b__1))          (String.Join<float>, 0x180D1F723)
    	// the closure (RVA 0xD1F900, captured multiplier +0x10 and actorScreenPosition +0x14):
    	//   key => key.AsInvariantFloat().GetValueOrDefault() * multiplier + actorScreenPosition * 100f    (100.0f = data_2AE2BC4)
    	// Annotated disassembly: work/title-path/disas/aae_02_ModifyKeysAsRelative.txt, aae_04_ModifyKeysAsRelative_closure.txt
    	[Token(Token = "0x60000AA")]
    	[Address(RVA = "0xD1F3C0", Offset = "0xD1DDC0", VA = "0x180D1F3C0")]
    	private string ModifyKeysAsRelative(StringParameter parameter, float multiplier, float actorScreenPosition)
    	{
    		if (!Assigned(parameter))
    		{
    			return null;
    		}
    		string[] keys = parameter.Value.Split(KeyDelimiters);
    		if (keys == null || keys.Length == 0)
    		{
    			return null;
    		}
    		if (keys.All(s => s == string.Empty))
    		{
    			return null;
    		}
    		return string.Join(KeyDelimiters[0], keys.Select(key => key.AsInvariantFloat().GetValueOrDefault() * multiplier + actorScreenPosition * 100f));
    	}

    	[Token(Token = "0x60000AB")]
    	[Address(RVA = "0xD1F760", Offset = "0xD1E160", VA = "0x180D1F760")]
    	public AnimateActorExtended()
    	{
    	}
    }
}
