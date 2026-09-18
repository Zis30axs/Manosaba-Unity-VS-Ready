using System.Runtime.InteropServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000054")]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [CommandAlias("unloadStage")]
    public class UnloadPerspectiveStage : Command
    {
    	[Token(Token = "0x4000114")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private IPerspectiveStageManager _perspectiveStageManager;

    	[Token(Token = "0x17000017")]
    	private IPerspectiveStageManager PerspectiveStageManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x322050, 186 B
    		// Lazy cache in +0x30 via `ServiceLocator.Get<IPerspectiveStageManager>()`
    		// with the default `includeDerivedType` (`xor ecx, ecx`), as in
    		// `LoadPerspectiveStage`.
    		[Token(Token = "0x6000110")]
    		[Address(RVA = "0x322050", Offset = "0x320A50", VA = "0x180322050")]
    		get
    		{
    			return _perspectiveStageManager ??= ServiceLocator.Get<IPerspectiveStageManager>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (behavior) | RVA: 0x322110, 282 B
    	// The getter is inlined, then an interface call through `sub_22E0` with
    	// ecx = 1 on the IPerspectiveStageLoader TypeInfo: slot 1 `UnloadStage()`.
    	// The method then returns an all-zero `Naninovel.UniTask` after a class-init
    	// check on `Naninovel.UniTask`, i.e. it completes synchronously and ignores
    	// `token`. Writing that as `UniTask.CompletedTask` is Confidence B.
    	[Token(Token = "0x6000111")]
    	[Address(RVA = "0x322110", Offset = "0x320B10", VA = "0x180322110", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		PerspectiveStageManager.UnloadStage();
    		return UniTask.CompletedTask;
    	}

    	[Token(Token = "0x6000112")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public UnloadPerspectiveStage()
    	{
    	}
    }
}
