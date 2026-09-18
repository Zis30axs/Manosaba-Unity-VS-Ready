using GigaCreation.Essentials.Input;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Presenters
{
    [Token(Token = "0x200014F")]
    public class WitchTrialsInputPresenter : MonoBehaviour
    {
    	[Token(Token = "0x400066F")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private string _naninovelActionMapName;

    	[Token(Token = "0x4000670")]
    	[FieldOffset(Offset = "0x28")]
    	private IInputManager _inputManager;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38EC50, 368 B
    	//
    	// Native evidence (log:
    	// tools\unity-rebuild\out\logs\witchtrialsinputpresenter_awake_0x38EC50.txt):
    	// allocates a WitchTrialsInputManager, calls the *base*
    	// InputManager<WitchTrialsActions> parameterless ctor, then writes
    	// `_naninovelActionMapName` directly at +0x20 -- exactly what
    	// WitchTrialsInputManager's own one-arg constructor does (RVA 0x397C20,
    	// confirmed by disassembling it separately: identical base-ctor-call +
    	// single-field-store shape, and both share the *same* class-init guard
    	// flag address, data_30D9504 -- proof this is that constructor inlined at
    	// the call site, not independently-written duplicate logic). So the
    	// source is simply `new WitchTrialsInputManager(_naninovelActionMapName)`.
    	// The registration is proven by METHODINFO
    	// `ServiceLocator.Register<IInputManager>()` (0x38EDA2) + a tail `jmp` to
    	// `ServiceLocator$$Register<object>`: this is the IInputManager service
    	// registration the boot chain's `ServiceLocator.Get<IInputManager>(true)`
    	// depends on.
    	[Token(Token = "0x6000591")]
    	[Address(RVA = "0x38EC50", Offset = "0x38D650", VA = "0x18038EC50")]
    	private void Awake()
    	{
    		_inputManager = new WitchTrialsInputManager(_naninovelActionMapName);
    		ServiceLocator.Register(_inputManager);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38EDC0, 104 B (mirror of Awake())
    	// Tail `jmp` to `ServiceLocator$$Unregister<object>` after loading
    	// METHODINFO `ServiceLocator.Unregister<IInputManager>()` (0x38EE14) and
    	// `_inputManager` (+0x28).
    	// (log: tools\unity-rebuild\out\logs\witchtrialsinputpresenter_ondestroy_0x38EDC0.txt)
    	[Token(Token = "0x6000592")]
    	[Address(RVA = "0x38EDC0", Offset = "0x38D7C0", VA = "0x18038EDC0")]
    	private void OnDestroy()
    	{
    		ServiceLocator.Unregister(_inputManager);
    	}

    	[Token(Token = "0x6000593")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public WitchTrialsInputPresenter()
    	{
    	}
    }
}
