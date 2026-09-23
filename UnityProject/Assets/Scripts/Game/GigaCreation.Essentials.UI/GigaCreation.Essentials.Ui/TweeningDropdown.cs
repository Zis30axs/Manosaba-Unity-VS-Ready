using UnityEngine.EventSystems;
using UniRx.Triggers;
using GigaCreation.Tools.Service;
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE1B00 -- lazy: _inputManager (+0x1C0), else ServiceLocator.Get<IInputManager>(true)
    		// Annotated disassembly: work/title-path/disas/tdd_0xCE1B00.txt
    		[Token(Token = "0x60000A0")]
    		[Address(RVA = "0xCE1B00", Offset = "0xCE0500", VA = "0x180CE1B00")]
    		get
    		{
    			if (_inputManager == null)
    			{
    				_inputManager = ServiceLocator.Get<IInputManager>(true);
    			}
    			return _inputManager;
    		}
    	}

    	[Token(Token = "0x17000019")]
    	private SelectableUtility SelectableUtil
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE1BD0 -- lazy: _selectableUtil (+0x1C8), else new SelectableUtility(gameObject)
    		// Annotated disassembly: work/title-path/disas/tdd_0xCE1BD0.txt
    		[Token(Token = "0x60000A1")]
    		[Address(RVA = "0xCE1BD0", Offset = "0xCE05D0", VA = "0x180CE1BD0")]
    		get
    		{
    			if (_selectableUtil == null)
    			{
    				_selectableUtil = new SelectableUtility(base.gameObject);
    			}
    			return _selectableUtil;
    		}
    	}

    	[Token(Token = "0x1700001A")]
    	private ICancelEventBlockable[] ParentBlockables
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE1CA0 -- lazy: _parentBlockables (+0x1D0), else
    		//   GetComponentsInParent<ICancelEventBlockable>()
    		// Annotated disassembly: work/title-path/disas/tdd_0xCE1CA0.txt
    		[Token(Token = "0x60000A2")]
    		[Address(RVA = "0xCE1CA0", Offset = "0xCE06A0", VA = "0x180CE1CA0")]
    		get
    		{
    			if (_parentBlockables == null)
    			{
    				_parentBlockables = GetComponentsInParent<ICancelEventBlockable>();
    			}
    			return _parentBlockables;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE1D50, 404 B -- Selectable.OnEnable, then when SelectableUtil.IsActive
    	//   Initialize() (inlined)
    	// Annotated disassembly: work/title-path/disas/tdd_0xCE1D50.txt
    	[Token(Token = "0x60000A3")]
    	[Address(RVA = "0xCE1D50", Offset = "0xCE0750", VA = "0x180CE1D50", Slot = "5")]
    	protected override void OnEnable()
    	{
    		base.OnEnable();
    		if (SelectableUtil.IsActive)
    		{
    			Initialize();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE1EF0, 558 B -- TMP_Dropdown.OnDisable inlined (ImmediateDestroyDropdownList,
    	//   DestroyBlocker(m_Blocker) when alive, m_Blocker = null, Selectable.OnDisable); _selectableDisposables (+0x1B0)
    	//   and _playSfxDisposables (+0x1B8) cleared; a live _cancelEventBlockerDisposable (+0x1D8, 0x180CE2098) disposed
    	//   and cleared
    	// Annotated disassembly: work/title-path/disas/tdd_0xCE1EF0.txt
    	[Token(Token = "0x60000A4")]
    	[Address(RVA = "0xCE1EF0", Offset = "0xCE08F0", VA = "0x180CE1EF0", Slot = "7")]
    	protected override void OnDisable()
    	{
    		base.OnDisable();
    		_selectableDisposables.Clear();
    		_playSfxDisposables.Clear();
    		// 0x180CE2098: the shipped code tests the blocker subscription before disposing it
    		if (_cancelEventBlockerDisposable != null)
    		{
    			_cancelEventBlockerDisposable.Dispose();
    			_cancelEventBlockerDisposable = null;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE2120, 361 B -- SelectableUtil's highlighting / enabling observables
    	//   subscribed with OnHighlightingChanged / OnEnablingChanged, each AddTo(_selectableDisposables, this)
    	// Annotated disassembly: work/title-path/disas/tdd_0xCE2120.txt
    	[Token(Token = "0x60000A5")]
    	[Address(RVA = "0xCE2120", Offset = "0xCE0B20", VA = "0x180CE2120")]
    	private void Initialize()
    	{
    		SelectableUtil.OnHighlightingChangedAsObservable().Subscribe(OnHighlightingChanged).AddTo(_selectableDisposables, this);
    		SelectableUtil.OnEnablingChangedAsObservable().Subscribe(OnEnablingChanged).AddTo(_selectableDisposables, this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE2290, 61 B -- _scaleUpWhenHighlighted (+0x190) -> tail jump
    	//   _scaleUtil (+0x198).Scale(_transform (+0x180), isHighlighted)
    	// Annotated disassembly: work/title-path/disas/tdd_0xCE2290.txt
    	[Token(Token = "0x60000A6")]
    	[Address(RVA = "0xCE2290", Offset = "0xCE0C90", VA = "0x180CE2290")]
    	private void OnHighlightingChanged(bool isHighlighted)
    	{
    		if (_scaleUpWhenHighlighted)
    		{
    			_scaleUtil.Scale(_transform, isHighlighted);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE22D0, 61 B -- _fadeOutWhenDisabled (+0x1A0) -> tail jump
    	//   _fadeUtil (+0x1A8).Fade(_canvasGroup (+0x188), isEnabled)
    	// Annotated disassembly: work/title-path/disas/tdd_0xCE22D0.txt
    	[Token(Token = "0x60000A7")]
    	[Address(RVA = "0xCE22D0", Offset = "0xCE0CD0", VA = "0x180CE22D0")]
    	private void OnEnablingChanged(bool isEnabled)
    	{
    		if (_fadeOutWhenDisabled)
    		{
    			_fadeUtil.Fade(_canvasGroup, isEnabled);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE2310, 850 B / b__23_0 0xCE2E40 -- _cancelEventBlockerDisposable = new
    	//   CancelEventBlocker(ParentBlockables (inlined)); _canvasGroup.blocksRaycasts = false; the base blocker's
    	//   Button.onClick (+0x100).AsObservable() subscribed with b__23_0 (InputManager.PlayResponseSfx(Cancel), then
    	//   _playSfxDisposables.Clear()) AddTo(_playSfxDisposables, blocker) (GameObject overload 0x67FE60);
    	//   InputManager.GetAction("Cancel").performed += OnCancelActionPerformedWhileBlockerShown (CallbackArray.AddCallback)
    	// Annotated disassembly: work/title-path/disas/tdd_0xCE2310.txt, tdd_0xCE2E40.txt
    	[Token(Token = "0x60000A8")]
    	[Address(RVA = "0xCE2310", Offset = "0xCE0D10", VA = "0x180CE2310", Slot = "45")]
    	protected override GameObject CreateBlocker(Canvas rootCanvas)
    	{
    		_cancelEventBlockerDisposable = new CancelEventBlocker(ParentBlockables);
    		_canvasGroup.blocksRaycasts = false;
    		GameObject gameObject = base.CreateBlocker(rootCanvas);
    		gameObject.GetComponent<Button>().onClick.AsObservable().Subscribe(delegate
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Cancel);
    			_playSfxDisposables.Clear();
    		}).AddTo(_playSfxDisposables, gameObject);
    		InputManager.GetAction("Cancel").performed += OnCancelActionPerformedWhileBlockerShown;
    		return gameObject;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE2670, 342 B -- TMP_Dropdown.DestroyDropdownList inlined (Object.Destroy);
    	//   _canvasGroup.blocksRaycasts = true; _playSfxDisposables.Clear(); _cancelEventBlockerDisposable disposed (no
    	//   null test: 0x180CE2756 throws) and cleared
    	// Annotated disassembly: work/title-path/disas/tdd_0xCE2670.txt
    	[Token(Token = "0x60000A9")]
    	[Address(RVA = "0xCE2670", Offset = "0xCE1070", VA = "0x180CE2670", Slot = "48")]
    	protected override void DestroyDropdownList(GameObject dropdownList)
    	{
    		base.DestroyDropdownList(dropdownList);
    		_canvasGroup.blocksRaycasts = true;
    		_playSfxDisposables.Clear();
    		_cancelEventBlockerDisposable.Dispose();
    		_cancelEventBlockerDisposable = null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE27D0, 340 B / b__25_0 0xCE2EB0 -- TMP_Dropdown.CreateItem inlined
    	//   (Instantiate); the item's toggle (+0x38).OnPointerClickAsObservable() subscribed with b__25_0
    	//   (InputManager.PlayResponseSfx(Submit), then _playSfxDisposables.Clear()) AddTo(_playSfxDisposables, item)
    	// Annotated disassembly: work/title-path/disas/tdd_0xCE27D0.txt, tdd_0xCE2EB0.txt
    	[Token(Token = "0x60000AA")]
    	[Address(RVA = "0xCE27D0", Offset = "0xCE11D0", VA = "0x180CE27D0", Slot = "49")]
    	protected override DropdownItem CreateItem(DropdownItem itemTemplate)
    	{
    		DropdownItem dropdownItem = base.CreateItem(itemTemplate);
    		dropdownItem.toggle.OnPointerClickAsObservable().Subscribe(delegate
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			_playSfxDisposables.Clear();
    		}).AddTo(_playSfxDisposables, dropdownItem);
    		return dropdownItem;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE2930, 385 B -- as TweeningToggle.DoStateTransition: nothing while
    	//   !SelectableUtil.IsActive; states 0..4 to the matching SelectableUtility transition; anything else
    	//   ArgumentOutOfRangeException("state", boxed state, null)
    	// Annotated disassembly: work/title-path/disas/tdd_0xCE2930.txt
    	[Token(Token = "0x60000AB")]
    	[Address(RVA = "0xCE2930", Offset = "0xCE1330", VA = "0x180CE2930", Slot = "26")]
    	protected override void DoStateTransition(SelectionState state, bool instant)
    	{
    		if (!SelectableUtil.IsActive)
    		{
    			return;
    		}
    		switch (state)
    		{
    		case SelectionState.Normal:
    			SelectableUtil.OnStateTransitionToNormal();
    			break;
    		case SelectionState.Highlighted:
    			SelectableUtil.OnStateTransitionToHighlighted();
    			break;
    		case SelectionState.Pressed:
    			SelectableUtil.OnStateTransitionToPressed();
    			break;
    		case SelectionState.Selected:
    			SelectableUtil.OnStateTransitionToSelected();
    			break;
    		case SelectionState.Disabled:
    			SelectableUtil.OnStateTransitionToDisabled();
    			break;
    		default:
    			throw new ArgumentOutOfRangeException("state", state, null);
    		}
    	}

    	[Token(Token = "0x60000AC")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "38")]
    	public override void Select()
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE2AC0, 403 B -- InputManager.GetAction("Cancel").performed -=
    	//   OnCancelActionPerformedWhileBlockerShown (CallbackArray.RemoveCallback); then TMP_Dropdown.DestroyBlocker
    	//   inlined (tail jump Object.Destroy)
    	// Annotated disassembly: work/title-path/disas/tdd_0xCE2AC0.txt
    	[Token(Token = "0x60000AD")]
    	[Address(RVA = "0xCE2AC0", Offset = "0xCE14C0", VA = "0x180CE2AC0", Slot = "46")]
    	protected override void DestroyBlocker(GameObject blocker)
    	{
    		InputManager.GetAction("Cancel").performed -= OnCancelActionPerformedWhileBlockerShown;
    		base.DestroyBlocker(blocker);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE2C60, 116 B -- InputManager.PlayResponseSfx(Cancel) (slot 0, r9 = 0);
    	//   _playSfxDisposables.Clear(); tail jump TMP_Dropdown.Hide()
    	// Annotated disassembly: work/title-path/disas/tdd_0xCE2C60.txt
    	[Token(Token = "0x60000AE")]
    	[Address(RVA = "0xCE2C60", Offset = "0xCE1660", VA = "0x180CE2C60")]
    	private void OnCancelActionPerformedWhileBlockerShown(InputAction.CallbackContext _)
    	{
    		InputManager.PlayResponseSfx(ResponseKind.Cancel);
    		_playSfxDisposables.Clear();
    		Hide();
    	}

    	[Token(Token = "0x60000AF")]
    	[Address(RVA = "0xCE2CE0", Offset = "0xCE16E0", VA = "0x180CE2CE0")]
    	public TweeningDropdown()
    	{
    	}
    }
}
