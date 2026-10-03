using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Input;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using TMPro;
using UniRx;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000036")]
    public class Pagination : MonoBehaviour
    {
    	[Token(Token = "0x40000C2")]
    	[FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private MovingArrow _previousPageButton;

    	[Token(Token = "0x40000C3")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private MovingArrow _nextPageButton;

    	[Token(Token = "0x40000C4")]
    	[FieldOffset(Offset = "0x30")]
    	[Space]
    	[SerializeField]
    	private PaginationIndicator[] _indicators;

    	[Token(Token = "0x40000C5")]
    	[FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private DotIndicator _originalDot;

    	[Token(Token = "0x40000C6")]
    	[FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private TextMeshProUGUI _currentPageLabel;

    	[Token(Token = "0x40000C7")]
    	[FieldOffset(Offset = "0x48")]
    	[SerializeField]
    	private TextMeshProUGUI _totalPageLabel;

    	[Token(Token = "0x40000C8")]
    	[FieldOffset(Offset = "0x50")]
    	[Header("Parameters")]
    	[SerializeField]
    	private PaginationIndicatorStyle _indicatorStyle;

    	[Token(Token = "0x40000C9")]
    	[FieldOffset(Offset = "0x54")]
    	[SerializeField]
    	private bool _wrapped;

    	[Token(Token = "0x40000CA")]
    	[FieldOffset(Offset = "0x58")]
    	private readonly ReactiveProperty<int> _currentPageIndex = new ReactiveProperty<int>();

    	[Token(Token = "0x40000CB")]
    	[FieldOffset(Offset = "0x60")]
    	private readonly CompositeDisposable _disposables = new CompositeDisposable();

    	[Token(Token = "0x40000CC")]
    	[FieldOffset(Offset = "0x68")]
    	private readonly List<DotIndicator> _dots = new List<DotIndicator>();

    	[Token(Token = "0x40000CD")]
    	[FieldOffset(Offset = "0x70")]
    	private IInputManager _inputManager;

    	[Token(Token = "0x1700002F")]
    	public int CurrentPageIndex
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE5BF0 -- [this+0x58] ReactiveProperty<int>, tail jump get_Value
    		[Token(Token = "0x60000EF")]
    		[Address(RVA = "0xCE5BF0", Offset = "0xCE45F0", VA = "0x180CE5BF0")]
    		get
    		{
    			return _currentPageIndex.Value;
    		}
    	}

    	[Token(Token = "0x17000030")]
    	private IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE5C40 -- lazy: [this+0x70] tested, else
    		//   ServiceLocator.Get<IInputManager>(cl = 1) stored back
    		[Token(Token = "0x60000F0")]
    		[Address(RVA = "0xCE5C40", Offset = "0xCE4640", VA = "0x180CE5C40")]
    		get
    		{
    			if (_inputManager == null)
    			{
    				_inputManager = ServiceLocator.Get<IInputManager>(true);
    			}
    			return _inputManager;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (call vs. inlined copy) | RVA: 0xCE5D00 -- identical-code folded with
    	//   ChangeIndicatorStyle: the whole body is that method inlined
    	[Token(Token = "0x60000F1")]
    	[Address(RVA = "0xCE5D00", Offset = "0xCE4700", VA = "0x180CE5D00")]
    	private void Awake()
    	{
    		ChangeIndicatorStyle();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE5D00 -- over _indicators (+0x30): alpha of the indicator's CanvasGroup
    	//   (+0x18) is 1.0 (data 0x2AE2820) when its style (+0x10) is _indicatorStyle (+0x50), else 0
    	// Annotated disassembly: work/title-path/disas/pag_03_0xCE5D00.txt
    	[Token(Token = "0x60000F2")]
    	[Address(RVA = "0xCE5D00", Offset = "0xCE4700", VA = "0x180CE5D00")]
    	private void ChangeIndicatorStyle()
    	{
    		PaginationIndicator[] indicators = _indicators;
    		foreach (PaginationIndicator indicator in indicators)
    		{
    			indicator.CanvasGroup.alpha = ((indicator.Style == _indicatorStyle) ? 1f : 0f);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE5DA0; closures <>c__DisplayClass18_0 (<>4__this, numOfTotalPage)
    	// - _disposables (+0x60) Clear; MovingArrow.Activate on +0x20 / +0x28; `resetCurrentPage` -> _currentPageIndex = 0.
    	// - <Present>b__0 (0xCE67B0) on _currentPageIndex, AddTo(_disposables, this) (0x67FCB0): unless _wrapped (+0x54) the
    	//   arrows are shown (MovingArrow.Present inlined) only when a previous / next page exists; Dot style -> dot
    	//   sprites, Number style -> _currentPageLabel (+0x40) SetText(string.Format("{0}", pageIndex + 1)).
    	// - <Present>b__1 / b__2 (0xCE6A10 / 0xCE6B10) on the arrows' OnClickAsObservable (inlined): IInputManager slot 0
    	//   PlayResponseSfx(2 = Move), then step the page, modulo numOfTotalPage when _wrapped, clamped otherwise.
    	// - Dot style (1): _originalDot (+0x38) deactivated; dots already built (count read once) are activated, the rest
    	//   instantiated from _originalDot under its parent, activated and added; then the current page's sprite.
    	// - Number style (2): _totalPageLabel (+0x48) SetText(numOfTotalPage.ToString()). Two separate style tests.
    	// Annotated disassembly: work/title-path/disas/pag_04_0xCE5DA0.txt
    	// Annotated disassembly: work/title-path/disas/pag_09_Present_b0.txt
    	// Annotated disassembly: work/title-path/disas/pag_10_Present_b1.txt
    	// Annotated disassembly: work/title-path/disas/pag_11_Present_b2.txt
    	[Token(Token = "0x60000F3")]
    	[Address(RVA = "0xCE5DA0", Offset = "0xCE47A0", VA = "0x180CE5DA0")]
    	public void Present(int numOfTotalPage, bool resetCurrentPage = true)
    	{
    		_disposables.Clear();
    		_previousPageButton.Activate();
    		_nextPageButton.Activate();
    		if (resetCurrentPage)
    		{
    			_currentPageIndex.Value = 0;
    		}
    		_currentPageIndex.Subscribe(delegate(int pageIndex)
    		{
    			if (!_wrapped)
    			{
    				if (pageIndex > 0)
    				{
    					_previousPageButton.Present();
    				}
    				else
    				{
    					_previousPageButton.Dismiss();
    				}
    				if (pageIndex < numOfTotalPage - 1)
    				{
    					_nextPageButton.Present();
    				}
    				else
    				{
    					_nextPageButton.Dismiss();
    				}
    			}
    			if (_indicatorStyle == PaginationIndicatorStyle.Dot)
    			{
    				ModifySpriteInDotsEnabledTo(pageIndex);
    			}
    			if (_indicatorStyle == PaginationIndicatorStyle.Number)
    			{
    				_currentPageLabel.SetText(string.Format("{0}", pageIndex + 1));
    			}
    		}).AddTo(_disposables, this);
    		_previousPageButton.OnClickAsObservable().Subscribe(delegate
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Move);
    			_currentPageIndex.Value = (_wrapped ? ((_currentPageIndex.Value + numOfTotalPage - 1) % numOfTotalPage) : Mathf.Max(_currentPageIndex.Value - 1, 0));
    		}).AddTo(_disposables, this);
    		_nextPageButton.OnClickAsObservable().Subscribe(delegate
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Move);
    			_currentPageIndex.Value = (_wrapped ? ((_currentPageIndex.Value + 1) % numOfTotalPage) : Mathf.Min(_currentPageIndex.Value + 1, numOfTotalPage - 1));
    		}).AddTo(_disposables, this);
    		if (_indicatorStyle == PaginationIndicatorStyle.Dot)
    		{
    			_originalDot.Deactivate();
    			int numOfDots = _dots.Count;
    			for (int i = 0; i < numOfTotalPage; i++)
    			{
    				if (i < numOfDots)
    				{
    					_dots[i].Activate();
    					continue;
    				}
    				GameObject dotObject = UnityEngine.Object.Instantiate(_originalDot.gameObject, _originalDot.transform.parent);
    				dotObject.SetActive(true);
    				_dots.Add(dotObject.GetComponent<DotIndicator>());
    			}
    			ModifySpriteInDotsEnabledTo(_currentPageIndex.Value);
    		}
    		if (_indicatorStyle == PaginationIndicatorStyle.Number)
    		{
    			_totalPageLabel.SetText(numOfTotalPage.ToString());
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE62F0 -- over _dots (+0x68, Count re-read each pass): DotIndicator.ChangeSpriteTo
    	//   inlined (overrideSprite = _enable at +0x28 for enableIndex, _disable at +0x30 otherwise)
    	[Token(Token = "0x60000F4")]
    	[Address(RVA = "0xCE62F0", Offset = "0xCE4CF0", VA = "0x180CE62F0")]
    	private void ModifySpriteInDotsEnabledTo(int enableIndex)
    	{
    		for (int i = 0; i < _dots.Count; i++)
    		{
    			_dots[i].ChangeSpriteTo(i == enableIndex);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE63D0 -- _disposables.Clear, MovingArrow.Deactivate on both arrows,
    	//   List<DotIndicator>.Enumerator over _dots with DotIndicator.Deactivate inlined (gameObject.SetActive(false))
    	// Annotated disassembly: work/title-path/disas/pag_06_0xCE63D0.txt
    	[Token(Token = "0x60000F5")]
    	[Address(RVA = "0xCE63D0", Offset = "0xCE4DD0", VA = "0x180CE63D0")]
    	public void Dismiss()
    	{
    		_disposables.Clear();
    		_previousPageButton.Deactivate();
    		_nextPageButton.Deactivate();
    		foreach (DotIndicator dot in _dots)
    		{
    			dot.Deactivate();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE6530 -- tail jump UniRx Observable.AsObservable<int>([this+0x58])
    	[Token(Token = "0x60000F6")]
    	[Address(RVA = "0xCE6530", Offset = "0xCE4F30", VA = "0x180CE6530")]
    	public IObservable<int> OnPageChangedAsObservable()
    	{
    		return _currentPageIndex.AsObservable();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE65A0 -- only the readonly field initializers: new ReactiveProperty<int>()
    	// -> +0x58, new CompositeDisposable() -> +0x60, new List<DotIndicator>() -> +0x68; then MonoBehaviour..ctor
    	[Token(Token = "0x60000F7")]
    	[Address(RVA = "0xCE65A0", Offset = "0xCE4FA0", VA = "0x180CE65A0")]
    	public Pagination()
    	{
    	}
    }
}
