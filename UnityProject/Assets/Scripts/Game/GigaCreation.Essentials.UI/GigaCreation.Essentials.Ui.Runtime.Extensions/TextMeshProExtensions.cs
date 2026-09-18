using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace GigaCreation.Essentials.Ui.Runtime.Extensions
{
    [Token(Token = "0x2000051")]
    public static class TextMeshProExtensions
    {
    	[Token(Token = "0x4000155")]
    	[FieldOffset(Offset = "0x0")]
    	private static readonly int s_glowColor;

    	[Token(Token = "0x60001A7")]
    	[Address(RVA = "0xCF1350", Offset = "0xCEFD50", VA = "0x180CF1350")]
    	public static Color GetGlowColor(this TMP_Text label)
    	{
    		return default(Color);
    	}

    	[Token(Token = "0x60001A8")]
    	[Address(RVA = "0xCF1400", Offset = "0xCEFE00", VA = "0x180CF1400")]
    	public static void SetGlowColor(this TMP_Text label, Color color)
    	{
    	}
    }
}
