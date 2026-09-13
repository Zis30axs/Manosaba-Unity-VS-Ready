using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Serializable]
[Token(Token = "0x2000157")]
public class MapDataItem
{
	[Token(Token = "0x400069C")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private LocalizedText[] _buttonText;

	[Token(Token = "0x170000C3")]
	public IReadOnlyList<LocalizedText> ButtonText
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (generic reference-field getter)
		// 466 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_buttonText` (+0x10, LocalizedText[]).
		[Token(Token = "0x60005A7")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		get
		{
			return _buttonText;
		}
	}

	[Token(Token = "0x60005A8")]
	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
	public MapDataItem(LocalizedText[] buttonText)
	{
	}
}
