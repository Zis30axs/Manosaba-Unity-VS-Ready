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

	// RECONSTRUCTED | Confidence: A | RVA: 0x313440, 217 B (BackgroundMatcher..ctor 0x44C590, then +0x50 and +0x58)
	[Token(Token = "0x6000089")]
	[Address(RVA = "0x313440", Offset = "0x311E40", VA = "0x180313440")]
	private BackgroundMatcherExtended(ICameraManager cameraManager, TransitionalSpriteRenderer renderer, BackgroundMetadata metadata)
		: base(cameraManager, renderer, metadata)
	{
		_renderer = renderer;
		_metadata = metadata;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x313520, 503 B
	// `MatchMode` is BackgroundMetadata +0x70 (dump.cs), compared with 3 = AspectMatchMode.Disable (metadata is
	// dereferenced without a null check). The renderer test is a hierarchy-depth `is` check. The constructor is inlined,
	// then CameraMatcher slot 5 `Start` is called. This mirrors Naninovel's BackgroundMatcher.CreateFor.
	[Token(Token = "0x600008A")]
	[Address(RVA = "0x313520", Offset = "0x311F20", VA = "0x180313520")]
	public static BackgroundMatcherExtended CreateForBackground(BackgroundMetadata metadata, TransitionalRenderer renderer)
	{
		if (metadata.MatchMode == AspectMatchMode.Disable || !(renderer is TransitionalSpriteRenderer spriteRenderer))
		{
			return null;
		}
		BackgroundMatcherExtended matcher = new BackgroundMatcherExtended(Engine.GetServiceOrErr<ICameraManager>(), spriteRenderer, metadata);
		matcher.Start();
		return matcher;
	}

	// RECONSTRUCTED | Confidence: A (values), B (whether the source divided a Vector2 or each component) | RVA: 0x313720, 535 B
	// Unity null checks on `_renderer` and TransitionalRenderer slot 7 `MainTexture`; `width` is Texture slot 5
	// (vtable +0x188). x = width / PixelsPerUnit (OrthoActorMetadata +0x38); y = width / AspectRatio (rdata
	// 0x2AE28C4 = 1.7777778) / PixelsPerUnit. The reference height is derived from a 16:9 width, not the texture height.
	[Token(Token = "0x600008B")]
	[Address(RVA = "0x313720", Offset = "0x312120", VA = "0x180313720", Slot = "8")]
	protected override bool TryGetReferenceSize(out Vector2 referenceSize)
	{
		if (_renderer != null && _renderer.MainTexture != null)
		{
			float width = _renderer.MainTexture.width;
			referenceSize = new Vector2(width / _metadata.PixelsPerUnit, width / AspectRatio / _metadata.PixelsPerUnit);
			return true;
		}
		referenceSize = default(Vector2);
		return false;
	}
}
