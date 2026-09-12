using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x2000025")]
public class BackgroundMatcherExtended : BackgroundMatcher
{
	[Token(Token = "0x400005E")]
	private const float AspectRatio = 1.7777778f;

	[Token(Token = "0x400005F")]
	[FieldOffset(Offset = "0x50")]
	private readonly TransitionalSpriteRenderer _renderer;

	[Token(Token = "0x4000060")]
	[FieldOffset(Offset = "0x58")]
	private readonly BackgroundMetadata _metadata;

	[Token(Token = "0x6000089")]
	[Address(RVA = "0x313440", Offset = "0x311E40", VA = "0x180313440")]
	private BackgroundMatcherExtended(ICameraManager cameraManager, TransitionalSpriteRenderer renderer, BackgroundMetadata metadata)
	{
	}

	[Token(Token = "0x600008A")]
	[Address(RVA = "0x313520", Offset = "0x311F20", VA = "0x180313520")]
	public static BackgroundMatcherExtended CreateForBackground(BackgroundMetadata metadata, TransitionalRenderer renderer)
	{
		return null;
	}

	[Token(Token = "0x600008B")]
	[Address(RVA = "0x313720", Offset = "0x312120", VA = "0x180313720", Slot = "8")]
	protected override bool TryGetReferenceSize(out Vector2 referenceSize)
	{
		referenceSize = default;
		referenceSize = default;
		return default(bool);
	}
}
