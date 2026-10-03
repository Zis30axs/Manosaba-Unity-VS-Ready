using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Serializable]
    [Token(Token = "0x2000035")]
    public class PaginationIndicator
    {
    	[Token(Token = "0x40000C0")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private PaginationIndicatorStyle _style;

    	// RECONSTRUCTED | Confidence: A (name and type both pinned) | 0x18
    	//
    	// The serialised *name* is `_canvas`, not `_canvasGroup`: the shipped typetree stores the inline
    	// entries as `_style` + `_canvas` (res/Assets/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/
    	// SaveLoadUI~18.asset.json) and src/il2cpp/dump.cs prints the field as `_canvas` too.  The *type*
    	// is CanvasGroup, which the same disassembly proves: ChangeIndicatorStyle loads this field into rcx
    	// and calls `UnityEngine.CanvasGroup$$set_alpha` on it (pag_03_0xCE5D00.txt @0x180CE5D63).  Writing
    	// the YAML key as `_canvasGroup` made Unity drop the value as an unknown field, so the real `_canvas`
    	// stayed null and Pagination.Awake threw inside UIManager.InstantiateUIs.
    	[Token(Token = "0x40000C1")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private CanvasGroup _canvas;

    	[Token(Token = "0x1700002D")]
    	public PaginationIndicatorStyle Style
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x331650 -- shared trivial getter: mov eax, [rcx+0x10]
    		[Token(Token = "0x60000EC")]
    		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    		get
    		{
    			return _style;
    		}
    	}

    	[Token(Token = "0x1700002E")]
    	public CanvasGroup Canvas
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 -- shared trivial getter: mov rax, [rcx+0x18]
    		// (property name `Canvas` per dump.cs; the value is the CanvasGroup the disassembly sets alpha on)
    		[Token(Token = "0x60000ED")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _canvas;
    		}
    	}

    	[Token(Token = "0x60000EE")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public PaginationIndicator()
    	{
    	}
    }
}
