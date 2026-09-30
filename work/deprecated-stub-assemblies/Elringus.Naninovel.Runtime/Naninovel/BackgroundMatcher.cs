using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000027")]
    public class BackgroundMatcher : CameraMatcher
    {
    	[Token(Token = "0x4000098")]
    	[FieldOffset(Offset = "0x40")]
    	private readonly TransitionalSpriteRenderer renderer;

    	[Token(Token = "0x4000099")]
    	[FieldOffset(Offset = "0x48")]
    	private readonly BackgroundMetadata metadata;

    	[Token(Token = "0x60000AB")]
    	[Address(RVA = "0x44C590", Offset = "0x44AF90", VA = "0x18044C590")]
    	public BackgroundMatcher(ICameraManager cameraManager = default, TransitionalSpriteRenderer renderer = default, BackgroundMetadata metadata = default)
    	{
    	}

    	[Token(Token = "0x60000AC")]
    	[Address(RVA = "0x44C690", Offset = "0x44B090", VA = "0x18044C690")]
    	public static BackgroundMatcher CreateFor(BackgroundMetadata metadata, TransitionalRenderer renderer)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000AD")]
    	[Address(RVA = "0x44C7F0", Offset = "0x44B1F0", VA = "0x18044C7F0", Slot = "8")]
    	protected override bool TryGetReferenceSize(out Vector2 referenceSize)
    	{
    		referenceSize = default;
    		referenceSize = default;
    		return default(bool);
    	}

    	[Token(Token = "0x60000AE")]
    	[Address(RVA = "0x44CA50", Offset = "0x44B450", VA = "0x18044CA50", Slot = "10")]
    	protected override void ApplyScale(float scaleFactor)
    	{
    	}
    }
}
