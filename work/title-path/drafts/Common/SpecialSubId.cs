using FastEnumUtility;
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFA4A0, 269 B
    	// xmm0 = zOffset (kept in xmm6), rdx = subId, r8 = &z; the result is stored to [rdi] at the single exit
    	// 0x180CFA566, so `z` is zOffset unless a case below overwrites it (also when the parse fails).
    	//   0x180CFA50E  FastEnum.TryParseInternal<Id>(subId, ignoreCase: dl = 1, out id)  -> false: return false
    	//   switch (id), by the sub-chain at 0x180CFA51D:
    	//     0 Middle     0x180CFA54E  z = zOffset - 25   (data_2AE2B18 = 25.0f  = 1 * ZStepOfSubId)
    	//     1 Front      0x180CFA542  z = zOffset - 50   (data_2AE2B94 = 50.0f  = 2 * ZStepOfSubId)
    	//     2 Forefront  0x180CFA536  z = zOffset - 75   (data_2AE2BAC = 75.0f  = 3 * ZStepOfSubId)
    	//     3 Overlay    0x180CFA52E  z = 0              (OverlayPositionZ; the result flag is `cl` = 1)
    	//     other        0x180CFA575  throw new ArgumentOutOfRangeException()
    	// Annotated disassembly: work/title-path/disas/ssi_01_TryGetModifiedPositionZ.txt
    	[Token(Token = "0x6000021")]
    	[Address(RVA = "0xCFA4A0", Offset = "0xCF8EA0", VA = "0x180CFA4A0")]
    	public static bool TryGetModifiedPositionZ(float zOffset, string subId, out float z)
    	{
    		z = zOffset;
    		if (!FastEnum.TryParse<Id>(subId, true, out Id id))
    		{
    			return false;
    		}
    		switch (id)
    		{
    		case Id.Middle:
    			z = zOffset - ZStepOfSubId;
    			return true;
    		case Id.Front:
    			z = zOffset - ZStepOfSubId * 2f;
    			return true;
    		case Id.Forefront:
    			z = zOffset - ZStepOfSubId * 3f;
    			return true;
    		case Id.Overlay:
    			z = OverlayPositionZ;
    			return true;
    		default:
    			throw new System.ArgumentOutOfRangeException();
    		}
    	}
    }
}
