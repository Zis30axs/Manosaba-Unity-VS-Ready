using System;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WitchTrials.Models;

namespace WitchTrials.Presenters;

[Serializable]
[Token(Token = "0x200014E")]
public class PerspectiveStageData
{
	[Token(Token = "0x400066D")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private PerspectiveStageKind _kind;

	[Token(Token = "0x400066E")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private AssetReferenceGameObject _stage;

	[Token(Token = "0x170000B6")]
	public PerspectiveStageKind Kind
	{
		[Token(Token = "0x600058E")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
		get
		{
			return default(PerspectiveStageKind);
		}
	}

	[Token(Token = "0x170000B7")]
	public AssetReferenceGameObject Stage
	{
		[Token(Token = "0x600058F")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000590")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public PerspectiveStageData()
	{
	}
}
