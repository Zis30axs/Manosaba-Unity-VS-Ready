using System;
using GigaCreation.Essentials.Localization;
using GigaCreation.Essentials.Toggleables;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Serializable]
    [Token(Token = "0x200002D")]
    public class LabelData
    {
    	[Token(Token = "0x40000A2")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private LocalizableText _localizableText;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCE41F0: ToggleableStruct<float>.op_Implicit(1.0) -> +0x18
    	[Token(Token = "0x40000A3")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private ToggleableStruct<float> _scale = 1f;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCE41F0: ToggleableStruct<Color>.op_Implicit(data 0x2AE3470 = (0, 0, 0, 1)) -> +0x20
    	[Token(Token = "0x40000A4")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private ToggleableStruct<Color> _color = UnityEngine.Color.black;

    	[Token(Token = "0x40000A5")]
    	[FieldOffset(Offset = "0x34")]
    	[SerializeField]
    	private ToggleableStruct<float> _characterSpacing;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCE41F0: ToggleableStruct<HorizontalAlignmentOptions>.op_Implicit(1 = Left) -> +0x3C
    	[Token(Token = "0x40000A6")]
    	[FieldOffset(Offset = "0x3C")]
    	[SerializeField]
    	private ToggleableStruct<HorizontalAlignmentOptions> _alignment = HorizontalAlignmentOptions.Left;

    	[Token(Token = "0x40000A7")]
    	[FieldOffset(Offset = "0x48")]
    	[SerializeField]
    	private string _labelId;

    	[Token(Token = "0x17000021")]
    	public LocalizableText LocalizableText
    	{
    		[Token(Token = "0x60000C5")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000022")]
    	public ToggleableStruct<float> Scale
    	{
    		[Token(Token = "0x60000C6")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return default(ToggleableStruct<float>);
    		}
    	}

    	[Token(Token = "0x17000023")]
    	public ToggleableStruct<Color> Color
    	{
    		[Token(Token = "0x60000C7")]
    		[Address(RVA = "0xCE3F40", Offset = "0xCE2940", VA = "0x180CE3F40")]
    		get
    		{
    			return default(ToggleableStruct<Color>);
    		}
    	}

    	[Token(Token = "0x17000024")]
    	public ToggleableStruct<float> CharacterSpacing
    	{
    		[Token(Token = "0x60000C8")]
    		[Address(RVA = "0xCE3F60", Offset = "0xCE2960", VA = "0x180CE3F60")]
    		get
    		{
    			return default(ToggleableStruct<float>);
    		}
    	}

    	[Token(Token = "0x17000025")]
    	public ToggleableStruct<HorizontalAlignmentOptions> Alignment
    	{
    		[Token(Token = "0x60000C9")]
    		[Address(RVA = "0xCE3F70", Offset = "0xCE2970", VA = "0x180CE3F70")]
    		get
    		{
    			return default(ToggleableStruct<HorizontalAlignmentOptions>);
    		}
    	}

    	[Token(Token = "0x17000026")]
    	public string LabelId
    	{
    		[Token(Token = "0x60000CA")]
    		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60000CB")]
    	[Address(RVA = "0xCE3F80", Offset = "0xCE2980", VA = "0x180CE3F80")]
    	public bool IsEmpty()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000CC")]
    	[Address(RVA = "0xCE4110", Offset = "0xCE2B10", VA = "0x180CE4110")]
    	public bool ValidateLabelId(string value)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000CD")]
    	[Address(RVA = "0xCE41F0", Offset = "0xCE2BF0", VA = "0x180CE41F0")]
    	public LabelData()
    	{
    	}
    }
}
