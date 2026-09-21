using System;
using Il2CppDummyDll;
using Naninovel.UI;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Serializable]
    [Token(Token = "0x2000009")]
    public class CustomChoiceButtonData
    {
    	[Token(Token = "0x4000013")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private string _path;

    	[Token(Token = "0x4000014")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private ChoiceHandlerButton _customButtonPrefab;

    	[Token(Token = "0x17000005")]
    	public string Path
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 -- shared trivial getter: mov rax, [rcx+0x10]
    		[Token(Token = "0x600001B")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return _path;
    		}
    	}

    	[Token(Token = "0x17000006")]
    	public ChoiceHandlerButton CustomButtonPrefab
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 -- shared trivial getter: mov rax, [rcx+0x18]
    		[Token(Token = "0x600001C")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _customButtonPrefab;
    		}
    	}

    	[Token(Token = "0x600001D")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public CustomChoiceButtonData()
    	{
    	}
    }
}
