using System.Collections.Generic;
using GigaCreation.Essentials.AddressablesUtils;
using GigaCreation.Essentials.MasterData;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x2000171")]
[CreateAssetMenu]
public class MasterData : MasterDataBase, IMasterData, IAddressablesMasterData, IMasterDataBase
{
	[Token(Token = "0x40006F5")]
	[FieldOffset(Offset = "0x18")]
	[Header("Addressables")]
	[SerializeField]
	private string[] _assetKeyNames;

	[Token(Token = "0x40006F6")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private AdditionalCatalogData[] _additionalCatalogDataList;

	[Token(Token = "0x40006F7")]
	[FieldOffset(Offset = "0x28")]
	[Header("Trial")]
	[SerializeField]
	private float _defaultHp;

	[Token(Token = "0x40006F8")]
	[FieldOffset(Offset = "0x2C")]
	[SerializeField]
	private float _skipTimeScaleDuringDebate;

	[Token(Token = "0x40006F9")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private float _voicePitchOnSkip;

	[Token(Token = "0x17000102")]
	public IEnumerable<string> AssetKeyNames
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
		// 601 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_assetKeyNames` (+0x18, string[]).
		[Token(Token = "0x6000638")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "7")]
		get
		{
			return _assetKeyNames;
		}
	}

	[Token(Token = "0x17000103")]
	public IReadOnlyList<AdditionalCatalogData> AdditionalCatalogDataList
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x377910 (generic reference-field getter)
		// 292 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_additionalCatalogDataList` (+0x20, AdditionalCatalogData[]).
		[Token(Token = "0x6000639")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "8")]
		get
		{
			return _additionalCatalogDataList;
		}
	}

	[Token(Token = "0x17000104")]
	public float DefaultHp
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x398CF0 (shared `movss xmm0,[rcx+0x28]; ret`, +0x28 `_defaultHp`)
		[Token(Token = "0x600063A")]
		[Address(RVA = "0x398CF0", Offset = "0x3976F0", VA = "0x180398CF0", Slot = "4")]
		get
		{
			return _defaultHp;
		}
	}

	[Token(Token = "0x17000105")]
	public float SkipTimeScaleDuringDebate
	{
		[Token(Token = "0x600063B")]
		[Address(RVA = "0x398D00", Offset = "0x397700", VA = "0x180398D00", Slot = "5")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000106")]
	public float VoicePitchOnSkip
	{
		[Token(Token = "0x600063C")]
		[Address(RVA = "0x398D10", Offset = "0x397710", VA = "0x180398D10", Slot = "6")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x600063D")]
	[Address(RVA = "0x398D20", Offset = "0x397720", VA = "0x180398D20")]
	public MasterData()
	{
	}
}
