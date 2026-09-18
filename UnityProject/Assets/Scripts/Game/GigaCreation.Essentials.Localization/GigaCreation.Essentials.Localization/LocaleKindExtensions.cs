using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Localization
{
    [Token(Token = "0x200000B")]
    public static class LocaleKindExtensions
    {
    	[Token(Token = "0x6000012")]
    	[Address(RVA = "0xCD1050", Offset = "0xCCFA50", VA = "0x180CD1050")]
    	public static LocaleKind ToLocaleKind(this string tag)
    	{
    		return default(LocaleKind);
    	}

    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0xCD13B0", Offset = "0xCCFDB0", VA = "0x180CD13B0")]
    	public static string ToTagName(this LocaleKind locale)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xCD1480", Offset = "0xCCFE80", VA = "0x180CD1480")]
    	public static SystemLanguage ToSystemLanguage(this LocaleKind locale)
    	{
    		return default(SystemLanguage);
    	}
    }
}
