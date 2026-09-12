using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Ui;

[Serializable]
[Token(Token = "0x200003F")]
public class SelectableSizeData
{
	[Token(Token = "0x2000040")]
	public enum OverrideKind
	{
		[Token(Token = "0x400010B")]
		None,
		[Token(Token = "0x400010C")]
		Fixed,
		[Token(Token = "0x400010D")]
		Flexible
	}

	[Token(Token = "0x4000107")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private OverrideKind _kind;

	[Token(Token = "0x4000108")]
	[FieldOffset(Offset = "0x14")]
	[SerializeField]
	private float _fixedSize;

	[Token(Token = "0x4000109")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private float _padding;

	[Token(Token = "0x17000034")]
	public OverrideKind Kind
	{
		[Token(Token = "0x6000125")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
		get
		{
			return default(OverrideKind);
		}
	}

	[Token(Token = "0x17000035")]
	public float FixedSize
	{
		[Token(Token = "0x6000126")]
		[Address(RVA = "0x3686E0", Offset = "0x3670E0", VA = "0x1803686E0")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000036")]
	public float Padding
	{
		[Token(Token = "0x6000127")]
		[Address(RVA = "0x39FB70", Offset = "0x39E570", VA = "0x18039FB70")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x6000128")]
	[Address(RVA = "0xCEA680", Offset = "0xCE9080", VA = "0x180CEA680")]
	private bool IsFixed()
	{
		return default(bool);
	}

	[Token(Token = "0x6000129")]
	[Address(RVA = "0xCEA690", Offset = "0xCE9090", VA = "0x180CEA690")]
	private bool IsFlexible()
	{
		return default(bool);
	}

	[Token(Token = "0x600012A")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public SelectableSizeData()
	{
	}
}
