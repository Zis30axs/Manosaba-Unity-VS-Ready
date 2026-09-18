using System;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Serializable]
    [Token(Token = "0x2000005")]
    public class ButtonData
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private SelectableSizeData _widthData;

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private SelectableSizeData _heightData;

    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private ResponseKind _responseKind;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private LabelData _labelData;

    	[Token(Token = "0x17000001")]
    	public SelectableSizeData WidthData
    	{
    		[Token(Token = "0x6000004")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000002")]
    	public SelectableSizeData HeightData
    	{
    		[Token(Token = "0x6000005")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000003")]
    	public ResponseKind ResponseKind
    	{
    		[Token(Token = "0x6000006")]
    		[Address(RVA = "0x440360", Offset = "0x43ED60", VA = "0x180440360")]
    		get
    		{
    			return default(ResponseKind);
    		}
    	}

    	[Token(Token = "0x17000004")]
    	public LabelData LabelData
    	{
    		[Token(Token = "0x6000007")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public ButtonData()
    	{
    	}
    }
}
