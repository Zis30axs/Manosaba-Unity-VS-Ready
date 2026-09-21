using System;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000027")]
    public class TweeningDropdown : TMP_Dropdown
    {
    	[Token(Token = "0x4000083")]
    	[FieldOffset(Offset = "0x180")]
    	[Header("References")]
    	[SerializeField]
    	private Transform _transform;

    	[Token(Token = "0x4000084")]
    	[FieldOffset(Offset = "0x188")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x4000085")]
    	[FieldOffset(Offset = "0x190")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _scaleUpWhenHighlighted;

    	[Token(Token = "0x4000086")]
    	[FieldOffset(Offset = "0x198")]
    	[SerializeField]
    	private ScaleUtility _scaleUtil;

    	[Token(Token = "0x4000087")]
    	[FieldOffset(Offset = "0x1A0")]
    	[SerializeField]
    	private bool _fadeOutWhenDisabled;

    	[Token(Token = "0x4000088")]
    	[FieldOffset(Offset = "0x1A8")]
    	[SerializeField]
    	private FadeUtility _fadeUtil;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCE2CE0 (shared with TweeningDropdownLocalized): new CompositeDisposable() -> +0x1B0
    	[Token(Token = "0x4000089")]
    	[FieldOffset(Offset = "0x1B0")]
    	private readonly CompositeDisposable _selectableDisposables = new CompositeDisposable();

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCE2CE0: new CompositeDisposable() -> +0x1B8
    	[Token(Token = "0x400008A")]
    	[FieldOffset(Offset = "0x1B8")]
    	private readonly CompositeDisposable _playSfxDisposables = new CompositeDisposable();

    	[Token(Token = "0x400008B")]
    	[FieldOffset(Offset = "0x1C0")]
    	private IInputManager _inputManager;

    	[Token(Token = "0x400008C")]
    	[FieldOffset(Offset = "0x1C8")]
    	private SelectableUtility _selectableUtil;

    	[Token(Token = "0x400008D")]
    	[FieldOffset(Offset = "0x1D0")]
    	private ICancelEventBlockable[] _parentBlockables;

    	[Token(Token = "0x400008E")]
    	[FieldOffset(Offset = "0x1D8")]
    	private IDisposable _cancelEventBlockerDisposable;

    	[Token(Token = "0x17000018")]
    	private IInputManager InputManager
    	{
    		[Token(Token = "0x60000A0")]
    		[Address(RVA = "0xCE1B00", Offset = "0xCE0500", VA = "0x180CE1B00")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000019")]
    	private SelectableUtility SelectableUtil
    	{
    		[Token(Token = "0x60000A1")]
    		[Address(RVA = "0xCE1BD0", Offset = "0xCE05D0", VA = "0x180CE1BD0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700001A")]
    	private ICancelEventBlockable[] ParentBlockables
    	{
    		[Token(Token = "0x60000A2")]
    		[Address(RVA = "0xCE1CA0", Offset = "0xCE06A0", VA = "0x180CE1CA0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60000A3")]
    	[Address(RVA = "0xCE1D50", Offset = "0xCE0750", VA = "0x180CE1D50", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x60000A4")]
    	[Address(RVA = "0xCE1EF0", Offset = "0xCE08F0", VA = "0x180CE1EF0", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x60000A5")]
    	[Address(RVA = "0xCE2120", Offset = "0xCE0B20", VA = "0x180CE2120")]
    	private void Initialize()
    	{
    	}

    	[Token(Token = "0x60000A6")]
    	[Address(RVA = "0xCE2290", Offset = "0xCE0C90", VA = "0x180CE2290")]
    	private void OnHighlightingChanged(bool isHighlighted)
    	{
    	}

    	[Token(Token = "0x60000A7")]
    	[Address(RVA = "0xCE22D0", Offset = "0xCE0CD0", VA = "0x180CE22D0")]
    	private void OnEnablingChanged(bool isEnabled)
    	{
    	}

    	[Token(Token = "0x60000A8")]
    	[Address(RVA = "0xCE2310", Offset = "0xCE0D10", VA = "0x180CE2310", Slot = "45")]
    	protected override GameObject CreateBlocker(Canvas rootCanvas)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000A9")]
    	[Address(RVA = "0xCE2670", Offset = "0xCE1070", VA = "0x180CE2670", Slot = "48")]
    	protected override void DestroyDropdownList(GameObject dropdownList)
    	{
    	}

    	[Token(Token = "0x60000AA")]
    	[Address(RVA = "0xCE27D0", Offset = "0xCE11D0", VA = "0x180CE27D0", Slot = "49")]
    	protected override DropdownItem CreateItem(DropdownItem itemTemplate)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000AB")]
    	[Address(RVA = "0xCE2930", Offset = "0xCE1330", VA = "0x180CE2930", Slot = "26")]
    	protected override void DoStateTransition(SelectionState state, bool instant)
    	{
    	}

    	[Token(Token = "0x60000AC")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "38")]
    	public override void Select()
    	{
    	}

    	[Token(Token = "0x60000AD")]
    	[Address(RVA = "0xCE2AC0", Offset = "0xCE14C0", VA = "0x180CE2AC0", Slot = "46")]
    	protected override void DestroyBlocker(GameObject blocker)
    	{
    	}

    	[Token(Token = "0x60000AE")]
    	[Address(RVA = "0xCE2C60", Offset = "0xCE1660", VA = "0x180CE2C60")]
    	private void OnCancelActionPerformedWhileBlockerShown(InputAction.CallbackContext _)
    	{
    	}

    	[Token(Token = "0x60000AF")]
    	[Address(RVA = "0xCE2CE0", Offset = "0xCE16E0", VA = "0x180CE2CE0")]
    	public TweeningDropdown()
    	{
    	}
    }
}
