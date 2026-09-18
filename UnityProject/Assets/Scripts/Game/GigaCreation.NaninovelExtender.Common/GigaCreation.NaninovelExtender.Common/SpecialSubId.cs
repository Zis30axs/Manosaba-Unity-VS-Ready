using Il2CppDummyDll;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x2000011")]
    public static class SpecialSubId
    {
    	[Token(Token = "0x2000012")]
    	private enum Id
    	{
    		[Token(Token = "0x4000036")]
    		Middle,
    		[Token(Token = "0x4000037")]
    		Front,
    		[Token(Token = "0x4000038")]
    		Forefront,
    		[Token(Token = "0x4000039")]
    		Overlay
    	}

    	[Token(Token = "0x4000033")]
    	private const float ZStepOfSubId = 25f;

    	[Token(Token = "0x4000034")]
    	private const float OverlayPositionZ = 0f;

    	[Token(Token = "0x6000021")]
    	[Address(RVA = "0xCFA4A0", Offset = "0xCF8EA0", VA = "0x180CFA4A0")]
    	public static bool TryGetModifiedPositionZ(float zOffset, string subId, out float z)
    	{
    		z = default;
    		z = default;
    		return default(bool);
    	}
    }
}
