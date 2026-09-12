using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Models;

namespace WitchTrials.Views;

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
		[Token(Token = "0x6000110")]
		[Address(RVA = "0x322050", Offset = "0x320A50", VA = "0x180322050")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000111")]
	[Address(RVA = "0x322110", Offset = "0x320B10", VA = "0x180322110", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000112")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public UnloadPerspectiveStage()
	{
	}
}
