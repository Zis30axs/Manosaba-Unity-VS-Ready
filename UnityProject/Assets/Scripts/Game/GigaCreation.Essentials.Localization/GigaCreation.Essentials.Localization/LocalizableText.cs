#nullable enable annotations
﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Localization
{
    [Serializable]
    [Token(Token = "0x200000C")]
    public class LocalizableText
    {
    	[Token(Token = "0x4000010")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	[TextArea(1, 10)]
    	[SerializeField]
    	private string _defaultText;

    	[Token(Token = "0x4000011")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private LocalizedText[] _localizedTexts;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCD1C10: mov byte [rcx+0x20], 1
    	[Token(Token = "0x4000012")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private bool _changeFont = true;

    	[Token(Token = "0x4000013")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private Dictionary<LocaleKind, string> _localizedTextDict;

    	[Token(Token = "0x1700000A")]
    	public bool ChangeFont
    	{
    		[Token(Token = "0x6000015")]
    		[Address(RVA = "0x468AC0", Offset = "0x4674C0", VA = "0x180468AC0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700000B")]
    	private IReadOnlyDictionary<LocaleKind, string> LocalizedTextDict
    	{
    		[Token(Token = "0x6000016")]
    		[Address(RVA = "0xCD14C0", Offset = "0xCCFEC0", VA = "0x180CD14C0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0xCD17B0", Offset = "0xCD01B0", VA = "0x180CD17B0")]
    	public string GetText([Optional] LocaleKind? locale)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0xCD18C0", Offset = "0xCD02C0", VA = "0x180CD18C0")]
    	public bool TryGetLocalizedText(LocaleKind locale, out string text)
    	{
    		text = default;
    		text = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000019")]
    	[Address(RVA = "0xCD1A80", Offset = "0xCD0480", VA = "0x180CD1A80")]
    	public bool IsEmpty()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600001A")]
    	[Address(RVA = "0xCD1C10", Offset = "0xCD0610", VA = "0x180CD1C10")]
    	public LocalizableText()
    	{
    	}
    }
}
