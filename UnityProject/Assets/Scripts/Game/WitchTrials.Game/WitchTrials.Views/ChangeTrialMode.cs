using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x200005D")]
    [UsedImplicitly]
    [CommandAlias("trialMode")]
    public class ChangeTrialMode : Command
    {
    	[Token(Token = "0x400013A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public BooleanParameter Enable;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x325460, 266 B
    	// ServiceLocator.Get<IWitchTrialsNaninovelManager>(cl = 0); slot 2 set_IsPresentEvidenceMode(false); then
    	// Naninovel.Nullable<bool> implicit operator on `Enable` (+0x30) into slot 4 ChangeTrialMode. Returns default (completed).
    	[Token(Token = "0x6000133")]
    	[Address(RVA = "0x325460", Offset = "0x323E60", VA = "0x180325460", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		IWitchTrialsNaninovelManager manager = GigaCreation.Tools.Service.ServiceLocator.Get<IWitchTrialsNaninovelManager>(false);
    		manager.IsPresentEvidenceMode = false;
    		manager.ChangeTrialMode(Enable);
    		return UniTask.CompletedTask;
    	}

    	[Token(Token = "0x6000134")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public ChangeTrialMode()
    	{
    	}
    }
}
