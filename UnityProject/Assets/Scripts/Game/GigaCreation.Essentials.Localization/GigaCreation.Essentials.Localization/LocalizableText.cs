#nullable enable annotations
﻿using System.Linq;
using System;
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0x468AC0, 5 B -- `movzx eax, byte ptr [rcx + 0x20]; ret`: the backing field _changeFont (+0x20 in dump.cs)
    		[Address(RVA = "0x468AC0", Offset = "0x4674C0", VA = "0x180468AC0")]
    		get
    		{
    			return _changeFont;
    		}
    	}

    	[Token(Token = "0x1700000B")]
    	private IReadOnlyDictionary<LocaleKind, string> LocalizedTextDict
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCD14C0, 743 B -- lazy: [this+0x28], else
    		//   _localizedTexts (+0x18).ToDictionary(<>c.b__7_0 = t.Locale, <>c.b__7_1 = t.Text) stored to +0x28
    		// Annotated disassembly: work/title-path/disas/lt_0xCD14C0.txt
    		[Token(Token = "0x6000016")]
    		[Address(RVA = "0xCD14C0", Offset = "0xCCFEC0", VA = "0x180CD14C0")]
    		get
    		{
    			if (_localizedTextDict == null)
    			{
    				_localizedTextDict = _localizedTexts.ToDictionary((LocalizedText text) => text.Locale, (LocalizedText text) => text.Text);
    			}
    			return _localizedTextDict;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD17B0, 267 B -- no locale: _defaultText (+0x10); else
    	//   LocalizedTextDict.GetValueOrDefault(locale.Value, _defaultText)
    	// Annotated disassembly: work/title-path/disas/lt_0xCD17B0.txt
    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0xCD17B0", Offset = "0xCD01B0", VA = "0x180CD17B0")]
    	public string GetText([Optional] LocaleKind? locale)
    	{
    		if (!locale.HasValue)
    		{
    			return _defaultText;
    		}
    		return LocalizedTextDict.GetValueOrDefault(locale.Value, _defaultText);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD18C0, 437 B -- IReadOnlyDictionary.TryGetValue on LocalizedTextDict;
    	//   found: text = value, true; not found: text = _defaultText (+0x10), false
    	// Annotated disassembly: work/title-path/disas/lt_0xCD18C0.txt
    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0xCD18C0", Offset = "0xCD02C0", VA = "0x180CD18C0")]
    	public bool TryGetLocalizedText(LocaleKind locale, out string text)
    	{
    		if (LocalizedTextDict.TryGetValue(locale, out string value))
    		{
    			text = value;
    			return true;
    		}
    		text = _defaultText;
    		return false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD1A80, 399 B -- a non-empty _defaultText (+0x10) -> false; else
    	//   _localizedTexts (+0x18).All(<>c.b__10_0 (0xCD1CC0): string.IsNullOrEmpty(text.Text))
    	// Annotated disassembly: work/title-path/disas/lt_0xCD1A80.txt
    	[Token(Token = "0x6000019")]
    	[Address(RVA = "0xCD1A80", Offset = "0xCD0480", VA = "0x180CD1A80")]
    	public bool IsEmpty()
    	{
    		if (!string.IsNullOrEmpty(_defaultText))
    		{
    			return false;
    		}
    		return _localizedTexts.All((LocalizedText text) => string.IsNullOrEmpty(text.Text));
    	}

    	[Token(Token = "0x600001A")]
    	[Address(RVA = "0xCD1C10", Offset = "0xCD0610", VA = "0x180CD1C10")]
    	public LocalizableText()
    	{
    	}
    }
}
