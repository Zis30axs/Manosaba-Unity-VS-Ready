using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Serializable]
    [Token(Token = "0x200004F")]
    public class ToggleData
    {
    	[Token(Token = "0x4000148")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private SelectableSizeData _widthData;

    	[Token(Token = "0x4000149")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private SelectableSizeData _heightData;

    	[Token(Token = "0x400014A")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private LabelData _labelData;

    	[Token(Token = "0x17000042")]
    	public SelectableSizeData WidthData
    	{
    		[Token(Token = "0x6000199")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0, 5 B -- `mov rax, qword ptr [rcx + 0x10]; ret`: the backing field _widthData (+0x10 in dump.cs)
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return _widthData;
    		}
    	}

    	[Token(Token = "0x17000043")]
    	public SelectableSizeData HeightData
    	{
    		[Token(Token = "0x600019A")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0, 5 B -- `mov rax, qword ptr [rcx + 0x18]; ret`: the backing field _heightData (+0x18 in dump.cs)
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _heightData;
    		}
    	}

    	[Token(Token = "0x17000044")]
    	public LabelData LabelData
    	{
    		[Token(Token = "0x600019B")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x377910, 5 B -- `mov rax, qword ptr [rcx + 0x20]; ret`: the backing field _labelData (+0x20 in dump.cs)
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return _labelData;
    		}
    	}

    	[Token(Token = "0x600019C")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public ToggleData()
    	{
    	}
    }
}
