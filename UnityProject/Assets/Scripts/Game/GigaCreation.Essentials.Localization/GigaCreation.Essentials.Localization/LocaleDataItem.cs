using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GigaCreation.Essentials.Localization
{
    [Serializable]
    [Token(Token = "0x2000009")]
    public class LocaleDataItem
    {
    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private AssetReferenceT<TMP_FontAsset> _fontAssetReference;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private AssetReferenceT<Material>[] _fontMaterialReferences;

    	[Token(Token = "0x17000008")]
    	public AssetReferenceT<TMP_FontAsset> FontAssetReference
    	{
    		[Token(Token = "0x600000F")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return _fontAssetReference;
    		}
    	}

    	[Token(Token = "0x17000009")]
    	public ICollection<AssetReferenceT<Material>> FontMaterialReference
    	{
    		[Token(Token = "0x6000010")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _fontMaterialReferences;
    		}
    	}

    	[Token(Token = "0x6000011")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public LocaleDataItem()
    	{
    	}
    }
}
