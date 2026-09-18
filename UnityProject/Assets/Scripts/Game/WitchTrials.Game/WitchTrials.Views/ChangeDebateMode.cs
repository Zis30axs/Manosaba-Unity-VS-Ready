using System.Runtime.InteropServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x200005C")]
    [UsedImplicitly]
    [CommandAlias("debate")]
    public class ChangeDebateMode : Command
    {
    	[Token(Token = "0x4000139")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public BooleanParameter Enable;

    	// RECONSTRUCTED | Confidence: A (call shape), B (nullable-cast chain,
    	// same as ChangeCourtFireFxActivity) | RVA: 0x325360
    	// `IWitchTrialsNaninovelManager.ChangeDebateMode` is dump.cs Slot 6.
    	// No lazy field cache here (unlike DebateUi/ChangeCourtFireFxActivity) --
    	// this command re-resolves the service fresh via ServiceLocator on every
    	// Execute rather than caching it in a field.
    	[Token(Token = "0x6000131")]
    	[Address(RVA = "0x325360", Offset = "0x323D60", VA = "0x180325360", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		ServiceLocator.Get<IWitchTrialsNaninovelManager>().ChangeDebateMode(((bool?)Enable).Value);
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000132")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public ChangeDebateMode()
    	{
    	}
    }
}
