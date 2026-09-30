using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x200055B")]
    public interface IManagedUI
    {
    	[Token(Token = "0x170003BC")]
    	bool Visible
    	{
    		[Token(Token = "0x6001A90")]
    		get;
    		[Token(Token = "0x6001A91")]
    		set;
    	}

    	[Token(Token = "0x170003BD")]
    	Camera RenderCamera
    	{
    		[Token(Token = "0x6001A92")]
    		get;
    		[Token(Token = "0x6001A93")]
    		set;
    	}

    	[Token(Token = "0x170003BE")]
    	string ModalGroup
    	{
    		[Token(Token = "0x6001A94")]
    		get;
    	}

    	[Token(Token = "0x170003BF")]
    	int SortingOrder
    	{
    		[Token(Token = "0x6001A95")]
    		get;
    	}

    	[Token(Token = "0x1400008E")]
    	event Action<bool> OnVisibilityChanged;

    	[Token(Token = "0x6001A96")]
    	UniTask Initialize();

    	[Token(Token = "0x6001A97")]
    	UniTask ChangeVisibility(bool visible, [Optional] float? duration, [Optional] AsyncToken token);

    	[Token(Token = "0x6001A98")]
    	void SetFont(TMP_FontAsset font);

    	[Token(Token = "0x6001A99")]
    	void SetFontSize(int size);
    }
}
