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

    	[Token(Token = "0x40000C1")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

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
    	public CanvasGroup CanvasGroup
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 -- shared trivial getter: mov rax, [rcx+0x18]
    		[Token(Token = "0x60000ED")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _canvasGroup;
    		}
    	}

    	[Token(Token = "0x60000EE")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public PaginationIndicator()
    	{
    	}
    }
}
