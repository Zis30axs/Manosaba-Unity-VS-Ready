using System.Collections.Generic;
using System.Text;
using FastEnumUtility;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x2000165")]
public static class WitchBookDataHelper
{
	// RECONSTRUCTED | Confidence: A | RVA: 0x38FB90 (.cctor), 183 B
	// `new StringBuilder()` stored to static +0x0.
	[Token(Token = "0x40006C2")]
	[FieldOffset(Offset = "0x0")]
	private static readonly StringBuilder s_builder = new StringBuilder();

	// RECONSTRUCTED | Confidence: A | RVA: 0x38F610, 848 B
	// - `s_builder.Length = 0`, then `Append("General/WitchBook/Clue")`.
	// - `id.Split('-')`: a single `'-'` (0x2D) char span, count `int.MaxValue`,
	//   options None.
	// - For each part, `Append("_")`, then `Append(part.PadLeft(3, '0'))`.
	//   PadLeft is inlined: when `3 - Length > 0` it allocates a 3-char string,
	//   fills it with `'0'` (0x30) and memcpys the part in; otherwise the part
	//   itself is used.
	// - Returns `s_builder.ToString()` (vtable +0x168 = slot 3, tail jump).
	// Example: "1-2" becomes "General/WitchBook/Clue_001_002".
	[Token(Token = "0x60005C9")]
	[Address(RVA = "0x38F610", Offset = "0x38E010", VA = "0x18038F610")]
	public static string BuildClueTextureAddress(string id)
	{
		s_builder.Length = 0;
		s_builder.Append("General/WitchBook/Clue");
		string[] array = id.Split('-');
		foreach (string text in array)
		{
			s_builder.Append("_");
			s_builder.Append(text.PadLeft(3, '0'));
		}
		return s_builder.ToString();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x38F970, 65 B
	// Tail-jumps to `string.Concat("General/WitchBook/Profile_", id)`.
	[Token(Token = "0x60005CA")]
	[Address(RVA = "0x38F970", Offset = "0x38E370", VA = "0x18038F970")]
	public static string BuildProfileTextureAddress(string id)
	{
		return "General/WitchBook/Profile_" + id;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x38F9C0, 450 B
	// - `new List<WitchBookCategory>(categoryNames.Length)` (`List..ctor(int)`
	//   0x857C60; NRE on a null array).
	// - For each name: `FastEnum.TryParse<WitchBookCategory>(name, out value)`
	//   (TryParseInternal with ignoreCase = 0). On success `Add(value)`, otherwise
	//   `Debug.LogError("Invalid WitchBook category name: " + name + ".")` and
	//   the loop continues.
	[Token(Token = "0x60005CB")]
	[Address(RVA = "0x38F9C0", Offset = "0x38E3C0", VA = "0x18038F9C0")]
	public static List<WitchBookCategory> ParseCategories(string[] categoryNames)
	{
		List<WitchBookCategory> list = new List<WitchBookCategory>(categoryNames.Length);
		foreach (string text in categoryNames)
		{
			if (FastEnum.TryParse<WitchBookCategory>(text, out WitchBookCategory result))
			{
				list.Add(result);
			}
			else
			{
				Debug.LogError("Invalid WitchBook category name: " + text + ".");
			}
		}
		return list;
	}
}
