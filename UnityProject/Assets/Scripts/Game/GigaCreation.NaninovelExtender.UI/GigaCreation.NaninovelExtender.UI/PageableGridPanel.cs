using System;
using GigaCreation.Essentials.Ui;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using Naninovel;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x2000010")]
    public class PageableGridPanel<TGrid, TSlot> : MonoBehaviour where TGrid : ScriptableGrid<TSlot> where TSlot : ScriptableGridSlot
    {
    	[Token(Token = "0x4000034")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x4000035")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private TGrid _grid;

    	[Token(Token = "0x4000036")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private Pagination _pagination;

    	[Token(Token = "0x4000037")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private UnityEvent _prePageChanged;

    	[Token(Token = "0x4000038")]
    	[FieldOffset(Offset = "0x0")]
    	private IDisposable _paginationDisposable;

    	[Token(Token = "0x17000007")]
    	public TGrid Grid
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38F060 (shared PageableGridPanel<object, object> body) -- mov rax, [rcx+0x28]
    		[Token(Token = "0x600003A")]
    		get
    		{
    			return _grid;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x89DB10 (shared) -- GetComponent<CanvasGroup>() -> +0x20,
    	//   GetComponentInChildren<TGrid>() (RGCTX) -> +0x28, GetComponentInChildren<Pagination>() -> +0x30
    	// Annotated disassembly: work/title-path/disas/pgp_02_0x89DB10.txt
    	[Token(Token = "0x600003B")]
    	private void Reset()
    	{
    		_canvasGroup = GetComponent<CanvasGroup>();
    		_grid = GetComponentInChildren<TGrid>();
    		_pagination = GetComponentInChildren<Pagination>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x89DCB0 (shared)
    	// - The previous page subscription (+0x40) is disposed behind the shipped null test (0x18089DD3F).
    	// - ChangePage(_pagination.CurrentPageIndex) inlined: [pagination+0x58].Value, _prePageChanged (+0x38) invoked
    	//   behind its null test, grid vtable +0x598 = ScriptableGrid slot 70 SelectPage(index + 1).
    	// - _canvasGroup (+0x20) read once: CanvasGroupExtensions.Present inlined (alpha 1, interactable, blocksRaycasts).
    	// - Pagination.Present(grid vtable +0x528 = slot 63 PageCount, resetCurrentPage: false (r8 = 0)).
    	// - OnPageChangedAsObservable().Skip(1).Subscribe(new Action<int>(ChangePage) via RGCTX).AddTo(this) (0x67F4A0,
    	//   the Component overload) stored to +0x40.
    	// Annotated disassembly: work/title-path/disas/pgp_03_0x89DCB0.txt
    	[Token(Token = "0x600003C")]
    	public void Present()
    	{
    		if (_paginationDisposable != null)
    		{
    			_paginationDisposable.Dispose();
    		}
    		ChangePage(_pagination.CurrentPageIndex);
    		_canvasGroup.Present();
    		_pagination.Present(_grid.PageCount, resetCurrentPage: false);
    		_paginationDisposable = _pagination.OnPageChangedAsObservable().Skip(1).Subscribe(ChangePage).AddTo(this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x89DF50 (shared) -- the subscription (+0x40) disposed and cleared when set
    	//   (cmp [rbx+0x40], 0 / je), CanvasGroupExtensions.Dismiss inlined on +0x20, tail jump Pagination.Dismiss on +0x30
    	// Annotated disassembly: work/title-path/disas/pgp_04_0x89DF50.txt
    	[Token(Token = "0x600003D")]
    	public void Dismiss()
    	{
    		if (_paginationDisposable != null)
    		{
    			_paginationDisposable.Dispose();
    			_paginationDisposable = null;
    		}
    		_canvasGroup.Dismiss();
    		_pagination.Dismiss();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x89E040 (shared) -- _prePageChanged (+0x38) Invoke behind the shipped null
    	//   test (0x18089E053), then tail call grid vtable +0x598 = ScriptableGrid slot 70 SelectPage(idx + 1)
    	[Token(Token = "0x600003E")]
    	private void ChangePage(int idx)
    	{
    		if (_prePageChanged != null)
    		{
    			_prePageChanged.Invoke();
    		}
    		_grid.SelectPage(idx + 1);
    	}

    	[Token(Token = "0x600003F")]
    	public PageableGridPanel()
    	{
    	}
    }
}
