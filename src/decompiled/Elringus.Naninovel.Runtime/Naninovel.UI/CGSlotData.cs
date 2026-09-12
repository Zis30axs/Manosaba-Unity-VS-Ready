using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x2000507")]
public readonly struct CGSlotData : IEquatable<CGSlotData>
{
	[Token(Token = "0x4001391")]
	[FieldOffset(Offset = "0x0")]
	public readonly string Id;

	[Token(Token = "0x4001392")]
	[FieldOffset(Offset = "0x8")]
	public readonly IReadOnlyList<string> TexturePaths;

	[Token(Token = "0x4001393")]
	[FieldOffset(Offset = "0x10")]
	public readonly IResourceLoader<Texture2D> TextureLoader;

	[Token(Token = "0x6001911")]
	[Address(RVA = "0x614440", Offset = "0x612E40", VA = "0x180614440")]
	public CGSlotData(string id, IEnumerable<string> texturePaths, IResourceLoader<Texture2D> textureLoader)
	{
	}

	[Token(Token = "0x6001912")]
	[Address(RVA = "0x456740", Offset = "0x455140", VA = "0x180456740", Slot = "4")]
	public bool Equals(CGSlotData other)
	{
		return default(bool);
	}

	[Token(Token = "0x6001913")]
	[Address(RVA = "0x614620", Offset = "0x613020", VA = "0x180614620", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x6001914")]
	[Address(RVA = "0x6146E0", Offset = "0x6130E0", VA = "0x1806146E0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
