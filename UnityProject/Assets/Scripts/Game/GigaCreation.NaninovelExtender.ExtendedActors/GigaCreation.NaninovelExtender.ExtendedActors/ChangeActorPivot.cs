using System.Linq;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000037")]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [CommandAlias("pivot")]
    public class ChangeActorPivot : Command
    {
    	[Token(Token = "0x40000B6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	[ActorContext("*", -1, null)]
    	public StringListParameter ActorIds;

    	[Token(Token = "0x40000B7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("point")]
    	[RequiredParameter]
    	public DecimalListParameter PivotPoint;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1FF60, 1117 B (`@pivot Ids point:x,y`)
    	// The twin of ChangeActorMaskability.Execute: the actor of each id in ActorIds [+0x30] `as IPivotChangeableActor` gets
    	//   ChangePivot(PivotPoint[0], PivotPoint[1]) (Enumerable.ElementAtOrDefault 0x180685F60 on PivotPoint [+0x38], NullableFloat.
    	//   op_Implicit 0x180526C50 for each coordinate; an unspecified coordinate stays null).
    	// Annotated disassembly: work/title-path/disas/cap_01_Execute.txt
    	[Token(Token = "0x60000B7")]
    	[Address(RVA = "0xD1FF60", Offset = "0xD1E960", VA = "0x180D1FF60", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken asyncToken)
    	{
    		foreach (NullableString actorId in ActorIds)
    		{
    			string id = actorId;
    			IActorManager manager = Engine.FindService<IActorManager, string>(id, (IActorManager actorManager, string arg) => actorManager.ActorExists(arg));
    			IActor actor = null;
    			// 0x180D2027E: the shipped binary tests the FindService result before GetActor (the source's `?.`)
    			if (manager != null)
    			{
    				actor = manager.GetActor(actorId);
    			}
    			if (actor is IPivotChangeableActor pivotable)
    			{
    				pivotable.ChangePivot(PivotPoint.ElementAtOrDefault(0), PivotPoint.ElementAtOrDefault(1));
    			}
    		}
    		return UniTask.CompletedTask;
    	}

    	[Token(Token = "0x60000B8")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public ChangeActorPivot()
    	{
    	}
    }
}
