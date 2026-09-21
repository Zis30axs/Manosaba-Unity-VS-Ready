using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace GigaCreation.Essentials.Ui.Runtime.Extensions
{
    [Token(Token = "0x2000051")]
    public static class TextMeshProExtensions
    {
    	// RECONSTRUCTED | Confidence: A | static initializer from the .cctor at 0xCF1550: Shader.PropertyToID("_GlowColor")
    	[Token(Token = "0x4000155")]
    	[FieldOffset(Offset = "0x0")]
    	private static readonly int s_glowColor = Shader.PropertyToID("_GlowColor");

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF1350 -- fontMaterial inlined (vtable +0x6E8 = TMP_Text slot 91
    	//   GetMaterial(m_sharedMaterial +0x118)), Material.GetColor(s_glowColor)
    	[Token(Token = "0x60001A7")]
    	[Address(RVA = "0xCF1350", Offset = "0xCEFD50", VA = "0x180CF1350")]
    	public static Color GetGlowColor(this TMP_Text label)
    	{
    		return label.fontMaterial.GetColor(s_glowColor);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF1400 -- fontMaterial inlined, Material.SetColor icall with s_glowColor
    	[Token(Token = "0x60001A8")]
    	[Address(RVA = "0xCF1400", Offset = "0xCEFE00", VA = "0x180CF1400")]
    	public static void SetGlowColor(this TMP_Text label, Color color)
    	{
    		label.fontMaterial.SetColor(s_glowColor, color);
    	}
    }
}
