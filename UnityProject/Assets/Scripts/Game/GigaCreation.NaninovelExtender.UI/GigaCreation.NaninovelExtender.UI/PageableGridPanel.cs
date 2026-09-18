using System;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using Naninovel;
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
    		[Token(Token = "0x600003A")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x600003B")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x600003C")]
    	public void Present()
    	{
    	}

    	[Token(Token = "0x600003D")]
    	public void Dismiss()
    	{
    	}

    	[Token(Token = "0x600003E")]
    	private void ChangePage(int idx)
    	{
    	}

    	[Token(Token = "0x600003F")]
    	public PageableGridPanel()
    	{
    	}
    }
}
