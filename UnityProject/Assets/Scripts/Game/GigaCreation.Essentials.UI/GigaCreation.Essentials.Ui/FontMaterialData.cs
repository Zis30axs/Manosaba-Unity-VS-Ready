using System;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GigaCreation.Essentials.Ui
{
    [Serializable]
    [Token(Token = "0x200002C")]
    public class FontMaterialData
    {
    	[Token(Token = "0x40000A0")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private AssetReferenceT<Material> _fontMaterialReference;

    	[Token(Token = "0x40000A1")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private LocaleKind _locale;

    	[Token(Token = "0x1700001F")]
    	public LocaleKind Locale
    	{
    		[Token(Token = "0x60000C2")]
    		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590")]
    		get
    		{
    			return default(LocaleKind);
    		}
    	}

    	[Token(Token = "0x17000020")]
    	public AssetReferenceT<Material> FontMaterialReference
    	{
    		[Token(Token = "0x60000C3")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60000C4")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public FontMaterialData()
    	{
    	}
    }
}
