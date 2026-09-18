using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000088")]
    public class AutoToggleUi : CustomUI
    {
    	[Token(Token = "0x4000215")]
    	[FieldOffset(Offset = "0xE0")]
    	[Header("References for Auto Toggle UI")]
    	[SerializeField]
    	private Toggle _toggle;

    	[Token(Token = "0x4000216")]
    	[FieldOffset(Offset = "0xE8")]
    	private GigaCreation.Essentials.Input.IInputManager _inputManager;

    	[Token(Token = "0x4000217")]
    	[FieldOffset(Offset = "0xF0")]
    	private IScriptPlayer _naniScriptPlayer;

    	[Token(Token = "0x1700002A")]
    	private GigaCreation.Essentials.Input.IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x336930
    		// Lazy cache in +0xE8 via `ServiceLocator.Get<IInputManager>(true)` (`cl = 1`).
    		[Token(Token = "0x60001BA")]
    		[Address(RVA = "0x336930", Offset = "0x335330", VA = "0x180336930")]
    		get
    		{
    			return _inputManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<GigaCreation.Essentials.Input.IInputManager>(true);
    		}
    	}

    	[Token(Token = "0x1700002B")]
    	private IScriptPlayer NaniScriptPlayer
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x336A00
    		// Lazy cache in +0xF0 via `Engine.GetServiceOrErr<IScriptPlayer>()`.
    		[Token(Token = "0x60001BB")]
    		[Address(RVA = "0x336A00", Offset = "0x335400", VA = "0x180336A00")]
    		get
    		{
    			return _naniScriptPlayer ??= Engine.GetServiceOrErr<IScriptPlayer>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x336AD0
    	// CustomUI.OnEnable (real call); IScriptPlayer slot 10 add_OnAutoPlay(new Action<bool>(HandleAutoModeChanged)).
    	[Token(Token = "0x60001BC")]
    	[Address(RVA = "0x336AD0", Offset = "0x3354D0", VA = "0x180336AD0", Slot = "5")]
    	protected override void OnEnable()
    	{
    		base.OnEnable();
    		NaniScriptPlayer.OnAutoPlay += HandleAutoModeChanged;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x336BA0
    	// CustomUI.OnDisable (real call); IScriptPlayer slot 11 remove_OnAutoPlay(HandleAutoModeChanged).
    	[Token(Token = "0x60001BD")]
    	[Address(RVA = "0x336BA0", Offset = "0x3355A0", VA = "0x180336BA0", Slot = "7")]
    	protected override void OnDisable()
    	{
    		base.OnDisable();
    		NaniScriptPlayer.OnAutoPlay -= HandleAutoModeChanged;
    	}

    	// RECONSTRUCTED | Confidence: A (return spelling: B) | RVA: 0x336C70, lambda <Initialize>b__9_0 0x336F80
    	// - `_toggle` (+0xE0) UniRx `OnValueChangedAsObservable()` -> Subscribe(instance lambda) -> AddTo(this). The lambda is
    	//   an instance method on this class (IScriptPlayer fetched through the getter each call), so it is a lambda, not a
    	//   method group bound to the player: `value => NaniScriptPlayer.SetAutoPlayEnabled(value)` (slot 35).
    	// - `_toggle` loaded, then IScriptPlayer slot 17 AutoPlayActive -> Toggle.Set(value, false) = inlined
    	//   SetIsOnWithoutNotify.
    	// - Returns default: `UniTask.CompletedTask` and an inlined `base.Initialize()` (CustomUI 0x3483B0) are identical.
    	[Token(Token = "0x60001BE")]
    	[Address(RVA = "0x336C70", Offset = "0x335670", VA = "0x180336C70", Slot = "83")]
    	public override UniTask Initialize()
    	{
    		_toggle.OnValueChangedAsObservable().Subscribe(value => NaniScriptPlayer.SetAutoPlayEnabled(value)).AddTo(this);
    		_toggle.SetIsOnWithoutNotify(NaniScriptPlayer.AutoPlayActive);
    		return UniTask.CompletedTask;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x336DE0
    	// Toggle.Set(enable, true) = inlined `isOn` setter; IScriptPlayer slot 34 SetSkipEnabled(false); vtable +0x2F8 = slot 28
    	// `Visible` (ScriptableUIBehaviour) gates InputManager (inlined getter) slot 0 PlayResponseSfx(1 = Submit).
    	[Token(Token = "0x60001BF")]
    	[Address(RVA = "0x336DE0", Offset = "0x3357E0", VA = "0x180336DE0")]
    	private void HandleAutoModeChanged(bool enable)
    	{
    		_toggle.isOn = enable;
    		NaniScriptPlayer.SetSkipEnabled(false);
    		if (Visible)
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    		}
    	}

    	[Token(Token = "0x60001C0")]
    	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
    	public AutoToggleUi()
    	{
    	}
    }
}
