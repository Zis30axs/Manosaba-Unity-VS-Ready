using GigaCreation.NaninovelExtender.Core;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using WitchTrials.Models;

namespace WitchTrials.Presenters
{
    [Token(Token = "0x2000150")]
    public class WitchTrialsNaninovelPresenter : NaninovelPresenter
    {
    	[Token(Token = "0x4000671")]
    	[FieldOffset(Offset = "0x28")]
    	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38EE30
    	// Creates the concrete manager and registers it under its interface. No
    	// call to base.Awake() is visible in the disassembly (goes straight from
    	// the class-init guards into `new WitchTrialsNaninovelManager()`).
    	[Token(Token = "0x6000594")]
    	[Address(RVA = "0x38EE30", Offset = "0x38D830", VA = "0x18038EE30", Slot = "4")]
    	protected override void Awake()
    	{
    		_witchTrialsNaninovelManager = new WitchTrialsNaninovelManager();
    		ServiceLocator.Register(_witchTrialsNaninovelManager);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38EF20
    	// Mirror of Awake(): unregisters the same instance. No base.OnDestroy() call
    	// visible.
    	[Token(Token = "0x6000595")]
    	[Address(RVA = "0x38EF20", Offset = "0x38D920", VA = "0x18038EF20", Slot = "5")]
    	protected override void OnDestroy()
    	{
    		ServiceLocator.Unregister(_witchTrialsNaninovelManager);
    	}

    	[Token(Token = "0x6000596")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public WitchTrialsNaninovelPresenter()
    	{
    	}
    }
}
