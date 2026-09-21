using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000035")]
    [UsedImplicitly]
    [CommandAlias("mask")]
    public class ChangeActorMaskability : Command
    {
    	[Token(Token = "0x40000B2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	[ActorContext("*", -1, null)]
    	public StringListParameter ActorIds;

    	[Token(Token = "0x40000B3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[UsedImplicitly]
    	[ParameterDefaultValue("true")]
    	public BooleanParameter Enable;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1FA30, 1067 B (`@mask Ids enable:`)
    	// For every id of ActorIds [+0x30]: the manager that has the actor -- Engine.FindService<IActorManager, string>(id,
    	//   <>c.<Execute>b__2_0 = (manager, actorId) => manager.ActorExists(actorId), IActorManager slot 6; 0xD1FF00) (0x1806821B0) --
    	//   its GetActor(id) (IActorManager slot 7, 0x180D1FD77) `as IMaskabilityChangeableActor` (0x180298020) gets
    	//   ChangeMaskability(Enable [+0x38]) (IMaskabilityChangeableActor interface slot 2, 0x180D1FDC6); nothing is awaited.
    	// Annotated disassembly: work/title-path/disas/cam_01_Execute, cam_02_lambda.txt
    	[Token(Token = "0x60000B2")]
    	[Address(RVA = "0xD1FA30", Offset = "0xD1E430", VA = "0x180D1FA30", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken asyncToken)
    	{
    		foreach (NullableString actorId in ActorIds)
    		{
    			string id = actorId;
    			IActorManager manager = Engine.FindService<IActorManager, string>(id, (IActorManager actorManager, string arg) => actorManager.ActorExists(arg));
    			IActor actor = null;
    			// 0x180D1FD56: the shipped binary tests the FindService result before GetActor (the source's `?.`)
    			if (manager != null)
    			{
    				actor = manager.GetActor(actorId);
    			}
    			if (actor is IMaskabilityChangeableActor maskable)
    			{
    				maskable.ChangeMaskability(((bool?)Enable).GetValueOrDefault());
    			}
    		}
    		return UniTask.CompletedTask;
    	}

    	[Token(Token = "0x60000B3")]
    	[Address(RVA = "0x325690", Offset = "0x324090", VA = "0x180325690")]
    	public ChangeActorMaskability()
    	{
    	}
    }
}
