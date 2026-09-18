using System.Runtime.InteropServices;
using FastEnumUtility;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000053")]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [CommandAlias("splitStageScreen")]
    public class SplitPerspectiveStageScreen : Command
    {
    	[Token(Token = "0x400010F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public IntegerParameter Index;

    	[Token(Token = "0x4000110")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("posX")]
    	public DecimalParameter ScenePositionX;

    	[Token(Token = "0x4000111")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	public DecimalParameter Roll;

    	[Token(Token = "0x4000112")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("direction")]
    	public StringParameter SplitDirection;

    	[Token(Token = "0x4000113")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private IPerspectiveStageScreenModifier _screenModifier;

    	[Token(Token = "0x17000016")]
    	private IPerspectiveStageScreenModifier ScreenModifier
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x321C20, 186 B
    		// Lazy cache in +0x50 via `ServiceLocator.Get<IPerspectiveStageManager>(true)`.
    		[Token(Token = "0x600010D")]
    		[Address(RVA = "0x321C20", Offset = "0x320620", VA = "0x180321C20")]
    		get
    		{
    			return _screenModifier ??= ServiceLocator.Get<IPerspectiveStageManager>(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (behavior) | RVA: 0x321CE0, 868 B
    	// Synchronous, not async.
    	// - `posX` is a percentage: `ScenePositionX` when assigned, else rdata
    	//   0x2AE2B94 = `00004842` = 50f, then `divss` by 0x2AE2BC4 = `0000c842` =
    	//   100f, so it becomes the normalized 0..1 split position.
    	// - `SplitDirection` is not `Assigned`-checked. Its string (null when
    	//   unassigned) goes to `FastEnum.TryParse<ScreenSplitDirection>(value,
    	//   ignoreCase: true, out direction)`, and a failed parse explicitly
    	//   re-stores 0 = `ScreenSplitDirection.Left`.
    	// - `roll` is `Roll` when assigned, else 0f.
    	// - The getter is inlined, then `sub_3E00` with ecx = 1 is
    	//   IPerspectiveStageScreenModifier slot 1 `SplitScreen(Index, posX, roll,
    	//   direction)`.
    	// - Returns an all-zero Naninovel.UniTask after a class-init check;
    	//   `UniTask.CompletedTask` is Confidence B for the spelling.
    	[Token(Token = "0x600010E")]
    	[Address(RVA = "0x321CE0", Offset = "0x3206E0", VA = "0x180321CE0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		// The float reads call `Naninovel.Nullable<float>.op_Implicit` (not
    		// `get_Value`); the up-cast selects that operator over DecimalParameter's
    		// own `float?` operator, which C# would otherwise consider ambiguous.
    		float normalizedPosX = (Assigned(ScenePositionX) ? (float)(Naninovel.Nullable<float>)ScenePositionX : 50f) / 100f;
    		if (!FastEnum.TryParse<ScreenSplitDirection>((string)SplitDirection, true, out ScreenSplitDirection direction))
    		{
    			direction = ScreenSplitDirection.Left;
    		}
    		float roll = Assigned(Roll) ? (float)(Naninovel.Nullable<float>)Roll : 0f;
    		ScreenModifier.SplitScreen(Index, normalizedPosX, roll, direction);
    		return UniTask.CompletedTask;
    	}

    	[Token(Token = "0x600010F")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public SplitPerspectiveStageScreen()
    	{
    	}
    }
}
