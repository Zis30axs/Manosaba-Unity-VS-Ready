using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Localization
{
    [Token(Token = "0x200000B")]
    public static class LocaleKindExtensions
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD1050 -- compiler string switch (FNV-1a hash 0x811C9DC5 / 0x1000193, then
    	//   SequenceEqual): ja, ja-JP -> 0; en, en-US -> 1; zh, zh-CN, zh-Hans -> 2; zh-TW, zh-Hant -> 3; ko -> 4;
    	//   null or anything else -> 0
    	// Annotated disassembly: work/title-path/disas/lke_01_ToLocaleKind.txt
    	[Token(Token = "0x6000012")]
    	[Address(RVA = "0xCD1050", Offset = "0xCCFA50", VA = "0x180CD1050")]
    	public static LocaleKind ToLocaleKind(this string tag)
    	{
    		switch (tag)
    		{
    		case "ja":
    		case "ja-JP":
    			return LocaleKind.Ja;
    		case "en":
    		case "en-US":
    			return LocaleKind.EnUs;
    		case "zh":
    		case "zh-CN":
    		case "zh-Hans":
    			return LocaleKind.ZhHans;
    		case "zh-TW":
    		case "zh-Hant":
    			return LocaleKind.ZhHant;
    		case "ko":
    			return LocaleKind.Ko;
    		default:
    			return LocaleKind.Ja;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD13B0 -- 1 en-US, 2 zh-Hans, 3 zh-Hant, 4 ko; 0 and out-of-range -> ja
    	//   (the stub returned null, so LocalizationManagerExtended.GetDefaultLocale gave '' and Naninovel warned
    	//   "Failed to select locale: Locale '' is not available" on every start)
    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0xCD13B0", Offset = "0xCCFDB0", VA = "0x180CD13B0")]
    	public static string ToTagName(this LocaleKind locale)
    	{
    		switch (locale)
    		{
    		case LocaleKind.EnUs:
    			return "en-US";
    		case LocaleKind.ZhHans:
    			return "zh-Hans";
    		case LocaleKind.ZhHant:
    			return "zh-Hant";
    		case LocaleKind.Ko:
    			return "ko";
    		default:
    			return "ja";
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD1480 -- 1 -> 10 English, 2 -> 40 ChineseSimplified, 3 -> 41
    	//   ChineseTraditional, 4 -> 23 Korean; 0 and out-of-range -> 22 Japanese
    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xCD1480", Offset = "0xCCFE80", VA = "0x180CD1480")]
    	public static SystemLanguage ToSystemLanguage(this LocaleKind locale)
    	{
    		switch (locale)
    		{
    		case LocaleKind.EnUs:
    			return SystemLanguage.English;
    		case LocaleKind.ZhHans:
    			return SystemLanguage.ChineseSimplified;
    		case LocaleKind.ZhHant:
    			return SystemLanguage.ChineseTraditional;
    		case LocaleKind.Ko:
    			return SystemLanguage.Korean;
    		default:
    			return SystemLanguage.Japanese;
    		}
    	}
    }
}
