using System;
using GigaCreation.Essentials.Localization;
using GigaCreation.Essentials.Toggleables;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Serializable]
    [Token(Token = "0x200001C")]
    public class DialogData
    {
    	[Token(Token = "0x4000057")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private LocalizableText _titleLocalizableText;

    	[Token(Token = "0x4000058")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private ToggleableStruct<float> _minWidth;

    	[Token(Token = "0x4000059")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private bool _cancelableByAction;

    	[Token(Token = "0x400005A")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private LabelData[] _contentLabelsData;

    	[Token(Token = "0x400005B")]
    	[FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private ButtonData[] _buttonsData;

    	[Token(Token = "0x1700000B")]
    	public LocalizableText TitleText
    	{
    		[Token(Token = "0x6000065")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0, 5 B -- `mov rax, qword ptr [rcx + 0x10]; ret`: the backing field _titleLocalizableText (+0x10 in dump.cs)
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return _titleLocalizableText;
    		}
    	}

    	[Token(Token = "0x1700000C")]
    	public ToggleableStruct<float> MinWidth
    	{
    		[Token(Token = "0x6000066")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0, 5 B -- `mov rax, qword ptr [rcx + 0x18]; ret`: the backing field _minWidth (+0x18 in dump.cs)
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _minWidth;
    		}
    	}

    	[Token(Token = "0x1700000D")]
    	public bool CancelableByAction
    	{
    		[Token(Token = "0x6000067")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x468AC0, 5 B -- `movzx eax, byte ptr [rcx + 0x20]; ret`: the backing field _cancelableByAction (+0x20 in dump.cs)
    		[Address(RVA = "0x468AC0", Offset = "0x4674C0", VA = "0x180468AC0")]
    		get
    		{
    			return _cancelableByAction;
    		}
    	}

    	[Token(Token = "0x1700000E")]
    	public LabelData[] MessageLabelsData
    	{
    		[Token(Token = "0x6000068")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38F060, 5 B -- `mov rax, qword ptr [rcx + 0x28]; ret`: the backing field _contentLabelsData (+0x28 in dump.cs)
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
    		get
    		{
    			return _contentLabelsData;
    		}
    	}

    	[Token(Token = "0x1700000F")]
    	public ButtonData[] ButtonsData
    	{
    		[Token(Token = "0x6000069")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3786A0, 5 B -- `mov rax, qword ptr [rcx + 0x30]; ret`: the backing field _buttonsData (+0x30 in dump.cs)
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
    		get
    		{
    			return _buttonsData;
    		}
    	}

    	[Token(Token = "0x600006A")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public DialogData()
    	{
    	}
    }
}
